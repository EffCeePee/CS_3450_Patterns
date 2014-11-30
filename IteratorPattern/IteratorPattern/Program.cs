using System;
using System.Collections;
using System.IO;

namespace IteratorPattern
{
    class Program
    {
        private static void Main(string[] args) // client
        {
            string ints = "Ints.txt";
            string floats = "Floats.txt";
            string strings = "Strings.txt";
            string line;
            int count = 0;
            int choice = 0;
            MyArray _myArray = new MyArray();
            StreamReader _streamReader;
            IIterator I = _myArray.CreateIterator();
            IIterator F = new FilterIterator(I);

            Console.WriteLine("Press 1 to display  even integers (1).");
            Console.WriteLine("Press 2 to filter out even floating point numbers (2)");
            Console.WriteLine("Press 3 to see strings with numbers in them");
            Console.WriteLine();

            


            choice = Convert.ToInt32(Console.ReadLine());

            switch(choice)
            {
                case 1:
                    _streamReader = new StreamReader(ints);
                    while ((line = _streamReader.ReadLine()) != null)
                    {
                        _myArray[count] = line;
                    }
                    break;

                case 2:
                    _streamReader = new StreamReader(floats);
                    while ((line = _streamReader.ReadLine()) != null)
                    {
                        _myArray[count] = line;
                    }
                    break;

                case 3:
                    _streamReader = new StreamReader(strings);
                    while ((line = _streamReader.ReadLine()) != null)
                    {
                        _myArray[count] = line;
                    }
                    break;

                default:
                    Console.WriteLine("Please select 1 or 2");
                    Console.WriteLine();

                    break;
            }

            for (F.CurrentItem(); !F.IsDone(); F.Next())
            {
                Console.WriteLine(F.CurrentItem());
            }


            Console.ReadLine();

        }
    }

    public interface IIterator
    {
        object First();
        void Next();
        object CurrentItem();
        bool IsDone();
    }

    public class MyIterator : IIterator
    {
        private readonly MyArray _myIt;
        private int _location = 0;

        public MyIterator(MyArray myit)
        {
            this._myIt = myit;
        }
        public object First()
        {
            return _myIt[0];
        }

        public void Next()
        {
             if (_location < _myIt.Count)
                ++_location;
        }

        public bool IsDone()
        {
            if (_location >= _myIt.Count - 1 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object CurrentItem()
        {
            return _myIt[_location];
        }
    }

    public class FilterIterator : IIterator
    {
        private readonly IIterator _myIt;
        
        public FilterIterator(IIterator myit)
        {
            this._myIt = myit;
        }
        public object First()
        {
            return _myIt.First();
        }

        public void Next()
        {
             _myIt.Next();
        }

        public bool IsDone()
        {
            return _myIt.IsDone();
        }

        public object CurrentItem()
        {
            return _myIt.CurrentItem();
        }
    }
    

    interface IIterable
    {
        IIterator CreateIterator();
    }

    public class MyArray : IIterable
    {
        private readonly ArrayList _items = new ArrayList();

        public IIterator CreateIterator()
        {
            return new MyIterator(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }   
}



// 1. keeps track of the current element
// 2. what has already been traveresed
// 3. how many there are to be traversed
// 4. a link to the associated container 