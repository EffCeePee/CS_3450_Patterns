using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string location ="2.dat";
            StreamReader file = new StreamReader(location);
            Directory root = null;
            root = CreateTree(file,root,0);
            root.listall(root.depth);
            Console.ReadLine();
        }

     


        public static Directory CreateTree(StreamReader stream, Directory tree, int depth)
        {
            string line = null;
            int ndepth = depth;
            
            while ((line = stream.ReadLine()) != null)
            {
                if (tree == null)
                {
                    Directory dtemp = new Directory(line);
                    tree = dtemp;
                    CreateTree(stream, tree, depth);
                } 


                else if (!line.Contains(':')) // files
                {
                    File ftemp = new File(line);

                    if (ftemp.depth == ndepth)
                    {
                        tree.add(ftemp);
                        CreateTree(stream, tree, ftemp.depth);
                    }
                    else if(ftemp.depth > depth)
                    {   
                        tree.add(ftemp);
                        CreateTree(stream, tree, ftemp.depth);
                    }
                    else if (depth > ftemp.depth)
                    {
                        tree.add(ftemp);
                        CreateTree(stream, tree, ftemp.depth);
                        
                    }

                }


                else if (line.Contains(':'))  //Directories
                {
                    Directory dtemp = new Directory(line);
                    
                    if (dtemp.depth == ndepth)
                    {
                        //tree.add(dtemp);
                        //CreateTree(stream, dtemp, dtemp.depth);
                        tree.add(CreateTree(stream, dtemp, dtemp.depth));
                    }
                    else if (dtemp.depth > depth)
                    {
                        //tree.add(dtemp);
                        //CreateTree(stream, dtemp, dtemp.depth)
                        tree.add(CreateTree(stream, dtemp, dtemp.depth));
                    }
                    else if (depth > dtemp.depth)
                    {
                        CreateTree(stream, dtemp, dtemp.depth);
                        tree.add(dtemp);
                    }
                }

                
            }
            return tree;
        }
}

   public abstract class CompositeDirectory
   {
       public abstract void listall(int depth);

   }

   public class Directory : CompositeDirectory
    {
        public string name { get; set; }
        public int depth;
        public Directory parent;
        private List<CompositeDirectory> _directories = new List<CompositeDirectory>();

        public Directory(string oname)
        {
            name = oname.Trim();
            depth = finddepth(oname);
            this.parent = parent;
        }

        public int finddepth(string oname)
        {
            int count = 0;
            for (int i = 0; i < oname.Length; i++)
            {
                if (oname[i] == ' ')
                    count++;
            }
            return count;
        }


        public void add(CompositeDirectory d)
        {
            _directories.Add(d);
        }

        public override void listall(int depth)
        {
            Console.WriteLine(new String(' ', this.depth) + name);

            // Display each child element on this node
            foreach (var d in _directories)
            {
                d.listall(this.depth);
            }
        }

    }

    public class File : CompositeDirectory
    {
        public string name { get; set; }
        public int depth;
        public Directory parent { get; set; }

        public File(string oname)
                {
            name = oname.Trim();
            depth = finddepth(oname);
            this.parent = parent;
        }

        public int finddepth(string oname)
        {
            int count = 0;
            for (int i = 0; i < oname.Length; i++)
            {
                if (oname[i] == ' ')
                    count++;
            }
            return count;
        }

        public override void listall(int depth)
        {
            Console.WriteLine(new String(' ', this.depth) + name);
        }
        
    }

}