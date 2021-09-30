# SOLID_Principles_Explores

They say they best way to learn is teach. Or at least in my case my understanding is fairly good so I want to consolidate my own knowledge and share with others.

Therefore this project will see code examples which show non-solid and solid coding examples.

I will use .NET, and maybe WinForms if I need any GUIs.

# Liskov Substition Principle

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

