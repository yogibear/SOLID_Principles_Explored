using LiskovEvilA;


namespace Liskov
{
    class Program
    {
        static void Main(string[] args)
        {
            LiskovEvilTwin evilTwin = new LiskovEvilTwin();
            evilTwin.AddShape(new Circle(10, 10));
            evilTwin.AddShape(new Square(20, 20));

            // Uncomment the following line and you will see that
            // the shape isn't listed, highlighting the breaking of LSP
            // evilTwin.AddShape(new GeoShape(20, 20));

            evilTwin.DisplayList();
        }
    }
}
