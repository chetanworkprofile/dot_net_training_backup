//custom exception
/*
using System;

namespace Program
{
    public class EmployeeListNotFoundException : Exception
    {
        public EmployeeListNotFoundException()
        {
        }

        public EmployeeListNotFoundException(string message)
            : base(message)
        {
        }

        public EmployeeListNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class Employee
    {
        static int[] list = new int[] { 1, 2, 3, 4, 5 };
        int emp_id;

        public Employee(int id)
        {
            emp_id = id;
        }
        public void validate()
        {

            try
            {
                if (Array.IndexOf(list, this.emp_id) >= 0)
                {
                    Console.WriteLine("ok");
                }
                else
                {
                    throw new EmployeeListNotFoundException("Not valid");
                }
            }
            catch (EmployeeListNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public class program
    {
        public static void Main(string[] args)
        {
            Employee e1 = new Employee(1);
            Employee e2 = new Employee(6);
            e1.validate();
            e2.validate();
            Console.ReadKey();
        }
    }
}*/


//construstor injection
/*
using System;
interface IAccount
{
    void printDetails();
}
class SavingAccount : IAccount
{
    public void printDetails()
    {
        Console.WriteLine("saving acc");
    }
}
class CurrentAccount : IAccount
{
    public void printDetails()
    {
        Console.WriteLine("current acc");
    }
}
class Account
{
    private IAccount account;
    public Account(IAccount account)
    {
        this.account = account;
    }
    public void printAccount()
    {
        account.printDetails();
    }
}
class Program
{
    public static void Main()
    {
        IAccount ca = new CurrentAccount();
        Account a1 = new Account(ca);
        a1.printAccount();
        IAccount sa = new SavingAccount();
        Account a2 = new Account(sa);
        a2.printAccount();
    }
}*/


//property injection

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice1
{
    *//*internal class Program
    {
        static void Main(string[] args)
        {
        }
    }*//*

    public interface IAccount
    {
        void PrintDetails();
    }

    class CurrentAccount : IAccount
    {
        public void PrintDetails()
        {
            Console.WriteLine("Details of Current acc");
        }
    }
    class SavingAccount : IAccount
    {
        public void PrintDetails()
        {
            Console.WriteLine("Details of Savings account");
        }
    }

    class Account
    {
        public IAccount account { get; set; }

        public void PrintAcc()
        {
            account.PrintDetails();
        }
    }

    class program
    {
        static void Main(string[] args)
        {
            Account sa = new Account();

            sa.account = new SavingAccount();
            sa.PrintAcc();

            Console.ReadLine();
        }
    }

}
*/

//method injection

/*using System;

namespace practice1
{

    public interface IAccount
{
    void PrintDetails();
}

class CurrentAccount : IAccount
{
    public void PrintDetails()
    {
        Console.WriteLine("Details of Current acc");
    }
}
class SavingAccount : IAccount
{
    public void PrintDetails()
    {
        Console.WriteLine("Details of Savings account");
    }
}

class Account
{

    public void PrintAcc(IAccount account)
    {
        account.PrintDetails();
    }
}

class program
{
    static void Main(string[] args)
    {
        Account sa = new Account();

        sa.PrintAcc(new SavingAccount());

        Console.ReadLine();
    }
}

}*/

//singleton class

/*using System;

namespace practice1
{
    class program
    {
        static void Main(string[] args)
        {
            //singleton
            MySingleton mysing = MySingleton.Instance;
            mysing.DoSomething();
            Console.ReadKey();


        }
    }

    public class MySingleton
    {
        private static MySingleton instance = null;
        private MySingleton() { }

        public static MySingleton Instance
        {
            get { 
                if(instance == null)
                {
                    instance = new MySingleton();
                }
                return instance;
            }
        }
        public void DoSomething()
        {
            Console.WriteLine("singleton class called");
        }
    }
}*/

