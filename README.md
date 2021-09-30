# SOLID_Principles_Explores

They say they best way to learn is teach. Or at least in my case my understanding is fairly good so I want to consolidate my own knowledge and share with others.

Therefore this project will see code examples which show non-solid and solid coding examples.

I will use .NET, and maybe WinForms if I need any GUIs.

# Liskov Substition Principle - LSP

## Code which breaks Liskov

If we set the code in ```Program.cs``` to look like the following. This makes use of the ```LiskovEveilTwin.cs``` class (and related entities).
```
        static void Main(string[] args)
        {
            LiskovEvilTwin evilTwin = new LiskovEvilTwin();
            evilTwin.AddShape(new Circle(10, 10));
            evilTwin.AddShape(new Square(20, 20));
            evilTwin.DisplayList();
        }
```

Then we will see the code runs without a problem. However there is a bug hiding. In fact there are may problems with this code genrally, especially a number of other SOLID principles which has been broken.

The output should look like this.
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

This is the sort of bug, which unless you have good testing could go un-noticed.

## Fixing the broken code

We now have a ```LiskovGoodTwin``` class, but he starts out with the same evil as above.

However this class is willing to become good. So what is the minimum set of changes we need in order for the good twin to change to good from evil? 

First of all we create an interface ```iDrawableShape```
```
    interface iDrawableShape
    {
        void Draw();
    }
```

Then we make sure all the classes implement it. Lets look at this.

The Evil twin has these classe headers
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
        public void Draw() { Console.WriteLine($"Circle at {position.Item1} , {position.Item1}"); }
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

