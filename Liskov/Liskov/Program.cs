using LiskovEvilA;
using LiskovEvilB;

namespace Liskov
{
    class Program
    {
        static void Main(string[] args)
        {
            LiskovEvilTwin evilTwin = new LiskovEvilTwin();
            evilTwin.AddShape(new LiskovEvilA.Circle(10, 10));
            evilTwin.AddShape(new LiskovEvilA.Square(20, 20));
            evilTwin.AddShape(new LiskovEvilA.GeoShape(20, 20)); // Is not displayed
            evilTwin.DisplayList();


            LiskovGoodTwin goodTwin = new LiskovGoodTwin();
            goodTwin.AddShape(new LiskovEvilB.Circle(10, 10));
            goodTwin.AddShape(new LiskovEvilB.Square(20, 20));
            goodTwin.AddShape(new LiskovEvilB.GeoShape(20, 20));

            goodTwin.DisplayList();
        }
    }
}
