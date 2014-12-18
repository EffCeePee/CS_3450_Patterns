using System.Collections;
using System.Runtime.InteropServices;

namespace Iterator
{
    class Program
    {
        private static void Main(string[] args) // client
        {
           
        }
    }

    public interface IIterator
    {
        object First();
        object Next();
        object CurrentItem();
        bool IsDone();
    }

    public class MyIterator : IIterator
    {
        private readonly ConcreteAggregate _conAgg;
        private int _location = 0;

        public MyIterator(ConcreteAggregate conAgg)
        {
            this._conAgg = conAgg;
        }
        public object First()
        {
            return _conAgg[0];
        }

        public object Next()
        {
            object ret = null;

            if (_location < _conAgg.Count - 1)
            {

                ret = _conAgg[++_location];

            }

            return ret;
        }

        public bool IsDone()
        {
            return _location >= _conAgg.Count;
        }

        public object CurrentItem()
        {

            return _conAgg[_location];
        }
    }

    interface Iterable
    {
         IIterator CreateIterator();
    }

    public class ConcreteAggregate : Iterable
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