//file io writing bits
/*using System;
using System.IO;

public class practice1
{
    public static void Main(string[] args)
    {
        FileStream f = new FileStream("./b.txt",FileMode.OpenOrCreate);
        //f.WriteByte(65);
        for (int i = 65; i <= 90; i++)
        {
            f.WriteByte((byte)i);
        }
        int j = 0;
        f.Close();

        f = new FileStream("./b.txt", FileMode.OpenOrCreate);
        Console.WriteLine("output: ");
        while ((j = f.ReadByte()) != -1)
        {
            Console.Write((char)j);
        }
        Console.ReadKey();
        f.Close();
    }
}*/

//file stream writer

/*using System;
using System.IO;

public class practice1
{
    public static void Main(string[] args)
    {
        FileStream f = new FileStream("./b.txt", FileMode.OpenOrCreate);
        //f.WriteByte(65);
        StreamWriter s = new StreamWriter(f);
        StreamReader sr = new StreamReader(f);
        s.WriteLine("hello b.txt file ");
        s.WriteLine("i am writing from c# file");

        string line = "";
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }
        s.Close();
        sr.Close();
        Console.ReadKey();
        f.Close();
    }
}*/

//throwing in finally
/*
using System;

public class practice1
{
    public static void Main(string[] args)
    {
        Console.WriteLine("start of program");
        try
        {
            int a = 1;
            int b = 2;
            a = a * b;
            Console.WriteLine("in try block");
            Console.WriteLine(a);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            int c = 0;
            int a = 10 / c;
            string abc = Console.ReadLine();
            Console.WriteLine(abc + " user input displayed in finally");
        }
    }
}*/

// textwriter and textreader

/*using System;
using System.IO;

public class practice1
{
    public static void Main(string[] args)
    {
        using (TextWriter writer = File.CreateText("./b.txt"))
        {
            writer.WriteLine("hello c#");
            writer.WriteLine("file handling using textwriter");
        }
        Console.WriteLine("done");

        using (TextReader reader = File.OpenText("./b.txt"))
        {
            Console.WriteLine(reader.ReadToEnd());
        }
        Console.ReadKey();
    }
}*/

// binary writer
/*
using System;
using System.IO;

public class practice1
{
    public static void Main(string[] args)
    {
        using (BinaryWriter wr = new BinaryWriter(File.Open("./b.txt", FileMode.Create)))
        {
            wr.Write(12.5);
            wr.Write("this is string");
            wr.Write(true);
            wr.Dispose();
        }
        using (BinaryReader rd = new BinaryReader(File.Open("./b.txt", FileMode.Open)))
        {
            Console.WriteLine(rd.ReadDouble());
            Console.WriteLine(rd.ReadString());
            Console.WriteLine(rd.ReadBoolean());
            rd.Dispose();
        }
        Console.ReadKey();
    }
}*/

//string writer/reader
/*
using System;
using System.IO;
using System.Text;

public class practice1
{
    public static void Main(string[] args)
    {
        string text = "Hello, Welcome to this c# program \n" + "it is nice ";
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);

        sw.WriteLine(text);
        sw.Flush();
        sw.Close();

        StringReader sr = new StringReader(sb.ToString());

        while(sr.Peek() != -1)
        {
            Console.WriteLine(sr.ReadLine());
        }
        Console.ReadKey();
    }
}*/
/*

using System;
using System.IO;

namespace name1
{
    public class practice1
    {
        public static void Main(string[] args)
        {
            try
            {*//*
                FileInfo file = new FileInfo("./bc.txt");
                *//*file.Create();
                Console.WriteLine("file created "+ file.Name);
                *//*
                StreamWriter sw = file.CreateText();
                sw.WriteLine("hello there i am writing from a c-sharp program ");
                sw.Close();

                StreamReader sr = file.OpenText();
                string data = "";
                while ((data = sr.ReadLine()) != null)
                {
                    Console.WriteLine(data);
                }
                sr.Close();
                Console.ReadKey();*//*

                DirectoryInfo directory = new DirectoryInfo("./dir");
                if (directory.Exists)
                {
                    Console.WriteLine("Directory already exist.");
                    return;
                }
                // Creating a new directory.  
                directory.Create();
                Console.WriteLine("The directory is created successfully.");

            }
            catch(Exception ex) { Console.WriteLine(ex); }
        }
    }
}*/


