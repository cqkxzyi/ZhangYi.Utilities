using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DoNet基础.异步
{
    #region 委托类
    public class 委托类
    {
        delegate string MyDelegate(string name);

        public void 基础()
        {
            //建立委托
            MyDelegate myDelegate = new MyDelegate(Hello);
            //异步调用委托，获取计算结果
            IAsyncResult result = myDelegate.BeginInvoke("Leslie", null, null);

            //在异步线程未完成前执行其他工作
            while (!result.IsCompleted)
            {
                Thread.Sleep(200);      //虚拟操作
                Console.WriteLine("Main thead do work!");
            }
            //也可以使用WailHandle完成同样的工作，WaitHandle里面包含有方法WaitOne，与使用 IAsyncResult.IsCompleted 同样且更方便
            while (!result.AsyncWaitHandle.WaitOne(200))
            {
                Console.WriteLine("Main thead do work!");
            }

            //等待异步方法完成，调用EndInvoke(IAsyncResult)获取运行结果
            string data = myDelegate.EndInvoke(result);

            // EndInvoke执行完毕，取得之前传递的参数内容
            string strState = (string)result.AsyncState;

            Console.WriteLine(data);
        }

        //当要监视多个运行对象的时候，使用IAsyncResult.WaitHandle.WaitOne可就派不上用场了。
        static void WaitAny用法(string[] args)
        {
            ThreadMessage("Main Thread");

            //建立委托
            MyDelegate myDelegate = new MyDelegate(Hello);

            //异步调用多个委托
            IAsyncResult result1 = myDelegate.BeginInvoke("Leslie", null, null);
            IAsyncResult result2 = myDelegate.BeginInvoke("Leslie", null, null);

            //此处可加入多个检测对象
            WaitHandle[] waitHandleList = new WaitHandle[] { result1.AsyncWaitHandle, result2.AsyncWaitHandle };
            while (!WaitHandle.WaitAll(waitHandleList, 200))
            {
                Console.WriteLine("Main thead do work!");
            }
            string data1 = myDelegate.EndInvoke(result1);
            string data2 = myDelegate.EndInvoke(result2);

            Console.WriteLine(data1 + data2);
            Console.ReadKey();
        }


        static string Hello(string name)
        {
            Thread.Sleep(2000);            //虚拟异步工作
            return "Hello " + name;
        }
        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  ThreadId is:{1}", data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }

    }
    #endregion

    #region 回调函数

    public class 回调函数
    {
        delegate string MyDelegate(string name);

        static void Test(string[] args)
        {
            ThreadMessage("Main Thread");

            //建立委托
            MyDelegate myDelegate = new MyDelegate(Hello);
            //异步调用委托，获取计算结果
            myDelegate.BeginInvoke("Leslie", new AsyncCallback(Completed), null);
            //在启动异步线程后，主线程可以继续工作而不需要等待
            for (int n = 0; n < 6; n++)
            {
                Console.WriteLine("  Main thread do work!");
            }
            Console.WriteLine("");
            Console.ReadKey();
        }

        static string Hello(string name)
        {
            ThreadMessage("Async Thread");
            Thread.Sleep(2000);             //模拟异步操作
            return "\nHello " + name;
        }

        static void Completed(IAsyncResult result)
        {
            ThreadMessage("Async Completed");

            AsyncResult _result = (AsyncResult)result;
            MyDelegate myDelegate = (MyDelegate)_result.AsyncDelegate;
            //获取委托对象，调用EndInvoke方法获取运行结果
            string data = myDelegate.EndInvoke(_result);

            Console.WriteLine(data);
        }

        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  ThreadId is:{1}", data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }
    }
    #endregion

    #region 回调函数_回调函数带参数

    public class 回调函数_回调函数带参数
    {
        public class Person
        {
            public string Name;
            public int Age;
        }

        delegate string MyDelegate(string name);

        static void Test(string[] args)
        {
            ThreadMessage("Main Thread");

            //建立委托
            MyDelegate myDelegate = new MyDelegate(Hello);

            //建立Person对象
            Person person = new Person();
            person.Name = "Elva";
            person.Age = 27;

            //异步调用委托，输入参数对象person, 获取计算结果
            myDelegate.BeginInvoke("Leslie", new AsyncCallback(Completed), person);

            //在启动异步线程后，主线程可以继续工作而不需要等待
            for (int n = 0; n < 6; n++)
                Console.WriteLine("  Main thread do work!");
            Console.WriteLine("");

            Console.ReadKey();
        }

        static string Hello(string name)
        {
            ThreadMessage("Async Thread");
            Thread.Sleep(2000);
            return "\nHello " + name;
        }

        static void Completed(IAsyncResult result)
        {
            ThreadMessage("Async Completed");

            //获取委托对象，调用EndInvoke方法获取运行结果
            AsyncResult _result = (AsyncResult)result;
            MyDelegate myDelegate = (MyDelegate)_result.AsyncDelegate;
            string data = myDelegate.EndInvoke(_result);
            //获取Person对象
            Person person = (Person)result.AsyncState;
            string message = person.Name + "'s age is " + person.Age.ToString();

            Console.WriteLine(data + "\n" + message);
        }

        static void ThreadMessage(string data)
        {
            string message = string.Format("{0}\n  ThreadId is:{1}",data, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }

    }
    #endregion



    public class 简单了解线程池
    {
        public void CallAsyncSleep30Times()
        {
            // 创建包含Sleep函数的委托对象
            MethodInvoker invoker = new MethodInvoker(Sleep);

            for (int i = 0; i < 30; i++)
            {
                // 以异步的形式，调用Sleep函数30次
                invoker.BeginInvoke(null, null);
            }
        }

        private void Sleep()
        {
            int intAvailableThreads, intAvailableIoAsynThreds;

            // 取得线程池内的可用线程数目，我们只关心第一个参数即可
            ThreadPool.GetAvailableThreads(out intAvailableThreads,out intAvailableIoAsynThreds);

            // 线程信息
            string strMessage =String.Format("是否是线程池线程：{0},线程托管ID：{1},可用线程数：{2}",
            Thread.CurrentThread.IsThreadPoolThread.ToString(),Thread.CurrentThread.GetHashCode(),intAvailableThreads);

            Console.WriteLine(strMessage);

            Thread.Sleep(15020);
        }
    }

    
}
