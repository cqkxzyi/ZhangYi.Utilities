using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoNet基础.多线程_异步
{

    public static class TaskTest
    {
        private static void button1_Click(object sender, EventArgs e)
        {
            Task t1 = Task.Factory.StartNew(() => k1());
            Task t2 = Task.Factory.StartNew(() => k2());
     

            //更加简洁的书写方式
            Task.Run(() =>
            {
                Console.WriteLine("执行");
            });
        }

        static void k1()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                //改变界面
                //this.Invoke(new Action(
                //    () => { this.label1.Text = i.ToString(); }));
            }
        }

        static void k2()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                //改变界面
                //this.Invoke(new Action(
                //    () => { this.label2.Text = i.ToString(); }));
            }
        }


        /// <summary>
        /// 测试Task.Run的线程执行顺序
        ///执行结果：   开始 线程1
        //              进入 线程2
        //              退出 线程1
        /// </summary>
        public static void Test()
        {
            Console.WriteLine("开始 线程" + Thread.CurrentThread.ManagedThreadId);
            var t = Task.Run(() =>
            {
                Console.WriteLine("进入 线程" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1500);
                Console.WriteLine("完毕" + Thread.CurrentThread.ManagedThreadId);
            });

            Task.WaitAll(t);
            Console.WriteLine("退出 线程" + Thread.CurrentThread.ManagedThreadId);
        }



    }



    public class ParallelDemo
    {

        public void ParallelForTest()
        {
            StringBuilder str = new StringBuilder();
            Parallel.For(0, 100, c =>
            {
                str.Append(c);
            });


            str = new StringBuilder();
            List<int> _data = new List<int>();
            _data.Add(1); _data.Add(2); _data.Add(3); _data.Add(4);

            Parallel.ForEach(_data, (index) =>
            {
                str.Append(index);
            });


            //并行调用多个任务
            var tasks = new Action[] { () => Task1(), () => Task1(), () => Task1() };
            Parallel.Invoke(tasks);

        }

        public void Task1()
        {
            Console.WriteLine("执行");
        }

    }


    public class ParallelInvokeDemo
    {
        public void TaskDemo()
        {
            /*
             * CancellationTokenSource - 取消任务的操作需要用到的一个类
             *     Token - 一个 CancellationToken 类型的对象，用于通知取消指定的操作
             *     IsCancellationRequested - 是否收到了取消操作的请求
             *     Cancel() - 结束任务的执行
             * ParallelOptions - 并行运算选项
             *     CancellationToken - 设置一个 Token，用于取消任务时的相关操作
             *     MaxDegreeOfParallelism - 指定一个并行循环最多可以使用多少个线程
             */

            CancellationTokenSource cts = new CancellationTokenSource();
            ParallelOptions pOption = new ParallelOptions() { CancellationToken = cts.Token };
            pOption.MaxDegreeOfParallelism = 10;

            Console.WriteLine("开始执行，3.5 秒后结束");
            Console.WriteLine("<br />");

            /*
             * Task - 任务类
             *     Factory.StartNew() - 创建并开始一个或一批新任务
             *     ContinueWith() - 此任务完成后执行指定的另一个任务
             *     AsyncState - 此任务的上下文对象
             *     Wait() - 阻塞，直到任务完成
             */
            Task task0 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("正在运行");
                Thread.Sleep(3500);
                cts.Cancel();
                Console.WriteLine("结束");
                Console.WriteLine("<br />");
            });

            // 通过 Parallel.Invoke 执行任务的时候，可以加入 ParallelOptions 参数，用于对此并行运算做一些配置
            Parallel.Invoke(pOption,
                () => Thread1(pOption.CancellationToken),
                () => Thread2(pOption.CancellationToken));


            /*
             * 一个 Task 内可以包含多个 Task
            Task tasks = new Task(() => 
            {
                Task.Factory.StartNew(() => Method()); 
                Task.Factory.StartNew(() => Method2()); 
                Task.Factory.StartNew(() => Method3()); 
            }); 
            tasks.Start(); 
            // 阻塞，直到整个任务完成
            tasks.Wait(); 
            */


            /*
             * 带返回值的 Task
            Func<object, long> fun = delegate(object state)
            {
                return 1.0;
            };
            Task<long> tsk = new Task<long>(fun, "state");
            tsk.Start();
            Console.WriteLine(tsk.Result.ToString()); 
            */
        }



        private void Thread1(CancellationToken token)
        {
            // 每隔 1 秒执行一次，直到此任务收到了取消的请求
            // 注意：虽然此处是其他线程要向主线程（UI线程）上输出信息，但因为使用了 Task ，所以不用做任何处理
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Task1 - " + "ThreadId: " + Thread.CurrentThread.ManagedThreadId.ToString());
                Console.WriteLine("<br />");
                Thread.Sleep(1000);
            }

        }
        private void Thread2(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Task2 - " + "ThreadId: " + Thread.CurrentThread.ManagedThreadId.ToString());
                Console.WriteLine("<br />");
                Thread.Sleep(1000);
            }
        }
    }


    public partial class ParallelPLINQ
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(i);
            }

            // AsParallel() - 并行运算
            // AsSequential() - 串行运算
            // AsOrdered() - 保持数据的原有顺序（AsSequential()指的是串行运算；AsOrdered()指的是如果在并行运算的前提下，它会把结果先缓存，然后排序，最后再把排序后的数据做输出）
            // AsUnordered() - 可以不必保持数据的原有顺序
            // WithDegreeOfParallelism() - 明确地指出需要使用多少个线程来完成工作
            // WithCancellation(new CancellationTokenSource().Token) - 指定一个 CancellationToken 类型的参数

            ParallelQuery nums = from num in list.AsParallel<int>().AsOrdered<int>()
                                 where num % 10 == 0
                                 select num;

            foreach (var num in nums)
            {
                Console.WriteLine(num.ToString());
                Console.WriteLine("<br />");
            }

            // 聚合方法也可以做并行运算
            Console.WriteLine(list.AsParallel().Average().ToString());
            Console.WriteLine("<br />");

            // 自定义聚合方法做并行运算的 Demo（实现一个取集合的平均值的功能）
            double myAggregateResult = list.AsParallel().Aggregate(
                // 聚合变量的初始值
                0d,

                // 在每个数据分区上，计算此分区上的数据
                // 第一个参数：对应的数据分区的计算结果；第二个参数：对应的数据分区的每个数据项
                (value, item) =>
                {
                    double result = value + item;
                    return result;
                },

                // 根据每个数据分区上的计算结果，再次做计算
                // 第一个参数：全部数据的计算结果；第二个参数：每个数据分区上的计算结果
                (value, data) =>
                {
                    double result = value + data;
                    return result;
                },

                // 根据全部数据的计算结果再次计算，得到最终的聚合结果
                (result) => result / list.Count
            );

            Console.WriteLine(myAggregateResult.ToString());
        }
    }

}
