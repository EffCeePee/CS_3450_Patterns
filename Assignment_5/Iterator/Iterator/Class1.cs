using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public interface IIterator<T>
    {
        void First();
        void Next();
        bool IsDone();
        T Current();
    }


    public interface IIterable<T>
    {
        IIterator<T> GetIterator();
    }

    interface ISequence<T>
    {
        void Add(T value);
        int Size();
        int Capacity();
        T Get(int i);
    }

    interface IIterableSequence<T> : IIterable<T>, ISequence<T>
    {
    }

    public class MyArray<T> : IIterableSequence<T>
    {
        public IIterator<T> GetIterator()
        {
            return null;
        }

        public void Add(T value)
        {
        }

        public int Size()
        {
            return 0;
        }

        public int Capacity()
        {
            return 0;
        }

        public T Get(int i)
        {
            return default(T);
        }
    }

    public class MyIterator<T> : IIterator<T>
    {
        MyArray<T> _myArray = new MyArray<T>();
        private int _location = 0;
 
        public void First()
        {
            
        }

        public void Next()
        {
        }

        public bool IsDone()
        {
            return false;
        }

        public T Current()
        {
            return default(T);
        }
    }


    public class MyFilterIterator<T> : IIterator<T>
    {
        public void First()
        {
        }

        public void Next()
        {
        }

        public bool IsDone()
        {
            return false;
        }

        public T Current()
        {
            return default(T);
        }
    }

}
