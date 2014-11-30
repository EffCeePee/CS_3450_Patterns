using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueueImplementation
{

    class Program
    {

        static void Main(string[] args)
        {
            string test1 = "The implementation done with integers";
            string test2 = "the implementation done with characters";
            const int ONE = 1;
            const int TWO = 2;
            const int THREE = 3;
            const int FOUR = 4;
            const int FIVE = 5;

            const char SONE = '1';
            const char STWO = '2';
            const char STHREE = '3';
            const char SFOUR = '4';
            const char SFIVE = '5';



            int size = 0;
            int choice = 0;

            Queue<int> q = new Queue<int>();
            MyQueue<int> mq = new MyQueue<int>(ref q);
            List<int> lst = new List<int>();

            Queue<char> s = new Queue<char>();
            MyQueue<char> sq = new MyQueue<char>(ref s);
            List<char> slst = new List<char>();

                           
            mq.Add(ONE);
            mq.Add(TWO);
            lst.Add(THREE);
            mq.ChangeImpl(ref lst);
            mq.Add(FOUR);
            mq.Add(FIVE);

            Console.WriteLine("{0}", test1);

            size = mq.size();

            for (int i = 0; i < size; i++)
            {
                Console.Write("{0}", mq.Get());
                mq.Remove();
            }

            mq.Clear();
            Console.WriteLine();
            Console.WriteLine("{0}", test2);

            sq.Add(SONE);
            sq.Add(STWO);
            slst.Add(STHREE);
            sq.ChangeImpl(ref slst);
            sq.Add(SFOUR);
            sq.Add(SFIVE);

            size = sq.size();

            for (int i = 0; i < size; i++)
            {
                Console.Write("{0}", sq.Get());
                sq.Remove();
            }

            sq.Clear();

            Console.WriteLine();          
          Console.ReadLine();
           }// end main
        }
    }

    public class MyQueue<T>
    {
        private Queue<T> q;
        private IMyQueue<T> M;
        
        
        public MyQueue(ref Queue<T> I)
        {
            q = I;
            M = new QueQueue<T>();
            
        }

        public void impl(IMyQueue<T> imp)
        {
            this.M = imp;
        }


        public void Add(T t)
        {
            M.add(t);

        }

        public T Get()
        {
            return M.get();
        }

        public void Remove()
        {
            M.remove();
        }

        public int size()
        {
            return M.size();
        }

        public void Clear()
        {
            M.clear();
        }
        public void ChangeImpl(ref List<T> l)
        {
            l.Clear();
            int size = M.size();
            for (int i = 0; i < size; i++)
            {
                l.Add(M.get());
                M.remove();
            }
            M = new listQueue<T>();
            impl(M);

            for (int i = 0; i < size; i++)
            {
                M.add(l.First<T>());
                l.RemoveAt(0);
            }
            

        }

    }

    public interface IMyQueue<T>
    {
       
        // adds an element to the queue
        void add(T t);
        // gets the value of the first element in the queue
        T get();

        // removes the first element in the queue
        T remove();

        // retuns the size of the queue
        int size();

        // removes all element from the queue
        void clear();


    }

    public class listQueue<T> : IMyQueue<T>
    {

        public List<T> List;

          public listQueue()
        {
            this.List = new List<T>();
            
        }
        
        public void add(T t)
        {
            List.Add(t);
        }

        public T get()
        {
            return List.First();
        }

        public T remove()
        {
            var something  = List[0];
            List.RemoveAt(0);
            return something;
        }

        public int size()
        {
            return List.Count();
        }
        public void clear()
        {
            List.Clear();
        }

    }

    public class QueQueue<T> : IMyQueue<T>
    {
        public Queue<T> Que;
        
        
        public QueQueue()
        {

            this.Que = new Queue<T>();
        }

        public void add(T t)
        {
            Que.Enqueue(t);
        }

        public T get()
        {
            return Que.Peek();
        }


        public T remove()
        {
            
            return Que.Dequeue();
            
        }


        public int size()
        {
            return Que.Count();
        }

        public void clear()
        {
            Que.Clear();
        }

    }





