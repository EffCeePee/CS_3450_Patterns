using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    class TestRegularExpressions
    {
        static void Main()
        {
            string[] sentences = 
        {
            "C# code",
            "Chapter 2: Writing Code",
            "Unicode",
            "no match here"
        };

            string sPattern = "code";

            foreach (string s in sentences)
            {
                System.Console.Write("{0}", s);

                if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    System.Console.WriteLine("  (match for '{0}' found)", sPattern);
                }
                else
                {
                    System.Console.WriteLine();
                }
            }

            // Keep the console window open in debug mode.
            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();

        }
    }
}
