using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    abstract class ComponentDirectory
    {
        public string name;
        public int depth;
        public Directory parent;

        public ComponentDirectory(string name, Directory parent, int depth)
        {
            this.name = name;
            this.parent = parent;
            this.depth = depth;
        }

        public abstract void listall(int depth);
        public abstract void list(int depth, Directory current);
        public abstract int countall( Directory current);

    }

}