// 6feb
/*
using System;
using System.Collections.Generic;

namespace Program
{
    public class Abc
    {
        public static void Main()
        {
            var names = new SortedSet<string>() { "abc", "hello", "hi", "hello", "done" };
            foreach(string i in names)
            {
                Console.WriteLine(i);
            }
            
        }
    }
}*/

/*using System;
using System.Collections;
using System.Collections.Generic;

namespace Abc
{
    class Program
    {
        public static void Main()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(5);
            list.AddLast(8);

            foreach(int i in list)
            {
                Console.WriteLine(i);
            }
            LinkedListNode<int> node = list.Find(5);
            list.AddBefore(node, 4);
            node = node.Next;
            list.Remove(node);
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
        }
    }
}*/


/*using System;
namespace Abc
{
    public class PrintMsg<T> { 
        public static T val(T arg1)
        {
            Console.WriteLine(arg1);
            Console.WriteLine(typeof(T));
            return arg1;
        }

    }

    public class Program
    {
        public static void Main()
        {
            PrintMsg<int>.val(10);
            PrintMsg<string>.val("hello");
        }
    }
}*/

//delegates
/*using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Abc
{
    internal class Program
    {

        public delegate int Del(int a, int b);      //delegate signature
        //private static Action<int, Del> a1;
        private static Del d;
        private static Func<Del,int> f;        //takes int and returns Del
        public static void Main()
        {
            //d = Add;            //field initialized with function of similar signature
            //d += new Del(Multiply);         //multicast delegate
            //Console.WriteLine(d(10,20));
            //d(10, 20);

            //using anonymous function
            *//*d = delegate (int a, int b)
            {
                Console.WriteLine("hello from anonymous function" + (a+b));
                return a + b;
            };

            d(10, 20);*//*

            //using lambda function
            d += (int a, int b) =>
            {
                Console.WriteLine("lambda functions says: " + (a * b));
                return a * b;
            };
           // d(60, 12);
            *//*a1 = (int i, Del d) =>
            {
                Console.WriteLine("action:" + i);
                d(i, 120);
            };
            a1(12, d);*//*
            f = (d) =>
            {
                Console.WriteLine("in func" + d(12,12));
                return d(12,12);
            };
            f(d);
        }


*//*
        public static int Add(int a,int b)
        {
            Console.WriteLine(a + b);
            return a + b;
        }
        public static int Multiply(int a,int b)
        {
            Console.WriteLine(a * b);
            return a * b;
        }*//*
    }
}
*/

//c# reflections
/*using System;
using System.Reflection;
public class ReflectionExample
{
    public static void Main()
    {
        Type t = typeof(System.Int32);
        Console.WriteLine(t.Assembly);
        Console.WriteLine(t.FullName);
        Console.WriteLine(t.BaseType);
        Console.WriteLine(t.IsClass);
        Console.WriteLine(t.IsEnum);
        Console.WriteLine(t.IsInterface);
    }
}*/

//mulitihreading introduction
/*
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Thread workerThread = new Thread(DoWork);
        workerThread.Start();

        Console.WriteLine("Main thread doing other work.");
        Thread.Sleep(10);
        Console.WriteLine("Main thread finished.");
        //Console.ReadLine();
    }

    static void DoWork()
    {
        Console.WriteLine("Worker thread starting.");
        Thread.Sleep(500);
        Console.WriteLine("Worker thread finished.");
    }
}
*/

