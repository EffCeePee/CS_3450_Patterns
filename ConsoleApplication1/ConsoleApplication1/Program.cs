using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {


            int i = 0;

            while (i != 3)
            {
                Console.WriteLine("Press 1 for a low res widget");
                Console.WriteLine("Press 2 for a high res widget");
                Console.WriteLine("Press 3 to exit");

                i = Convert.ToInt32(Console.ReadLine());


                client c = new client();
                if (i != 3)
                    c.createWidget(i);

                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }


        }
    }


    public interface iWidgetFactory
    {
        void createLowResWidget();
        void createHighResWidget();
    }

    public class lowResWidgetFactory : iWidgetFactory
    {
        lowresdisplay d = lowresdisplay.getlrdisp();
        lowResPrint p = lowResPrint.gethrprint();
        widget lwidget = new widget();
        string dtype = null;
        string ptype = null;
        public void createLowResWidget()
        {

            dtype = d.createDisplay();
            ptype = p.createPrintDriver();
            lwidget.draw(ptype, dtype);

        }

        public void createHighResWidget()
        {
            Console.WriteLine("Error wrong factory");
        }
    }

    public class highResWidgetFactory : iWidgetFactory
    {
        highresdisplay d = highresdisplay.gethrdisp();
        highResPrint p = highResPrint.gethrprint();
        string ptype = null;
        string dtype = null;
        widget hwidget = new widget();

        public void createLowResWidget()
        {
            Console.WriteLine("Error wrong factory");
        }

        public void createHighResWidget()
        {

            dtype = d.createDisplay();
            ptype = p.createPrintDriver();
            hwidget.draw(ptype, dtype);
        }
    }

    public class client
    {
        private iWidgetFactory lowres;
        private iWidgetFactory highres;
        public void createWidget(int i)
        {
            if (i == 1)
            {
                lowres = new lowResWidgetFactory();
                lowres.createLowResWidget();
            }
            else if (i == 2)
            {
                highres = new highResWidgetFactory();
                highres.createHighResWidget();
            }
        }
    }

    public interface idisplay
    {
        string createDisplay();
    }

    public class highresdisplay : idisplay
    {
        private static highresdisplay hrdisp;

        private highresdisplay() { }

        public static highresdisplay gethrdisp()
        {
            if (hrdisp == null)
            {
                hrdisp = new highresdisplay();
                return hrdisp;
            }
            else
            {
                return hrdisp;
            }
        }


        public string createDisplay()
        {
            return "high";
        }

    }

    public class lowresdisplay : idisplay
    {
        private static lowresdisplay lrdisp;
        private lowresdisplay() { }
        public static lowresdisplay getlrdisp()
        {
            if (lrdisp == null)
            {
                lrdisp = new lowresdisplay();
                return lrdisp;
            }
            else
            {
                return lrdisp;
            }
        }


        public string createDisplay()
        {
            return "low";
        }

    }

    public interface iprint
    {
        string createPrintDriver();

    }

    public class highResPrint : iprint
    {
        private static highResPrint hrprint;
        private highResPrint() { }

        public static highResPrint gethrprint()
        {
            if (hrprint == null)
            {
                hrprint = new highResPrint();
                return hrprint;
            }
            else
            {
                return hrprint;
            }
        }
        public string createPrintDriver()
        {
            return "high";
        }
    }

    public class lowResPrint : iprint
    {
        private static lowResPrint lrprint;
        private lowResPrint() { }

        public static lowResPrint gethrprint()
        {
            if (lrprint == null)
            {
                lrprint = new lowResPrint();
                return lrprint;
            }
            else
            {
                return lrprint;
            }
        }

        public string createPrintDriver()
        {
            return "low";
        }
    }

    public interface iwidget
    {
        void draw(string p, string d);
    }

    public class widget : iwidget
    {
        public void draw(string p, string d)
        {
            Console.WriteLine("Drawing a widget using a {0} res display driver", d);
            Console.WriteLine("Printing a Document using a {0} res print driver ", p);
        }
    }
}
