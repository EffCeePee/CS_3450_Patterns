using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace program3
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileLoc = "Texting.txt";
            int [] choices = new int [25];
            int choice = 0;
            int index = 0;
            string userIn = "0";

            StreamOutput oPut = new StreamOutput(fileLoc);
            LineOutput lOPut = new LineOutput(oPut);
            

            Console.WriteLine("Press 1 to run the seeded file or 2 to enter a file name of your choice");

            
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 2)
            {
                Console.WriteLine("Please enter in a file name. (Assuming the file is in the local directory)");
                userIn = Console.ReadLine();
            }
            else
            {
                userIn = "decorator.dat";
            }


            string line;
            StreamReader file = new StreamReader(userIn);
            line = file.ReadToEnd();

            Console.WriteLine("Please select from follwing ways to decorate the file");
            while (choice != 5)
            {
                Console.WriteLine("Press 1 to add a new line with each write");
                Console.WriteLine("Press 2 to add a number to each line");
                Console.WriteLine("Press 3 to write out two streams");
                Console.WriteLine("Press 4 to write out only the lines that meet a criteria");
                Console.WriteLine("Press 5 to decorate the files and end the program");

                choice = Convert.ToInt32(Console.ReadLine());

                if(choice != 5)
                {
                    choices[index] = choice;
                    index++;
                } else
                {
                    Console.WriteLine("Thanks Please check your files.");
                }

            }


            oPut.write(line);
            Console.ReadLine();
        }
    }


    public abstract class Output
    {
        public abstract void write(Object o);
        

    }
    
    public class StreamOutput : Output
    {
        private StreamWriter sink;
        //private StreamWriter sink;
        public StreamOutput(string fileloc)
        {
          
            sink = new StreamWriter(fileloc);
        }
        public override void write(Object o)
        {
            writeString(o.ToString());
        }
        public void writeString(String s)
        {
            try
            {
                using(sink)
                {
                    sink.Write(s);
                }
               
            }
            catch (IOException)
            {
                throw new IOException("The file did not write");
            }
        }
    
    } // end streamOutput class 
    
    public abstract class decorator : Output
    {

        protected Output baseOutput = null;

        protected decorator(Output o)
        {
            baseOutput = o;
        }
     

        void write(Output baseOutput)
        {
            baseOutput.write(baseOutput);
        }

    }

    public class LineOutput : decorator
    {


        public LineOutput(Output baseOutput) : base(baseOutput)
        {
            // this is where I would manipulate the stream to add lines
        }

        public override void write(object o)
        {
            baseOutput.write(o);
        }

        
    }

    public class NumberedOutput : decorator
    {
         public NumberedOutput(Output baseOutput) : base(baseOutput)
        {
            // this is where I would manipulate the stream to add numbers
        }

        public override void write(object o)
        {
            baseOutput.write(o);
        }
    }

    public class TeeOutput : decorator
    {
        public TeeOutput(Output baseOutput) : base(baseOutput)
        {
            // this is where I would manipulate the stream into two parts
        }

        public override void write(object o)
        {
            baseOutput.write(o);
        }
    }

    public class FilterOutput : decorator
    {
        public FilterOutput(Output baseOutput) : base(baseOutput)
        {
            // this is where I would manipulate the stream print only what was filtered out
        }

        public override void write(object o)
        {
            baseOutput.write(o);
        }

    }

} // end namespace