//Multithreading example
//in this we use ParameterizedThreadStart delegate to pass object argument to method2 which is conveted to int
//in this comes the concept of boxing and unboxing
/*using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Abc
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Main thread started");
            Thread t1 = new Thread(Method1)
            {
                Name = "Thread1"
            };
            Thread t2 = new Thread(new ParameterizedThreadStart(Method2));
            Thread t3 = new Thread(Method3);
            t1.Start();
            t2.Start(1);
            t3.Start();
            t3.Join();
            Console.WriteLine("main thread ended");
        }
        static void Method1()
        {
            Console.WriteLine("Method1 started");

            Thread.Sleep(4000);
            Console.WriteLine("Method1 ended");
        }
        static void Method2(object a)
        {
            Console.WriteLine("Method2 started");
            int b = Convert.ToInt32(a) * 1000;
            Thread.Sleep(b);
            Console.WriteLine("Method2 ended");
        }
        static void Method3()
        {
            Console.WriteLine("3 started");
            Thread.Sleep(5000);
            Console.WriteLine("3 ended");
        }
    }
}*/


//type safe passing of arguments in multiple threads
/*using System.Threading;
using System;
namespace ThreadingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int Max = 10;
            NumberHelper obj = new NumberHelper(Max);

            Thread T1 = new Thread(new ThreadStart(obj.DisplayNumbers));

            T1.Start();
            Console.Read();
        }
    }
    public class NumberHelper
    {
        int _Number;

        public NumberHelper(int Number)
        {
            _Number = Number;
        }

        public void DisplayNumbers()
        {
            for (int i = 1; i <= _Number; i++)
            {
                Console.WriteLine("value : " + i);
            }
        }
    }
}*/

//returning value from thread using callback
/*
using System;
using System.Threading;

namespace ThreadingDemo
{
    // First Create the callback delegate with the same signature of the callback method.
    public delegate void ResultCallbackDelegate(int Results);

    //Creating the Helper class
    public class NumberHelper
    {
        //Creating two private variables to hold the Number and ResultCallback instance
        private int _Number;
        private ResultCallbackDelegate _resultCallbackDelegate;

        //Initializing the private variables through constructor
        //So while creating the instance you need to pass the value for Number and callback delegate
        public NumberHelper(int Number, ResultCallbackDelegate resultCallbackDelagate)
        {
            _Number = Number;
            _resultCallbackDelegate = resultCallbackDelagate;
        }

        //This is the Thread function which will calculate the sum of the numbers
        public void CalculateSum()
        {
            int Result = 0;
            for (int i = 1; i <= _Number; i++)
            {
                Result = Result + i;
            }

            //Before the end of the thread function call the callback method
            if (_resultCallbackDelegate != null)
            {
                _resultCallbackDelegate(Result);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Create the ResultCallbackDelegate instance and to its constructor 
            //pass the callback method name
            ResultCallbackDelegate resultCallbackDelegate = new ResultCallbackDelegate(ResultCallBackMethod);
            int Number = 10;
            //Creating the instance of NumberHelper class by passing the Number
            //the callback delegate instance
            NumberHelper obj = new NumberHelper(Number, resultCallbackDelegate);
            //Creating the Thread using ThreadStart delegate
            Thread T1 = new Thread(new ThreadStart(obj.CalculateSum));

            T1.Start();
            Console.Read();
        }
        //Callback method and the signature should be the same as the callback delegate signature
        public static void ResultCallBackMethod(int Result)
        {
            Console.WriteLine("The Result is " + Result);
        }
    }
}*/

