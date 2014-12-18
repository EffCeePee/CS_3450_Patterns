using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape s = new Point();
            s.display();
            s.fill();
            s.undisplay();
            Console.WriteLine();
            s = new Line();
            s.display();
            s.fill();
            s.undisplay();
            Console.WriteLine();
            s = new Rectangle();
            s.display();
            s.fill();
            s.undisplay();
            Console.WriteLine();
            s = new Circle();
            s.display();
            s.fill();
            s.undisplay();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }

    public abstract class Shape
    {
        public void setLocation() { Console.WriteLine("Sets location"); }
        public void getLocation(){ Console.WriteLine("gets location");}
        public abstract void display();
        public abstract void fill();
        public void setcolor() { Console.WriteLine("Sets Color"); }
        public abstract void undisplay();

    }

    public class Point : Shape
    {
        public override void display()
        {
            Console.WriteLine("Displaying a Point");
        }

        public override void fill()
        {
            Console.WriteLine("Filling a point");
        }

        public override void undisplay()
        {
            Console.WriteLine("Undiplaying a point");
        }
    }

    public class Line : Shape
    {
        public override void display()
        {
            Console.WriteLine("Displaying a Line");
        }

        public override void fill()
        {
            Console.WriteLine("Filling a Line");
        }

        public override void undisplay()
        {
            Console.WriteLine("Undiplaying a Line");
        }
    }

    public class Rectangle : Shape
    {
        public override void display()
        {
            Console.WriteLine("Displaying a Rectangle");
        }

        public override void fill()
        {
            Console.WriteLine("Filling a Rectangle");
        }

        public override void undisplay()
        {
            Console.WriteLine("Undiplaying a Rectangle");
        }
    }

    public class Circle : Shape
    {
        XXCircle xc = new XXCircle();

        public override void display()
        {
            xc.display();
        }

        public override void fill()
        {
            xc.fill();
        }

        public override void undisplay()
        {
            xc.undisplay();
        }
    }


   public class XXCircle
   {
       public void setLocation() { Console.WriteLine("Sets location"); }
       public void getLocation() { Console.WriteLine("gets location"); }
       public void display() {Console.WriteLine("Displaying a Circle");}
       public void fill() {Console.WriteLine("Filling a Circle");}
       public void setcolor() { Console.WriteLine("Sets Color"); }
       public void undisplay() { Console.WriteLine("Undiplaying a Circle"); }
   }
}
