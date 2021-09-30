using System;
using System.Collections.Generic;


namespace LiskovEvilB
{
    class LiskovGoodTwin
    {
        List<GeoShape> shapes = new List<GeoShape>();

        public void AddShape(GeoShape newShape)
        {
            shapes.Add(newShape);
        }

        public void DisplayList()
        {
            foreach (GeoShape shape in shapes)
            {
                GeoShape.DrawMyShape(shape);
            }
        }
    }

    public enum TypeOfShape { non_shape, square, circle };

    public class GeoShape
    {
        private TypeOfShape whichShapeAmI = TypeOfShape.non_shape;
        
        internal Tuple<int, int> 
            position = new Tuple<int , int >(0,0);

        public GeoShape(int x, int y, TypeOfShape myShapeType = TypeOfShape.non_shape) 
        { 
            whichShapeAmI = myShapeType;
            position = new Tuple<int,int>(x, y);
        }

        public static void DrawMyShape(GeoShape shapeToDraw)
        {
            switch (shapeToDraw.whichShapeAmI)
            {
                case TypeOfShape.square:
                    (shapeToDraw as Square).Draw();
                    break;
                case TypeOfShape.circle:
                    (shapeToDraw as Circle).Draw();
                    break;
                default:
                    break;
            }
        }
    }

    public class Circle : GeoShape
    {
        public Circle(int x, int y) : base(x, y, TypeOfShape.circle) { }
        public void Draw() { Console.WriteLine($"Circle at {base.position.Item1} , {base.position.Item1}"); }
    }

    public class Square : GeoShape
    {
        public Square(int x, int y) : base(x, y, TypeOfShape.square) { }
        public void Draw() { Console.WriteLine($"Square at {base.position.Item1} , {base.position.Item1}"); }
    }
}