// join overloaded method
/*
using System.Threading;
using System;
namespace ThreadingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread Started");

            //Main Thread creating three child threads
            Thread thread1 = new Thread(Method1);
            Thread thread2 = new Thread(Method2);
            Thread thread3 = new Thread(Method3);

            thread1.Start();
            thread2.Start();
            thread3.Start();

            //Now, Main Thread will block for 3 seconds and wait thread2 to complete its execution
            if (thread2.Join(TimeSpan.FromSeconds(3)))
            {
                Console.WriteLine("Thread 2 Execution Completed in 3 second");
            }
            else
            {
                Console.WriteLine("Thread 2 Execution Not Completed in 3 second");
            }

            //Now, Main Thread will block for 3 seconds and wait thread3 to complete its execution
            if (thread3.Join(3000))
            {
                Console.WriteLine("Thread 3 Execution Completed in 3 second");
            }
            else
            {
                Console.WriteLine("Thread 3 Execution Not Completed in 3 second");
            }

            Console.WriteLine("Main Thread Ended");
            Console.Read();
        }

        static void Method1()
        {
            Console.WriteLine("Method1 - Thread1 Started");
            Thread.Sleep(1000);
            Console.WriteLine("Method1 - Thread 1 Ended");
        }

        static void Method2()
        {
            Console.WriteLine("Method2 - Thread2 Started");
            Thread.Sleep(2000);
            Console.WriteLine("Method2 - Thread2 Ended");
        }

        static void Method3()
        {
            Console.WriteLine("Method3 - Thread3 Started");
            Thread.Sleep(5000);
            Console.WriteLine("Method3 - Thread3 Ended");
        }
    }
}*/


//thread synchronization using locking objects
/*using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static object lockObject = new object();
        static ManualResetEvent _mre = new ManualResetEvent(false);
        static AutoResetEvent _are = new AutoResetEvent(true);
        static void Main()
        {
            Thread thread1 = new Thread(SomeMethod)
            {
                Name = "Thread 1"
            };

            Thread thread2 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };

            Thread thread3 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };

            thread1.Start();
            thread2.Start();
            thread3.Start();

            //Console.ReadKey();
        }

        public static void SomeMethod()
        {
            // Locking the Shared Resource for Thread Synchronization
            *//* lock (lockObject)
             {
                 Console.Write("[Welcome To The ");
                 Thread.Sleep(1000);
                 Console.WriteLine("World of Dotnet!]");
             }*//*

            // using the monitor class
            *//*Monitor.Enter(lockObject);                  //start /enter the monitor class
            Console.Write("[Welcome To The ");
            Thread.Sleep(1000);
            Console.WriteLine("World of Dotnet!]");
            Monitor.Exit(lockObject);                   //end the monitor *//*
            // we have the flexibility of moving the code can start monitor in try and end in finally
            *//*try
            {
                Monitor.Enter(lockObject);                  //start /enter the monitor class
                Console.Write("[Welcome To The ");
                Thread.Sleep(1000);
                Console.WriteLine("World of Dotnet!]");
                throw new Exception();
               
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);                
            }
            finally
            {
                Monitor.Exit(lockObject);
            }*//*

            //manual reset to stop simultaneous read write
            // _mre.Reset()   .Set()   .WaitOne()

            //autoreset event
            // _are.WaitOne .Set()      in this bu deafult we have true state and once a thread starts it
            // become false with WaitOne() then after completion we have to make it .Set() for other threads to proceed
            
        }

        public static void Write()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            _mre.Reset();
            Thread.Sleep(3000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing Completed...");
            _mre.Set();
        }
        public static void Read()
        {
            _mre.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reading...");
            
            Thread.Sleep(3000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reading Completed...");
        }
    }
}*/

//using mutex
/*
using System;
using System.Threading;

namespace Abc
{
    class Program
    {
        static Mutex _mutex = new Mutex();
        static void Main()
        {
            //writer threads
            for(int i=0;i<5;i++)
            {
                new Thread(Write).Start();
            }
        }

        public static void Write()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting...");
            _mutex.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            Thread.Sleep( 4000 );
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} Ending...");
            _mutex.ReleaseMutex();
        }
    }
}

*/

/*
using System;
using System.Threading;

namespace Abc
{
    class Program
    {
        static Semaphore _semaphore = new Semaphore(1, 1);      //(initial count, max count)
        static void Main()
        {
            //writer threads
            for (int i = 0; i < 5; i++)
            {
                new Thread(Write).Start();
            }
        }

        public static void Write()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting...");
            _semaphore.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
            Thread.Sleep(4000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} Ending...");
            _semaphore.Release();
        }
    }
}

*/

