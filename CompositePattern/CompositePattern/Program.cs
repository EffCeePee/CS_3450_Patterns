using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = null;
            string[] choice;
            int depth = 0;
            Directory current;
            
            Directory root = null;

            Console.WriteLine("Please enter a file directory path");
            file = Console.ReadLine();
            StreamReader directory = new StreamReader(file);

          
            root = CheckLine(directory, root, depth);
           

            current = root;
            
            //Give User a list of options
            do
            {
            
                Console.WriteLine();
                Console.WriteLine("Please select a command:");
                Console.WriteLine();
                Console.WriteLine("list" + '\t' + '\t' + "-list all the entries in the current directory horizontally-");
                Console.WriteLine("listall" + '\t' + '\t' + "-lists all of the directory stating from current location-");
                Console.WriteLine("chdir" + '\t' + '\t' + "-changes directory to named adjacent directory-");
                Console.WriteLine("up" + '\t' + '\t' + "-moves up the the parent directory-");
                Console.WriteLine("count" + '\t' + '\t' + "-prints the number of files in current directory-");
                Console.WriteLine("countall" + '\t' + "-prints the number of files in the subdirectories-");
                Console.WriteLine("q" + '\t' + '\t' + "-quit the program-");
                Console.WriteLine();
                Console.Write(current.name + "> ");

                choice = Console.ReadLine().Split(' ');
                string name = null;
                if(choice.Count()>1)
                    name = choice[1];


                //Use Switch to run commands

                switch (choice[0])
                {
                    case "list":
                        current.list(current.depth,current);
                        break;
                    case "listall":
                        current.listall(current.depth);
                        break;
                    case "chdir":
                        if(name != null)
                            current = root.ChangeDirectory(name, current);
                        else
                            Console.WriteLine("Please enter a directory name.");
                        break;
                    case "up":
                        current = current.up(current);
                        break;
                    case "count":
                        Console.WriteLine(current.count(current));
                        break;
                    case "countall":
                        Console.WriteLine(current.countall(current));
                        break;
                    case "q":
                        //quits loop which ends program
                        break;
                    default:
                        Console.WriteLine("Please select a choice from the menu");
                        break;

                }
                
            } while (choice[0] != "q");

        }// end main


        // function to calculate the depth
        private static int depth;
        public static int Whitespaces(string line)
        {
            depth = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                    depth++;

            }
            
            return depth;

        }

        // recursive read in file function
        public static Directory CheckLine(StreamReader input, Directory parent, int cdepth)
        {
            string line;
            int depth;
            line = input.ReadLine();

            if (line != null)
            {

                depth = Whitespaces(line);

                if (depth == 0)
                {
                    parent = new Directory(line.Trim(), parent, depth);
                    cdepth = depth;
                    CheckLine(input, parent, cdepth);
                }
                else if (depth == cdepth && line.Contains(':'))
                {
                    Directory temp = new Directory(line.Trim(), parent, depth);
                    parent.Add(temp);
                    parent = temp;
                    CheckLine(input, parent, cdepth);
                }
                else if (depth == cdepth && !line.Contains(':'))
                {
                    parent.Add(new File(line.Trim(), parent, depth));
                    CheckLine(input, parent, cdepth);

                }
                else if (depth > cdepth)
                {
                    cdepth = depth;
                    if (line.Contains(':'))
                    {
                        Directory temp = new Directory(line.Trim(), parent, depth);
                        CheckLine(input, temp, cdepth);
                        parent.Add(temp);

                    }
                    else
                    {
                        parent.Add(new File(line.Trim(), parent, depth));
                        CheckLine(input, parent, cdepth);
                    }
                }
                else if (depth < cdepth)
                {
                    //cdepth = depth;
                    //parent = parent.parent;
                    //if (line.Contains(':'))
                    //{
                    //    parent.Add(new Directory(line.Trim(), parent, depth));
                    //}
                    //else
                    //{
                    //    parent.Add(new File(line.Trim(), parent, depth));
                    //}
                    //CheckLine(input, parent, cdepth);
                    //return parent;

                    cdepth = parent.depth;
                    if(parent.parent != null)
                    parent = parent.parent;
                    CheckLine(input, parent, cdepth);
                }
                
            }
            return parent;
        }

       


    } // end class program
}// end namespace composite pattern
