using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    class Program
    {
        
        static void Main(string[] args)
        {
            IDatabase db = new DataBase("file.dat");
            test(ref db);
            Console.WriteLine();
            IDatabase userdb = new DataBase("userdb.dat");
            IDatabase sdb = new SecureDB(ref db, ref userdb);
            test(ref sdb);
            Console.WriteLine();
            IDatabase cdb = new CacheDB(ref sdb);
            test(ref cdb);
            Console.WriteLine();

            try 
            {
            IDatabase db2 =  new DataBase("noname.dat");
            }
            catch (IOException)
            {
                Console.WriteLine("File does not exist");
            }


            Console.ReadLine();
        }

        
        static void test(ref IDatabase db)
        {
            try
            {
                Console.WriteLine(db.Get("one"));
                Console.WriteLine(db.Get("two"));
                Console.WriteLine(db.Get("two"));
                Console.WriteLine(db.Get("three"));
                Console.WriteLine(db.Get("four"));
                Console.WriteLine(db.Get("four"));
                Console.WriteLine(db.Get("five"));
                Console.WriteLine(db.Get("six"));
                Console.WriteLine(db.Get("one"));
                Console.WriteLine(db.Get("seven"));

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Driver Failed", e);
            }
        }

    }
}