//semaphoreslim example from dotnettutorials
/*

using System;
using System.Threading;
using System.Threading.Tasks;

public class Example
{
    // Create the semaphore.
    private static SemaphoreSlim semaphore = new SemaphoreSlim(0, 3);

    // A padding interval to make the output more orderly.
    private static int padding;

    public static void Main()
    {
        Console.WriteLine($"{semaphore.CurrentCount} tasks can enter the semaphore");
        Task[] tasks = new Task[5];

        // Create and start five numbered tasks.
        for (int i = 0; i <= 4; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                // Each task begins by requesting the semaphore.
                Console.WriteLine($"Task {Task.CurrentId} begins and waits for the semaphore");

                int semaphoreCount;
                semaphore.Wait();
                try
                {
                    Interlocked.Add(ref padding, 100);
                    Console.WriteLine($"Task {Task.CurrentId} enters the semaphore");
                    // The task just sleeps for 1+ seconds.
                    Thread.Sleep(1000 + padding);
                }
                finally
                {
                    semaphoreCount = semaphore.Release();
                }
                Console.WriteLine($"Task {Task.CurrentId} releases the semaphore; previous count: {semaphoreCount}");
            });
        }

        // Wait for one second, to allow all the tasks to start and block.
        Thread.Sleep(1000);

        // Restore the semaphore count to its maximum value.
        Console.Write("Main thread calls Release(3) --> ");
        semaphore.Release(3);
        Console.WriteLine($"{semaphore.CurrentCount} tasks can enter the semaphore");
        // Main thread waits for the tasks to complete.
        Task.WaitAll(tasks);

        Console.WriteLine("Main thread Exits");
        Console.ReadKey();
    }
}*/

/*using System;
namespace Abc
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Environment.ProcessorCount);
        }
    }
}*/

//thread pool
/*using System;
using System.Threading;

namespace ThreadPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(MyMethod));
            }
            //Console.Read();
        }

        public static void MyMethod(object obj)
        {
            Thread thread = Thread.CurrentThread;
            string message = $"Background: {thread.IsBackground}, Thread Pool: {thread.IsThreadPoolThread}, Thread ID: {thread.ManagedThreadId}";
            Console.WriteLine(message);
        }
    }
}*/

//background threads
/*using System;
using System.Threading;
namespace MultithreadingDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // A thread created here to run Method1 Parallely
            Thread thread1 = new Thread(Method1)
            {
                //Thread becomes background thread
                IsBackground = true
            };

            Console.WriteLine($"Thread1 is a Background thread:  {thread1.IsBackground}");
            thread1.Start();
            //The control will come here and will exit 
            //the main thread or main application
            Console.WriteLine("Main Thread Exited");
            //As the Main thread (i.e. foreground thread exits the application)
            //Automatically, the background thread quits the application
        }
        // Static method
        static void Method1()
        {
            Console.WriteLine("Method1 Started");
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine("Method1 is in Progress!!");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Method1 Exited");
            Console.WriteLine("Press any key to Exit.");
            //Console.ReadKey();
        }
    }
}*/


//thread lifecycle
/*using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Creating and initializing threads Unstarted state
                Thread thread1 = new Thread(SomeMethod);
                Console.WriteLine($"Before Start, IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");

                // Running State
                thread1.Start();
                Console.WriteLine($"After Start(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");

                // thread1 is in suspended state
                thread1.Suspend();
                Console.WriteLine($"After Suspend(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");

                // thread1 is resume to running state
                thread1.Resume();
                Console.WriteLine($"After Resume(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");

                // thread1 is in Abort state
                //In this case, it will start the termination, IsAlive still gives you as true
                thread1.Abort();
                Console.WriteLine($"After Abort(), IsAlive: {thread1.IsAlive}, ThreadState: {thread1.ThreadState}");

                //Calling the Start Method on a dead thread will result an Exception
                thread1.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }

            Console.ReadKey();
        }

        public static void SomeMethod()
        {
            for (int x = 0; x < 3; x++)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("SomeMethod Completed...");
        }
    }
}*/

