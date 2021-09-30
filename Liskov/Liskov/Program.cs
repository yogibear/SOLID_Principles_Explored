using System;

namespace Liskov
{
    class Program
    {
        static void Main(string[] args)
        {
            LiskovEvilTwin evilTwin = new LiskovEvilTwin();
            evilTwin.AddShape(new Circle(10, 10));
            evilTwin.AddShape(new Square(20, 20));
            evilTwin.DisplayList();
        }
    }
}
