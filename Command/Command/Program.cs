using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            string _line;
            StreamReader _file = new StreamReader("file.txt");
            List<string> _commands = new List<string>();
            int i = 0;


            while ((_line = _file.ReadLine()) != null)
            {
                _commands.Add(_line);

                Console.WriteLine(_commands[i]);

                i++;
            }

            Console.ReadLine();
        }
    }

    public class CmdList //invoker
    {
        List<ICommand> _clist = new List<ICommand>();

    }
    

    public abstract class ICommand //command interface
    {
        public abstract void execute();
    }

    public class AddCommand : ICommand //concrete command
    {
        Dictionary<string, string> d = new Dictionary<string, string>();
        public AddCommand(Dictionary<string,string> d)
        {
            this.d = d;
        }

        public override void execute()
        {
            
        }
    }

    public class Databases //reciever
    {
        
        Dictionary<string, Dictionary<string, string>> d = new Dictionary<string, Dictionary<string, string>>();
        //List<Dictionary<string, string>> d = new List<Dictionary<string, string>>();
        string key = null;
        string value = null;
        
        public Databases(string id)
        {
            d.ContainsKey(id);
        }

        public void addValuePair(string id)
        {
            d.ContainsKey(id);
        }
    }
}