//thread priority
/*using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(SomeMethod)
            {
                Name = "Thread 1"
            };
            //Setting the thread Priority as Normal
            thread1.Priority = ThreadPriority.Normal;

            Thread thread2 = new Thread(SomeMethod)
            {
                Name = "Thread 2"
            };
            //Setting the thread Priority as Lowest
            thread2.Priority = ThreadPriority.Lowest;

            Thread thread3 = new Thread(SomeMethod)
            {
                Name = "Thread 3"
            };
            //Setting the thread Priority as Highest
            thread3.Priority = ThreadPriority.Highest;

            //Getting the thread Prioroty
            Console.WriteLine($"Thread 1 Priority: {thread1.Priority}");
            Console.WriteLine($"Thread 2 Priority: {thread2.Priority}");
            Console.WriteLine($"Thread 3 Priority: {thread3.Priority}");

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Console.ReadKey();
        }

        public static void SomeMethod()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name} Printing {i}");
            }
        }
    }
}*/

//interthread communication
/*using System;
using System.Threading;

namespace InterthreadCommunications
{
    class Program
    {
        //Limit numbers will be printed on the Console
        const int numberLimit = 10;

        static readonly object _lockObject = new object();

        static void Main(string[] args)
        {
            Thread EvenThread = new Thread(PrintEvenNumbers);
            Thread OddThread = new Thread(PrintOddNumbers);

            //First Start the Even thread.
            EvenThread.Start();

            //Pause for 10 ms, to make sure Even thread has started 
            //or else Odd thread may start first resulting different sequence.
            Thread.Sleep(100);

            //Next, Start the Odd thread.
            OddThread.Start();

            //Wait for all the childs threads to complete
            OddThread.Join();
            EvenThread.Join();

            Console.ReadKey();
        }

        //Printing of Even Numbers Function
        static void PrintEvenNumbers()
        {
            try
            {
                //Implement lock as the Console is shared between two threads
                Monitor.Enter(_lockObject);
                for (int i = 0; i <= numberLimit; i = i + 2)
                {
                    //Printing Even Number on Console)
                    Console.Write($"{i} ");

                    //Notify Odd thread that I'm done, you do your job
                    Monitor.Pulse(_lockObject);

                    //I will wait here till Odd thread notify me 
                    // Monitor.Wait(monitor);
                    //Without this logic application will wait forever

                    bool isLast = false;
                    if (i == numberLimit)
                    {
                        isLast = true;
                    }

                    if (!isLast)
                    {
                        //I will wait here till Odd thread notify me
                        Monitor.Wait(_lockObject);
                    }
                }
            }
            finally
            {
                //Release the lock
                Monitor.Exit(_lockObject);
            }
        }

        //Printing of Odd Numbers Function
        static void PrintOddNumbers()
        {
            try
            {
                //Hold lock as the Console is shared between two threads
                Monitor.Enter(_lockObject);
                for (int i = 1; i <= numberLimit; i = i + 2)
                {
                    //Printing the odd numbers on the console
                    Console.Write($"{i} ");

                    //Notify Even thread that I'm done, you do your job
                    Monitor.PulseAll(_lockObject);

                    //I will wait here till even thread notify me
                    // Monitor.Wait(monitor);
                    // without this logic application will wait forever

                    bool isLast = false;
                    if (i == numberLimit - 1)
                    {
                        isLast = true;
                    }

                    if (!isLast)
                    {
                        //I will wait here till Even thread notify me
                        Monitor.Wait(_lockObject);
                    }
                }
            }
            finally
            {
                //Release lock
                Monitor.Exit(_lockObject);
            }
        }
    }
}*/


