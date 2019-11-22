using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoNet基础.多线程
{
    #region 多线程学习
    /// <summary>
    /// 多线程学习
    /// </summary>
    public class 多线程学习
    {
        public void ThreadTest()
        {
            //方式1
            Thread workerThread = new Thread(StartThread);
            Console.WriteLine("主线程的Id： {0}", Thread.CurrentThread.ManagedThreadId.ToString());
            workerThread.Start();

            //方式2
            Thread t = new Thread(new ThreadStart(StartThread));
            t.Start();

            //方式3
            Thread workerThread2 = new Thread(ParameterizedStartThread);
            workerThread2.Start(2);
            Console.ReadLine();

            //方式4
            Thread workerThread3 = new Thread(new ParameterizedThreadStart(ParameterizedStartThread));
            workerThread3.Start(3);
            Console.ReadLine();

            //方式5
            MethodInvoker mi = new MethodInvoker(StartThread);
            for (int i = 0; i < 100; i++)
            {
                mi.BeginInvoke(null,null);//只有windowForm窗口能用
                Thread.Sleep(100);
            } 


            ThreadPool.SetMaxThreads(20,20);

        }

        public static void StartThread()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Thread value {0} running on Thread Id {1}", i.ToString(), Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }

        public static void ParameterizedStartThread(object value)
        {
            Thread.Sleep(25);
            Console.WriteLine("Thread passed value {0} running on Thread Id {1}。哈啊哈", value.ToString(), Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }
    #endregion

    #region Join学习
    /// <summary>
    /// Join学习
    /// 阻塞
    /// </summary>
    public class Join学习
    {
        public static Thread T1;
        public static Thread T2;

        public void ThreadTest()
        {
            T1 = new Thread(new ThreadStart(First));
            T2 = new Thread(new ThreadStart(Second));
            T1.Name = "T1";
            T2.Name = "T2";
            T1.Start();
            T2.Start();
            Console.ReadLine();
        }

        private static void First()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Ti线程：T1 state [{0}], T1 showing {1}", T1.ThreadState, i.ToString());
            }
        }

        private static void Second()
        {
            Console.WriteLine("Ti调用Join之前.T2 state [{0}], T1 state [{1}], CurrentThreadName={2}",T2.ThreadState, T1.ThreadState,Thread.CurrentThread.Name);

            //T2被阻塞，T1执行完了再执行T2
            T1.Join();

            Console.WriteLine(
                "Ti调用Join后 T2 state [{0}] , T1 state [{1}], CurrentThreadName={2}",
                T2.ThreadState, T1.ThreadState,
                Thread.CurrentThread.Name);

            for (int i = 10; i < 20; i++)
            {
                Console.WriteLine(
                    "循环 T2 state [{0}], T1 state [{1}], CurrentThreadName={2} showing {3}",
                    T2.ThreadState, T1.ThreadState,
                    Thread.CurrentThread.Name, i.ToString());
            }

            Console.WriteLine(
                "最后 T2 state [{0}], T1 state [{1}], CurrentThreadName={2}",
                T2.ThreadState, T1.ThreadState,
                Thread.CurrentThread.Name);
        }
    }
    #endregion

    #region Sleep学习
    /// <summary>
    /// Sleep学习
    /// 来自不同的类分别是Thread和Object
    /// sleep不释放资源，wait释放资源
    /// </summary>
    public class Sleep学习
    {
        public static Thread A;
        public static Thread B;
        public void ThreadTest()
        {
            A = new Thread(new ThreadStart(Count1));
            B = new Thread(new ThreadStart(Count2));
            A.Start();
            B.Start();
            Console.ReadLine();
        }

        private static void Count1()
        {
            Console.WriteLine(" A 开始");
            for (int i = 1; i <= 100; i++)
            {
                Console.Write("A:" + i + " ");
                if (i == 10)
                    Thread.Sleep(2000);
            }
            Console.WriteLine(" A 结束");
        }

        private static void Count2()
        {
            Console.WriteLine(" B 开始");
            for (int i = 1; i <= 100; i++)
            {
                Console.Write("B:" + i + " ");
                if (i == 60)
                    Thread.Sleep(5000);
            }
            Console.WriteLine(" B 结束");
        }
    }
    #endregion

    #region Interrupt中断学习
    /// <summary>
    /// Interrupt中断学习
    /// Interrupt：如果终止工作线程，只能管到一次，工作线程的下一次sleep就管不到了，相当于一个contine操作。
    /// Abort：这个就是相当于一个break操作，工作线程彻底死掉。
    /// </summary>
    public class Interrupt中断学习
    {
        public static Thread sleeper;
        public static Thread waker;

        public void ThreadTest()
        {
            Console.WriteLine("主函数开始");
            sleeper = new Thread(new ThreadStart(PutThreadToSleep));
            waker = new Thread(new ThreadStart(WakeThread));
            sleeper.Start();
            waker.Start();
            Console.ReadLine();
        }
        
        private static void PutThreadToSleep()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write(" A:" + i);
                if (i == 10 || i == 20 || i == 30)
                {
                    try
                    {
                        Console.WriteLine("PutThreadToSleep线程在{0}时，设置睡眠20毫秒", i.ToString());
                        //在这里设置了睡眠，时间到了会唤醒，但是有别人把它Interrupt中断了，就会报异常。
                        Thread.Sleep(20);
                        
                    }
                    catch (ThreadInterruptedException e)
                    {
                        Console.WriteLine("Forcibly ，PutThreadToSleep函数出现异常");
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("woken");
                }
            }
        }

        //thread waker threadStart
        private static void WakeThread()
        {
            try
            {
                for (int i = 51; i < 100; i++)
                {
                    Console.Write(" B:" + i);

                    if (sleeper.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        Console.WriteLine("PutThreadToSleep 已经阻塞");
                        //中断处于阻塞的线程，会导致异常
                        sleeper.Interrupt();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("WakeThread函数出现异常");
                throw ex;
            }
        }
    }
    #endregion

    #region 线程池

    public class 线程池
    {
        public void Main()
        {
            //把线程池的最大值设置为1000
            ThreadPool.SetMaxThreads(50, 20);
            ThreadPool.SetMinThreads(20, 2);
            ThreadMessage("Start");
            ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncCallback), "Hello Elva");
            Console.ReadKey();
        }
        static void AsyncCallback(object state)
        {
            Thread.Sleep(200);
            ThreadMessage("AsyncCallback");
            string data = (string)state;
            Console.WriteLine("Async thread do work!\n" + data);
        }

        //显示线程现状
        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  CurrentThreadId is {1}", data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }
    }


    //可以在RegisterWaitForSingleObject溶于信号量的概念
    public class RegisterWaitForSingleObject测试
    {
        public void Main()
        {
            AutoResetEvent ar = new AutoResetEvent(false);

            ThreadPool.RegisterWaitForSingleObject(ar, Run1, null, Timeout.Infinite, false);

            Console.WriteLine("时间:{0} 工作线程请注意，您需要等待5s才能执行。\n", DateTime.Now);

            Thread.Sleep(5000);

            ar.Set();

            Console.WriteLine("时间:{0} 工作线程已执行。\n", DateTime.Now);

            Console.Read();
        }

        static void Run1(object obj, bool sign)
        {
            Console.WriteLine("当前时间:{0}  我是线程{1}\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }

    //Timer计时器溶于信号量的概念以后同样可以实现计时器的功能。
    public class Timer测试
    {
        public void Main()
        {
            AutoResetEvent ar = new AutoResetEvent(false);

            //参数2000：其实就是WaitOne(2000)，采取超时机制
            ThreadPool.RegisterWaitForSingleObject(ar, Run1, null, 2000, false);

            Console.Read();
        }

        static void Run1(object obj, bool sign)
        {
            Console.WriteLine("当前时间:{0}  我是线程{1}\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }

    //停止计数器的运行
    class 停止计数器的运行
    {
        public void Main()
        {
            AutoResetEvent ar = new AutoResetEvent(false);

            RegisteredWaitHandle handle = ThreadPool.RegisterWaitForSingleObject(ar, Run1, null, 2000, false);

            Thread.Sleep(10000);

            handle.Unregister(ar);

            Console.WriteLine("小子，主线程要干掉你了。");

            Console.Read();
        }

        static void Run1(object obj, bool sign)
        {
            Console.WriteLine("当前时间:{0}  我是线程{1}", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }

    #endregion 
 

    #region Monitor锁机制

    //1：Monitor.Enter和Monitor.Exit
    public class Monitor类学习1
    {
        public void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(Run);
                t.Start();
            }
        }

        //资源
        static object obj = new object();
        static int count = 0;

        static void Run()
        {
            Thread.Sleep(10);
            //进入临界区
            Monitor.Enter(obj);
            Console.WriteLine("当前数字：{0}", ++count);
            //退出临界区
            Monitor.Exit(obj);
        }
    }


     //2：Monitor.Wait和Monitor.Pulse
     //首先这两个方法是成对出现，通常使用在Enter，Exit之间。
     //Wait： 暂时的释放资源锁，然后该线程进入”等待队列“中，那么自然别的线程就能获取到资源锁。
     //Pulse:  唤醒“等待队列”中的线程，那么当时被Wait的线程就重新获取到了锁。

    /// <summary>
    /// 锁定对象
    /// </summary>
    public class LockObj { }

    public class Monitor类学习2
    {
        public void Main()
        {
            LockObj obj = new LockObj();

            //注意，这里使用的是同一个资源对象obj
            Jack jack = new Jack(obj);
            John john = new John(obj);

            Thread t1 = new Thread(new ThreadStart(jack.Run));
            Thread t2 = new Thread(new ThreadStart(john.Run));

            t1.Start();
            t1.Name = "Jack";

            t2.Start();
            t2.Name = "John";

            Console.ReadLine();
        }
    }

    public class Jack
    {
        private LockObj obj;

        public Jack(LockObj obj)
        {
            this.obj = obj;
        }

        public void Run()
        {
            Monitor.Enter(this.obj);
            Console.WriteLine("{0}:1我已进入茅厕。", Thread.CurrentThread.Name);
            Console.WriteLine("{0}：2擦，太臭了，我还是撤！", Thread.CurrentThread.Name);
            //暂时的释放锁资源
            Monitor.Wait(this.obj);
            Console.WriteLine("{0}：5兄弟说的对，我还是进去吧。", Thread.CurrentThread.Name);
            //唤醒别的等待队列中的线程
            Monitor.Pulse(this.obj);
            Console.WriteLine("{0}：6拉完了，真舒服。", Thread.CurrentThread.Name);
            Monitor.Exit(this.obj);
        }
    }

    public class John
    {
        private LockObj obj;

        public John(LockObj obj)
        {
            this.obj = obj;
        }

        public void Run()
        {
            Monitor.Enter(this.obj);
            Console.WriteLine("{0}:3直奔茅厕，兄弟，你还是进来吧，小心憋坏了！",Thread.CurrentThread.Name);
            //唤醒别的等待队列中的线程
            Monitor.Pulse(this.obj);
            Console.WriteLine("{0}:4哗啦啦....", Thread.CurrentThread.Name);
            //暂时的释放锁资源
            Monitor.Wait(this.obj);
            Console.WriteLine("{0}：7拉完了,真舒服。", Thread.CurrentThread.Name);
            Monitor.Exit(this.obj);
        }
    }


    #endregion

    #region ReaderWriterLock类锁机制
    //Monitor实现的是在读写两种情况的临界区中只可以让一个线程访问，那么如果业务中存在”读取密集型“操作，就
    //好比数据库一样，读取的操作永远比写入的操作多。针对这种情况，我们使用Monitor的话很吃亏，不过没关系，ReadWriterLock
    //就很牛X，因为实现了”写入串行“，”读取并行“。

    public class ReaderWriterLock类
    {
        static List<int> list = new List<int>();

        static ReaderWriterLock rw = new System.Threading.ReaderWriterLock();

        public void Main()
        {
            Thread t1 = new Thread(AutoAddFunc);
            Thread t2 = new Thread(AutoReadFunc);

            t1.Start();
            t2.Start();

            Console.Read();
        }

        /// <summary>
        /// 模拟3s插入一次
        /// </summary>
        /// <param name="num"></param>
        public static void AutoAddFunc()
        {
            //3000ms插入一次
            System.Threading.Timer timer1 = new System.Threading.Timer(new TimerCallback(Write), null, 0, 3000);
        }

        public static void AutoReadFunc()
        {
            //1000ms自动读取一次
            System.Threading.Timer timer1 = new System.Threading.Timer(new TimerCallback(Read), null, 0, 1000);
            System.Threading.Timer timer2 = new System.Threading.Timer(new TimerCallback(Read), null, 0, 1000);
            System.Threading.Timer timer3 = new System.Threading.Timer(new TimerCallback(Read), null, 0, 1000);
        }

        public static void Write(object obj)
        {
            var num = new Random().Next(0, 1000);

            //获取写锁
            rw.AcquireWriterLock(TimeSpan.FromSeconds(30));

            list.Add(num);
            Console.WriteLine("我是线程{0}，我插入的数据是{1}。", Thread.CurrentThread.ManagedThreadId, num);

            //释放写锁
            rw.ReleaseWriterLock();
        }

        public static void Read(object obj)
        {
            //获取读锁
            rw.AcquireReaderLock(TimeSpan.FromSeconds(30));
            Console.WriteLine("我是线程{0},我读取的集合为:{1}",Thread.CurrentThread.ManagedThreadId, string.Join(",", list));
            //释放读锁
            rw.ReleaseReaderLock();
        }
    }
    #endregion

    #region Metux类互斥（需要实例化）

    //1: 线程间同步
    public class Metux类1
    {
        static int count = 0;
        static Mutex mutex = new Mutex();

        public void Main()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread t = new Thread(Run);
                t.Start();
            }
            Console.Read();
        }

        static void Run()
        {
            Thread.Sleep(100);
            mutex.WaitOne();
            Console.WriteLine("当前数字：{0}", ++count);
            mutex.ReleaseMutex();
        }
    }

    //2:进程间同步
    public class Metux类2
    {
        static Mutex mutex = new Mutex(false, "cnblogs");

        public void  Main()
        {
            Thread t = new Thread(Run);
            t.Start();
            Console.Read();
        }

        static void Run()
        {
            mutex.WaitOne();

            Console.WriteLine("当前时间：{0}我是线程:{1}，我已经进去临界区", DateTime.Now, Thread.CurrentThread.GetHashCode());

            Thread.Sleep(10000);

            Console.WriteLine("\n当前时间:{0}我是线程:{1}，我准备退出临界区", DateTime.Now, Thread.CurrentThread.GetHashCode());

            mutex.ReleaseMutex();
        }
    }
    //小结：1：当给Mutex取名的时候能够实现进程同步，不取名实现线程同步
    //      2：Mutex封装了win32的同步机制，而Monitor是由framework封装，所以在线程同步角度来说，Monitor更加短小精悍，
    //         要是实现进程同步，Monitor也干不了，所以Mutex是首选。


    #endregion

    #region Interlocked互斥 为多个线程共享的变量提供原子操作

    class Interlocked1
    {
        static int count = 0;
        //static Mutex mutex = new Mutex();

        public void Main()
        {
            for (int i = 0; i < 20; i++)
            {
                Thread t = new Thread(Run);
                t.Start();
            }
            Console.Read();
        }

        static void Run()
        {
            Thread.Sleep(100);

            Console.WriteLine("当前数字：{0}", Interlocked.Increment(ref count));
            //Console.WriteLine("当前数字：{0}", Interlocked.Decrement(ref count));
            //Interlocked.Add(ref count, 20);
            //Interlocked.Exchange(ref count, 30);
        }
    }

    #endregion

    #region  ManualResetEvent信号量

    //1:信号量初始为False，WaitOne采用无限期阻塞，线程间可以进行交互。
    public class ManualResetEvent1
    {
        //为false为阻塞状态,若为true则非阻塞状态。
        static ManualResetEvent mr = new ManualResetEvent(false);

        public void Main1()
        {
            Thread t = new Thread(Run);
            t.Name = "Jack";

            Console.WriteLine("当前时间:{0}  {1} ,我是主线程,收到请回答。", DateTime.Now, t.Name);

            t.Start();

            Thread.Sleep(5000);

            mr.Set();

            Console.Read();
        }

        static void Run()
        {
            mr.WaitOne();

            Console.WriteLine("\n当前时间:{0}  主线程，主线程,{1}已收到！", DateTime.Now, Thread.CurrentThread.Name);
        }
    }

    //2:信号量初始为True，WaitOne采用无限期阻塞，实验发现WaitOne其实并没有被阻塞
    public class ManualResetEvent2
    {
        //若为true则非阻塞状态，为false为阻塞状态。
        static ManualResetEvent mr = new ManualResetEvent(true);

        public static void Main1()
        {
            Thread t = new Thread(Run);
            t.Name = "Jack";

            Console.WriteLine("当前时间:{0}  {1},我是主线程,收到请回答。", DateTime.Now, t.Name);

            t.Start();

            Thread.Sleep(5000);

            mr.Set();

            Console.Read();
        }

        static void Run()
        {
            mr.WaitOne();//由于是非阻塞状态，这里没有任何意义，畅通无阻。

            Console.WriteLine("\n当前时间:{0}  主线程，主线程,{1}已收到！", DateTime.Now, Thread.CurrentThread.Name);
        }
    }

    //3:信号量初始为False，WaitOne采用超时2s，虽然主线程要等5s才能进行Set操作，但是WaitOne已经等不及提前执行了
    public class ManualResetEvent3
    {
        static ManualResetEvent mr = new ManualResetEvent(false);

        public static void Main1()
        {
            Thread t = new Thread(Run);
            t.Name = "Jack";

            Console.WriteLine("当前时间:{0}  {1} {1},我是主线程,收到请回答。", DateTime.Now, t.Name);

            t.Start();

            Thread.Sleep(5000);

            mr.Set();

            Console.Read();
        }

        static void Run()
        {
            mr.WaitOne(2000);

            Console.WriteLine("\n当前时间:{0}  主线程，主线程,{1}已收到！", DateTime.Now, Thread.CurrentThread.Name);
        }
    }

    #endregion

    #region AutoResetEvent信号量

    //跟ManualResetEvent类似都是继承于EventWaitHandle，
    //AutoResetEvent打开Set后执行一个WaitOne()马上就关闭，也就是每调用一次Set，仅有一个线程会继续。AutoResetEvent是典型的队列操作形式。
    //ManualResetEvent可以唤醒多个线程，打开后不管，除非手动Reset
    public class AutoResetEvent1
    {
        static AutoResetEvent ar = new AutoResetEvent(true);

        public void Main1()
        {
            Thread t = new Thread(Run);
            t.Name = "Jack";

            t.Start();

            Console.Read();
        }

        static void Run()
        {
            var state = ar.WaitOne(1000, true);

            Console.WriteLine("我当前的信号量状态:{0}", state);

            state = ar.WaitOne(1000, true);

            Console.WriteLine("我恨你，不理我，您现在的状态是:{0}", state);
        }
    }


    public class 买书测试
    {
        static int number; //这是关键资源

        static AutoResetEvent myResetEvent = new AutoResetEvent(false);
        static AutoResetEvent ChangeEvent = new AutoResetEvent(false);

        public static void Main1()
        {
            Thread payMoneyThread = new Thread(new ThreadStart(PayMoneyProc));
            payMoneyThread.Name = "付钱线程";

            Thread getBookThread = new Thread(new ThreadStart(GetBookProc));
            getBookThread.Name = "取书线程";

            payMoneyThread.Start();
            getBookThread.Start();

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("①买书线程：数量{0}", i);
                number = i;

                myResetEvent.Set();
                ChangeEvent.Set();
                Thread.Sleep(1000);
            }
            payMoneyThread.Abort();
            getBookThread.Abort();

            Console.ReadLine();
        }

        static void PayMoneyProc()
        {
            while (true)
            {
                myResetEvent.WaitOne();
                Console.WriteLine("②{0}：数量{1}", Thread.CurrentThread.Name, number);
            }
        }
        static void GetBookProc()
        {
            while (true)
            {
                ChangeEvent.WaitOne();
                Console.WriteLine("③完结了，{0}：数量{1}---------------", Thread.CurrentThread.Name, number);
                Thread.Sleep(0);
            }
        }


    }

    #endregion

    #region Semaphore信号量

    //.net 4.0新增的，用于控制线程的访问数量

    //1:initialCount=1,maximunCount=10,WaitOne采用无限期等待。

    public class Semaphore1
    {
        static Semaphore sem = new Semaphore(1, 10);

        public void Main()
        {
            Thread t1 = new Thread(Run1);
            t1.Start();

            Thread t2 = new Thread(Run2);
            t2.Start();

            Thread.Sleep(1000);

            sem.Release(10);

            Console.Read();
        }

        static void Run1()
        {
            sem.WaitOne();

            Console.WriteLine("大家好，我是Run1");
        }

        static void Run2()
        {
            sem.WaitOne();

            Console.WriteLine("大家好，我是Run2");
        }
    }


    //2:Semaphore命名，升级进程交互。
    //Semaphore是继承字WaitHandle，而WaitHandle封装了win32的一些同步机制，所以当我们给Semaphore命名的时候就会在系统中可见，
    //下面举个例子，把下面的代码copy一份，运行两个程序。
    public class Semaphore2
    {
        static Semaphore sem = new Semaphore(3, 10, "cnblogs");

        public void Main()
        {
            Thread t1 = new Thread(Run1);
            t1.Start();

            Thread t2 = new Thread(Run2);
            t2.Start();

            Console.Read();
        }

        static void Run1()
        {
            sem.WaitOne();

            Console.WriteLine("当前时间:{0} 大家好，我是Run1", DateTime.Now);
        }

        static void Run2()
        {
            sem.WaitOne();

            Console.WriteLine("当前时间:{0} 大家好，我是Run2", DateTime.Now);
        }
    }
    //设置了信号量是3个，只能有三个线程持有WaitOne，后续的线程只能等待。

    #endregion 


    #region 生产者消费者

    /// <summary>
    /// 多线程主函数
    /// </summary>
    public class 生产者消费者
    {
        public  void Main()
        {
            int result = 0; //一个标志位，如果是0表示程序没有出错，如果是1表明有错误发生
            Cell cell = new Cell();

            //下面使用cell初始化CellProd和CellCons两个类，生产和消费次数均为20次
            CellProd prod = new CellProd(cell, 20);
            CellCons cons = new CellCons(cell, 20);

            Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));
            //生产者线程和消费者线程都已经被创建，但是没有开始执行 
            try
            {
                producer.Start();
                consumer.Start();

                producer.Join();
                consumer.Join();
                Console.ReadLine();
            }
            catch (ThreadStateException e)
            {
                //当线程因为所处状态的原因而不能执行被请求的操作
                Console.WriteLine(e);
                result = 1;
            }
            catch (ThreadInterruptedException e)
            {
                //当线程在等待状态的时候中止
                Console.WriteLine(e);
                result = 1;
            }

            //尽管Main()函数没有返回值，但下面这条语句可以向父进程返回执行结果
            Environment.ExitCode = result;
        }
    }

    /// <summary>
    /// 生产操作类
    /// </summary>
    public class CellProd
    {
        Cell cell; // 被操作的Cell对象
        int quantity = 1; // 生产者生产次数，初始化为1 

        ///构造函数
        public CellProd(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }

        /// <summary>
        /// 生产
        /// </summary>
        public void ThreadRun()
        {
            for (int looper = 1; looper <= quantity; looper++)
            {
                //生产者向操作对象写入信息
                cell.WriteToCell(looper);
            }
        }
    }

    /// <summary>
    /// 消费操作类
    /// </summary>
    public class CellCons
    {
        Cell cell;
        int quantity = 1;

        public CellCons(Cell box, int request)
        {
            //构造函数
            cell = box;
            quantity = request;
        }
        /// <summary>
        /// 消费
        /// </summary>
        public void ThreadRun()
        {
            int valReturned;
            for (int looper = 1; looper <= quantity; looper++)
            {
                //消费者从操作对象中读取信息
                valReturned = cell.ReadFromCell(looper);
            }
        }
    }


    /// <summary>
    /// 操作对象类
    /// </summary>
    public class Cell
    {
        int cellContents; // Cell对象里边的内容
        bool isread = false; //为false则不能读取，写入正在进行

       
        /// <summary>
        /// 生产操作
        /// </summary>
        /// <param name="n"></param>
        public void WriteToCell(int n)
        {
            lock (this)
            {
                if (isread)
                {
                    Monitor.Wait(this);
                }
                cellContents = n;
                Console.WriteLine("生产了: {0}", cellContents);

                isread = true;
                //唤醒等待队列中的线程
                Monitor.Pulse(this);
            }
        }

        /// <summary>
        /// 消费操作
        /// </summary>
        /// <returns></returns>
        public int ReadFromCell(int n)
        {
            lock (this)
            {
                if (!isread)//如果现在不可读取
                {
                    Monitor.Wait(this);
                }
                Console.WriteLine("消费了: {0},目前生产了{1}", n,cellContents);

                //表示消费行为已经完成
                isread = false;
                Monitor.Pulse(this);
            }
            return cellContents;
        }

        
                    //catch (SynchronizationLockException e)
                    //{
                    //    Console.WriteLine(e);
                    //}
                    //catch (ThreadInterruptedException e)
                    //{
                    //    Console.WriteLine(e);
                    //}
    }

    #endregion


    #region 简易线程的三种实现方式
    /// <summary>
    /// 简易线程的三种实现方式
    /// </summary>
    public class 简易线程的三种实现方式
    {
        public void 方式一()
        {
            //QueueUserWorkItem方式一
            ThreadPool.QueueUserWorkItem(delegate { Console.WriteLine("成功"); });


            //QueueUserWorkItem方式二
            Action<object> action = (object obj) => { Console.WriteLine(obj.ToString()); };
            ThreadPool.UnsafeQueueUserWorkItem(obj => action(obj), "obj");


            //QueueUserWorkItem方式三
            ThreadPool.UnsafeQueueUserWorkItem(RunWorkerThread, "obj");
            void RunWorkerThread(object obj)
            {
                Console.WriteLine("RunWorkerThread开始工作");
                Console.WriteLine("工作者线程启动成功!");
            }


        }

        public void 方式二() 
        {
            //Task.Factory.StartNew 可以设置线程是长时间运行，这时线程池就不会等待这个线程回收
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("3.5秒后结束");
                Thread.Sleep(3500);
                Console.WriteLine("结束2");
            });

            //带返回参数
            Func<object, int> function = (object a) => { return 11; };
            var result = Task.Factory.StartNew(function, 1);
            var aaa = result.Result;
        }

        public void 方式三()
        {
            //Task.Run方式
            Task.Run(() =>
            {
                Console.WriteLine("结束3");
            });

            //高级测试
            var task = new Task(
                   (object obj) => { Console.WriteLine("Title:" + obj.ToString()); }
                   , 123);
            Func<Task> func = () => { return task; };
            var resunt2 = Task.Run(func);
        }
    }
    #endregion
}
