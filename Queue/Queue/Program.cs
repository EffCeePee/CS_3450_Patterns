using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }


public interface MyQueue<T>
{
    // adds an element to the queue
    public void add(T t);
    // gets the value of the first element in the queue
    public T get();

    // removes the first element in the queue
    public void remove();

    // retuns the size of the queue
    public int size();

    // removes all element from the queue
    public void clear();

    // changes from an array to a list or vice-versa
    public T changeImpl();
 }

public class listQueue<T>: MyQueue<T>
    {
        string pop = "The first element has been removed ";
    
        private List<T> testQueue; 
        private void add(T t)
        {
            testQueue.Add(t);
        }

        private T get()
        {
            return testQueue.First();
        }

        private void remove()
        {
            testQueue.RemoveAt(0);
            Console.WriteLine("{0}", pop);
        }

        private int size()
        {
            return testQueue.Count();
        }
        public void clear()
        {
            testQueue.Clear();
        }

        public T changeImpl()
        {
        
        }

    }

    public class changeQueue<T>: MyQueue<T>
    {
        private Queue<T> que;
        string pop = "The first element has been removed ";
               
        private void add(T t)
        {
            que.Enqueue(t);
        }

        private T get()
        {
            return que.Peek();
        }


        private void remove()
        {
            que.Dequeue();
            Console.WriteLine("{0}", pop);
        }


        private int size()
        {
            return que.Count();
        }

        private void clear()
        {
            que.Clear();
        }


        private T changeImpl()
        {

        }
    }


}