//async await
/*using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

        public async static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

            //Thread.Sleep(TimeSpan.FromSeconds(10));
            await Task.Delay(TimeSpan.FromSeconds(4));
            Console.WriteLine("\n");
            Console.WriteLine("Some Method End");
        }
    }
}*/
/*using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

        public async static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");
            //await Wait();
            Wait();

            Console.WriteLine("Some Method End");
        }

        private static async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            Console.WriteLine("\n4 Seconds wait Completed\n");
        }
    }
}*/

//executing multile tasks
/*using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main Thread Started");

            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(10);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");

            ProcessCreditCards(creditCards);

            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new List<Task<string>>();

            //Processing the creditCards using foreach loop
            *//* foreach (var creditCard in creditCards)
             {
                 var response = ProcessCard(creditCard);
                 tasks.Add(response);
             }
             await Task.WhenAll(tasks);
             *//*

            await Task.Run(() =>
            {
                foreach (var creditCard in creditCards)
                {
                    var response = ProcessCard(creditCard);
                    tasks.Add(response);
                }
            });

            //It will execute all the tasks concurrently
           
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
            //foreach(var item in tasks)
            //{
            //    Console.WriteLine(item.Result);
            //}
        }

        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            //Here we can do any API Call to Process the Credit Card
            //But for simplicity we are just delaying the execution for 1 second
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            Console.WriteLine($"Credit Card Number: {creditCard.CardNumber} Processed");
            return message;
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }

        public static List<CreditCard> GenerateCreditCards(int number)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < number; i++)
            {
                CreditCard card = new CreditCard()
                {
                    CardNumber = "10000000" + i,
                    Name = "CreditCard-" + i
                };

                creditCards.Add(card);
            }

            return creditCards;
        }
    }
}*/

//parallel processing
/*using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgrammingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Getting the Number of Processor count
            int processorCount = Environment.ProcessorCount;

            Console.WriteLine($"Processor Count on this Machine: {processorCount}\n");

            //Limiting the maximum degree of parallelism to processorCount - 1
            var options = new ParallelOptions()
            {
                //You can hard code the value as follows
                //MaxDegreeOfParallelism = 7
                //But better to use the following statement
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1
            };

            Parallel.For(1, 11, options, i =>
            {
                Thread.Sleep(500);
                Console.WriteLine($"Value of i = {i}, Thread = {Thread.CurrentThread.ManagedThreadId}");
            });

            //Console.ReadLine();
        }
    }
}*/

//cancelation token in parallel programming
/*
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgrammingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an Instance of CancellationTokenSource
            var CTS = new CancellationTokenSource();

            //Set when the token is going to cancel the parallel execution
            CTS.CancelAfter(TimeSpan.FromSeconds(5));

            //Create an instance of ParallelOptions class
            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 2,
                //Set the CancellationToken value
                CancellationToken = CTS.Token
            };

            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //Passing ParallelOptions as the first parameter
                Parallel.Invoke(
                        parallelOptions,
                        () => DoSomeTask(1),
                        () => DoSomeTask(2),
                        () => DoSomeTask(3),
                        () => DoSomeTask(4),
                        () => DoSomeTask(5),
                        () => DoSomeTask(6),
                        () => DoSomeTask(7)
                    );
                stopwatch.Stop();
                Console.WriteLine($"Time Taken to Execute all the Methods : {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
            }
            //When the token cancelled, it will throw an exception
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Finally dispose the CancellationTokenSource and set its value to null
                CTS.Dispose();
                CTS = null;
            }
            Console.ReadLine();
        }

        static void DoSomeTask(int number)
        {
            Console.WriteLine($"DoSomeTask {number} started by Thread {Thread.CurrentThread.ManagedThreadId}");
            //Sleep for 2 seconds
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine($"DoSomeTask {number} completed by Thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}*/

//interlocked 
/*using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgrammingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var ValueInterlocked = 0;
            Parallel.For(0, 100000, _ =>
            {
                //Incrementing the value
                Interlocked.Increment(ref ValueInterlocked);
            });
            Console.WriteLine("Expected Result: 100000");
            Console.WriteLine($"Actual Result: {ValueInterlocked}");
            Console.ReadKey();
        }
    }
}*/