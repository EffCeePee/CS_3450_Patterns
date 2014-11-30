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
            Subject s = new Subject();
            AvgObserver avg = new AvgObserver();
            PercentObserver perc = new PercentObserver();
            getInfoOberver gio = new getInfoOberver();
            
            
            s.parseData();
            s.add(avg);
            s.add(perc);
            s.add(gio);
            s.update();
            
            Console.ReadLine();
        }
    }

    public abstract class ASubject
    {
        public abstract void add(Observer o);
        public abstract void update();
        public abstract void list();
    }


    public class Subject : ASubject
    {
        const int ROW = 800;
        const int COLUMN = 9;
        int counter = 0;
        string line;
        List<Observer> olist = new List<Observer>();
        string[] ticker = new string[ROW];
        string[,] parsedData = new string[ROW, COLUMN];
        char[] numSplitters = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        Regex tickerRegex = new Regex(@"[a-zA-Z]*[\s]+$");
        int index = 0;



        public Subject()
        {

            // open a stream object and point it to a file
            System.IO.StreamReader file = new System.IO.StreamReader(@"Ticker.dat");

            while ((line = file.ReadLine()) != null)
            {
                ticker[counter] = line;
                counter++;
            }

            file.Close();
        }

        public void parseData()
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
        }


        public override void add(Observer o)
        {
            olist.Add(o);
        }

        public override void list()
         {
             for (int i = 0; i < olist.Count; i++)
             {
                 Console.WriteLine(olist[i]);
             }
         }

        public override void update()
        {
            for (int i = 0; i < olist.Count; i++ )
            {
                olist[i].update(parsedData);
                Console.ReadKey();
            }
        }

    }  // end class

    public abstract class Observer
    {
        public abstract void update(string[,] pd);
    }

    public class  AvgObserver : Observer
    {
        const int ROW = 800;
        const int COLUMN = 9;
        string [,] data = new string[ROW, COLUMN];

        public override void update(string[,] pd)
        {
            data = pd;
            averageStock();
        }

        public void averageStock()
        {
            double average = 0;
            string time = "";
            int count = 0;
            string[] lines = new string[100];
            int linesIndex = 0;

            for (int i = 0; i < ROW; i++)
            {

                if (data[i, 0] == null)
                {
                    continue;

                }
                else if (data[i, 0].Contains("Last updated"))
                {
                    if (average != 0)
                    {
                        average = average / count;
                        Console.WriteLine("{0}, Average Price {1}", time, average);
                        lines[linesIndex] = time + " Average Price " + average;
                        linesIndex++;
                    }
                    time = data[i, 0].Remove(0, 13);
                    average = 0;
                    count = 0;
                }
                else
                {
                    average += Convert.ToDouble(data[i, 2]);
                    count++;
                }

            }

            average = average / count;

            Console.WriteLine("{0}, Average Price {1}", time, average);
            lines[linesIndex] = time + " Average Price " + average;
            System.IO.File.WriteAllLines(@"Average.dat", lines);



        }
    }// end AvgObserver class

    public class PercentObserver : Observer
    {
        const int ROW = 800;
        string time = "";
        public override void update(string[,] pd)
        {
            percentChange(pd);
        }

        public void percentChange(string[,] pd)
        {
            string[] lines = new string[100];
            int lineIndex = 0;

            for(int i = 0; i < ROW; i++)
            {

                if(pd[i,0] == null)
                {
                    continue;

                }else if(pd[i,0].Contains("Last updated"))
                {
                    time = pd[i, 0].Remove(0, 13);
                    Console.WriteLine();
                    Console.WriteLine("{0}", time);
                    Console.WriteLine();
                    lineIndex++;
                    lines[lineIndex] = time;
                    lineIndex++;
                    lineIndex++;

                } else if(Math.Abs( Convert.ToDouble(pd[i,4])) > 10 )
                {
                    string tick = pd[i,1];
                    string price = pd[i,2];
                    string change = pd[i,4];

                    Console.WriteLine("{0}, {1}, {2}", tick, price, change);
                    
                    lines[lineIndex] = tick +" " + price + " " + change;
                    lineIndex++;
                    
                }

            }

            System.IO.File.WriteAllLines(@"Change10.dat", lines);
        }
    }

    public class getInfoOberver : Observer
    {
        public override void update(string[,] pd)
        {
            getInfo(pd);   
        }

        public void getInfo(string [,] pd)
        {
            string[] companies = { "ALL ", "BA ", "BC ", "GBEL ", "KFT ", "MCD ", "TR ", "WAG " };
            const int ROW = 800;
            string[] lines = new string[100];
            int lineIndex = 0;


            for(int i = 0; i < ROW; i++)
            {
                if(pd[i,0]== null)
                {
                    continue;
                } else if(pd[i,0].Contains("Last updated"))
                {
                    string time = pd[i,0];
                    Console.WriteLine();
                    Console.WriteLine(time);
                    Console.WriteLine();

                    lineIndex++;
                    lines[lineIndex] = time;
                    lineIndex++;



                }else
                {
                    for(int j = 0; j < companies.Length; j++)
                    {
                        if(companies[j] == pd[i,1])
                        {
                            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7}", pd[i, 0], pd[i, 2], pd[i, 3], pd[i, 4], pd[i, 5], pd[i, 6], pd[i, 7], pd[i, 8]);
                            lines[lineIndex] = pd[i, 0] + " " + pd[i, 2] + " " + pd[i, 3] + " " + pd[i, 4] + " " + pd[i, 5] + " " + pd[i, 6] + " " + pd[i, 7] + " " + pd[i, 8];
                            lineIndex++;
                        }
                    }
                }
            }


            System.IO.File.WriteAllLines(@"Selections.dat", lines);

        }
    }












}// end namespace

