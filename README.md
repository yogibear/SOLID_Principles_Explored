# SOLID_Principles_Explored

They say they best way to learn is teach. Or at least in my case my understanding is fairly good so I want to consolidate my own knowledge and share with others.

Therefore this project will see code examples which show non-solid and solid coding examples.

# Liskov Substition Principle - LSP

* Subclasses and derived classes should be substitutable for the base/parent class
* Objects are replaceable with instances of subtype without altering correctness

## Code which breaks Liskov

If you look at the LiskovEvilTwin.cs file you will see an example of code which breaks the Liskov Substitution Principle. 

When the code in ```Program.cs``` looks like the following and run it. All looks ot be fine.
```
static void Main(string[] args)
{
	LiskovEvilTwin evilTwin = new LiskovEvilTwin();
	evilTwin.AddShape(new Circle(10, 10));
	evilTwin.AddShape(new Square(20, 20));
	evilTwin.DisplayList();
}
```

The code runs without a problem. However there is a bug hiding. In fact there are may problems with this code genrally, especially a number of other SOLID principles which has been broken.

The output looks like the following which is correct.
```
Circle at 10 , 10
Square at 20 , 20
```

However if we change the code in ```Program.cs``` to look like the following.
```
static void Main(string[] args)
{
	LiskovEvilTwin evilTwin = new LiskovEvilTwin();
	evilTwin.AddShape(new Circle(10, 10));
	evilTwin.AddShape(new Square(20, 20));
	evilTwin.AddShape(new GeoShape(20, 20));
	evilTwin.DisplayList();
}
```

And then run it, we get the same output as before. Even though we have an extra shape.
```
Circle at 10 , 10
Square at 20 , 20
```

This is the sort of bug which LSP helps you avoid.

## Fixing the broken code

Now lets work through the bear minimum we need to change step by step, to see how we fix this violation of LSP.

We now have a ```LiskovGoodTwin``` class, but it starts out with the same evil as above.

However this class will be modofied to not violate LSP.  

First of all we create an interface ```iDrawableShape```
```
interface iDrawableShape
{
	void Draw();
}
```

Then we make sure all the classes implement it. Lets look at this.

The before set of class headers
```
public class GeoShape 
public class Circle : GeoShape
public class Square : GeoShape
```

We add the interface to the class definitions like this.
```
public class GeoShape : iDrawableShape
public class Circle : GeoShape , iDrawableShape
public class Square : GeoShape, iDrawableShape
```

The GIT diff of the change looks like ...
```
+    interface iDrawableShape
+    {
+        void Draw();
+    }
+
     public enum TypeOfShape { non_shape, square, circle };
 
-    public class GeoShape
+    public class GeoShape : iDrawableShape
     {
         private TypeOfShape whichShapeAmI = TypeOfShape.non_shape;
         
         internal Tuple<int, int> 
             position = new Tuple<int , int >(0,0);
@@ -51,17 +56,17 @@ namespace LiskovEvilB
                     break;
             }
         }
     }
 
-    public class Circle : GeoShape
+    public class Circle : GeoShape , iDrawableShape
     {
         public Circle(int x, int y) : base(x, y, TypeOfShape.circle) { }
         public void Draw() { Console.WriteLine($"Circle at {base.position.Item1} , {base.position.Item1}"); }
     }
 
-    public class Square : GeoShape
+    public class Square : GeoShape, iDrawableShape
     {
         public Square(int x, int y) : base(x, y, TypeOfShape.square) { }
         public void Draw() { Console.WriteLine($"Square at {base.position.Item1} , {base.position.Item1}"); }
     }
 }
 ```
 
However we are not finished yet because now the program wont compile.
```
Severity	Code	Description	Project	File	Line	Suppression State
Error	CS0535	'GeoShape' does not implement interface member 'iDrawableShape.Draw()'	
```
Which results because whilst circle and square have ```Draw()``` methods, ```GeoShape``` does not. Lets add one.
```
			public void Draw() { Console.WriteLine($"GeoShape at {position.Item1} , {position.Item1}"); }
```
However we still get the same output as before, GeoShape isn't listed but the others are. This is, of course because our switch statement doesn't cater for the GeoShape. Simple fix.
```
                case TypeOfShape.non_shape:
                    (shapeToDraw as GeoShape).Draw();
                    break;
```

