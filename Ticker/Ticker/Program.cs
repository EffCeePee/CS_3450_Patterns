using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace StockTicker
{
    class Program
    {
        static void Main(string[] args)
        {

            Observer o = new Observer();
            o.parseTicker();
            o.averageStock();

            Console.ReadLine();
        }
    }



    public class Subject
    {
        const int SIZE = 800;
        int counter = 0;
        string line;
        string[] lines = new string[SIZE];

        public Subject()
        {

            // open a stream object and point it to a file
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\colton.parry\Desktop\3450-Program2\Ticker.dat");

            while ((line = file.ReadLine()) != null)
            {
                lines[counter] = line;
                counter++;
            }

            file.Close();
        }

        public string[] getFileIn()
        {
            return lines;
        }

    }  // end class


    public class Observer
    {
        const int ROW = 800;
        const int COLUMN = 9;
        string[] ticker = new string[ROW];
        string[,] parsedData = new string[ROW, COLUMN];
        char[] numSplitters = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        Regex tickerRegex = new Regex(@"[a-zA-Z]*[\s]+$");
        int index = 0;



        public Observer()
        {

            Subject f = new Subject();
            ticker = f.getFileIn();
            Console.ReadLine();

        }

        public void parseTicker()
        {
            for (int i = 0; i < ticker.Length; i++)
            {
                string temp = ticker[i];
                index = 0;

                if (temp == null || temp == "")
                {
                    continue;

                }
                else if (temp.Contains("Last updated"))
                {
                    parsedData[i, 0] = temp;
                }
                else
                {

                    string[] aTemp = temp.Split(numSplitters, 2);
                    parsedData[i, 0] = aTemp[0];

                    string[] numbers = Regex.Split(temp, @"^[a-zA-Z][a-zA-Z\s.&',-]+");
                    string bTemp = numbers[1];

                    string tickerSymbol = "";
                    tickerSymbol = tickerRegex.Match(aTemp[0]).ToString();
                    parsedData[i, 1] = tickerSymbol;
                                      
                    string[] cTemp = Regex.Split(bTemp, @"[\s]+");
                    for (int j = 2; j < COLUMN; j++)
                    {
                     
                        cTemp[index].Trim();
                        parsedData[i, j] = cTemp[index];
                        index++;
                    }


                }
            } //end outer for loop



            //string[] lines = new string[ROW];


            //Console.WriteLine(parsedData[0, 0]);
            //Console.ReadLine();
            //for (int e = 0
            //    ; e < ROW; e++)
            //{
            //    if (parsedData[e, 0] != null)
            //    {
            //        lines[e] = parsedData[e, 0] + "," + parsedData[e, 1] + "," + parsedData[e, 2] + "," + parsedData[e, 3] + "," + parsedData[e, 4] + "," + parsedData[e, 5] + "," + parsedData[e, 6] + "," + parsedData[e, 7] + "," + parsedData[e, 8];

            //        Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", parsedData[e, 0], parsedData[e, 1], parsedData[e, 2], parsedData[e, 3], parsedData[e, 4], parsedData[e, 5], parsedData[e, 6], parsedData[e, 7], parsedData[e,8]);
            //    }
            //}

            //System.IO.File.WriteAllLines(@"C:\Users\colton.parry\Desktop\3450-Program2\lines.dat", lines);

        }// end method

        public void averageStock()
        {
            double average = 0;
            string time = "";
            int count = 0;

            for (int i = 0; i < ROW; i++)
            {

                if (parsedData[i, 0] == null)
                {
                    continue;

                }
                else if (parsedData[i, 0].Contains("Last updated"))
                {
                    if (average != 0)
                    {
                        average = average / count;
                        Console.WriteLine("{0}, Average Price {1}", time, average);
                    }
                    time = parsedData[i, 0].Remove(0, 13);
                    average = 0;
                    count = 0;
                }
                else
                {
                    average += Convert.ToDouble(parsedData[i, 2]);
                    count++;
                }

            }

            average = average / count;

            Console.WriteLine("{0}, Average Price {1}", time, average);
        }



    }// end class
}// end namespace

