using System;


namespace CompositePattern
{
    class File : ComponentDirectory
    {
     
        
        public File(string name, Directory parent, int depth) : base(name,parent,depth)
        {
            
        }

        public override void listall(int depth)
        {
            Console.WriteLine(new String(' ', this.depth)+ name);
        }

        public override void list(int depth, Directory current)
        {
            Console.Write("this is not a directory");
        }

        public override int countall(Directory current)
        {
            return 0;
        }

    }
}
