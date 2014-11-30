using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    class Directory : ComponentDirectory
    {
        
        public Directory newcurrent;

      
    
        public List<ComponentDirectory> _directories = new List<ComponentDirectory>();

        public Directory(string name, Directory parent, int depth) : base(name, parent, depth)
        {

        }

        public void Add(ComponentDirectory component)
        {
            _directories.Add(component);
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

        public Directory ChangeDirectory(string name, Directory current)
        {
            for (int i = 0; i < current._directories.Count; i++)
            {
                if (current._directories[i].name == name)
                {
                    newcurrent = current._directories[i] as Directory;
                }
            }

            if (newcurrent == null)
            {
                Console.WriteLine("Cannot change directory");
                newcurrent = current;
            }

            return newcurrent;
        }

        public override void list(int depth, Directory current)
        {
            for (int i = 0; i < current._directories.Count; i++)
            {
                Console.Write(current._directories[i].name + " ");
            }
        }

        public Directory up(Directory current)
        {
            if (current.parent != null)
            {
                return current.parent;
            }
            else
            {
                Console.WriteLine("You are at the root");
                return current;
            }
        }

        public int count(Directory current)
        {
            int count = 0; 
            foreach (var D in current._directories)
            {
                if (!D.name.Contains(':'))
                {
                    count++;
                }
            }

            return count;
        }

        public override int countall(Directory current)
        {
            int count = 0;
            foreach (var d in current._directories)
            {
                if (!d.name.Contains(':'))
                {
                    count++;
                }
                if (d.name.Contains(':'))
                {
                    current = d as Directory;
                    count += d.countall(current);
                }
                
            }

            return count;
        }
        
    }
}