Now for a comparison lets update ```Program.cs``` so that it runs the good and evil twins and we can see output from both. Among some other minor changes, we have this.
```
             LiskovEvilTwin evilTwin = new LiskovEvilTwin();
-            evilTwin.AddShape(new Circle(10, 10));
-            evilTwin.AddShape(new Square(20, 20));
+            evilTwin.AddShape(new LiskovEvilA.Circle(10, 10));
+            evilTwin.AddShape(new LiskovEvilA.Square(20, 20));
+            evilTwin.AddShape(new LiskovEvilA.GeoShape(20, 20)); // Is not displayed
+            evilTwin.DisplayList();
 
-            // Uncomment the following line and you will see that
-            // the shape isn't listed, highlighting the breaking of LSP
-            // evilTwin.AddShape(new GeoShape(20, 20));
 
-            evilTwin.DisplayList();
+            LiskovGoodTwin goodTwin = new LiskovGoodTwin();
+            goodTwin.AddShape(new LiskovEvilB.Circle(10, 10));
+            goodTwin.AddShape(new LiskovEvilB.Square(20, 20));
+            goodTwin.AddShape(new LiskovEvilB.GeoShape(20, 20));
+
+            goodTwin.DisplayList();
```

Now when we run the program we see the out of good and evil. Evil demonstrating the breaking of LSP, and the Good one showing the fix.
```
EVIL TWIN
=========
Circle at 10 , 10
Square at 20 , 20

GOOD TWIN
=========
Circle at 10 , 10
Square at 20 , 20
GeoShape at 20 , 20
```
However were are not finished yet because we now have two warnings.
```
Severity	Code	Description	Project	File	Line	Suppression State
Warning	CS0108	'Square.Draw()' hides inherited member 'GeoShape.Draw()'. Use the new keyword if hiding was intended.	Liskov	...\Liskov\Liskov\LiskovGoodTwin.cs	77	Active
Warning	CS0108	'Circle.Draw()' hides inherited member 'GeoShape.Draw()'. Use the new keyword if hiding was intended.	Liskov	...\Liskov\Liskov\LiskovGoodTwin.cs	71	Active
```

We don't want those warnings because the code is not as clear as it might be. Easy fix. We mark the GeoShape Draw() method as virtual. Then mark the circle and sqaure classes version as override. 
```
public override void Draw()
public virtual void Draw()
```

We can see the full change here;
```
-        public void Draw() { Console.WriteLine($"GeoShape at {position.Item1} , {position.Item1}"); }
+        public virtual void Draw() { Console.WriteLine($"GeoShape at {position.Item1} , {position.Item1}"); }
 
         public static void DrawMyShape(GeoShape shapeToDraw)
         {
             switch (shapeToDraw.whichShapeAmI)
             {
@@ -66,14 +66,14 @@ namespace LiskovEvilB
     }
 
     public class Circle : GeoShape , iDrawableShape
     {
         public Circle(int x, int y) : base(x, y, TypeOfShape.circle) { }
-        public void Draw() { Console.WriteLine($"Circle at {base.position.Item1} , {base.position.Item1}"); }
+        public override void Draw() { Console.WriteLine($"Circle at {base.position.Item1} , {base.position.Item1}"); }
     }
 
     public class Square : GeoShape, iDrawableShape
     {
         public Square(int x, int y) : base(x, y, TypeOfShape.square) { }
-        public void Draw() { Console.WriteLine($"Square at {base.position.Item1} , {base.position.Item1}"); }
+        public override void Draw() { Console.WriteLine($"Square at {base.position.Item1} , {base.position.Item1}"); }
     }
```
Now we are no longer violating LSP.





## Other ways to confirm to LSP