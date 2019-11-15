using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoNet基础.多线程_异步_并行
{
    public class 安全集合
    {
        /// <summary>
        /// ConcurrentBag 安全集合
        /// 如果想在删除时也是线程安全的，也可使用BlockingCollection<T>类
        /// </summary>
        public static void ConcurrentBagWithPallel()
        {
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            Parallel.For(0, 10000, item =>
            {
                list.Add(item);
            });
            Console.WriteLine("ConcurrentBag's count is {0}", list.Count());

            //以下代码不安全，会报错
            List<int> list2 = new List<int>();
            Parallel.For(0, 100, item =>
            {
                list2.Add(item);
            });
            Console.WriteLine("List's count is {0}", list2.Count());
        }

    }

    public class 控制并发的高级用法
    {
        public int num = 1;
        List<int> list = new List<int>();
        
        public 控制并发的高级用法() {
            
            for (int i = 1; i <= 2000; i++)
            {
                list.Add(i);
            }
            Console.WriteLine($"num初始值为：" + num.ToString());
        }

        /// <summary>
        /// 未控制并发
        /// </summary>
        public void 测试1() {
           
            list.AsParallel().ForAll(n =>
            {
                num++;
            });
            Console.WriteLine($"不加锁，并发{list.Count}次后为：" + num.ToString());
            Console.ReadKey();
        }



        /// <summary>
        /// 控制并发的用法
        /// C#内置了很多锁对象，如lock 互斥锁，Interlocked 内部锁，Monitor 这几个比较常见，lock内部实现其实就是使用了Monitor对象
        /// </summary>
        public void 测试2()
        {
            list.AsParallel().ForAll(n =>
            {
                //Interlocked对象提供了，变量自增，自减、或者相加等方法,我们使用自增方法Interlocked.Increment
                Interlocked.Increment(ref num);
            });
            //Parallel.ForEach(list, (index) =>
            //{
            //    Interlocked.Increment(ref index);
            //});

            Console.WriteLine($"使用内部锁，并发{list.Count}次后为：" + num.ToString());
        }


        /// <summary>
        /// 继续测试3   m.Add(num)会加入重复数据
        /// </summary>
        public void 测试3()
        {
            var total = 0;
            Random random = new Random();
            ConcurrentBag<int> m = new ConcurrentBag<int>();

            list.AsParallel().ForAll(n =>
            {
                var c = random.Next(1, 50);
                Interlocked.Add(ref total, c);
                for (int i = 0; i < c; i++)
                {
                    Interlocked.Increment(ref num);
                    m.Add(num);
                }
            });
            Console.WriteLine($"使用内部锁，并发+内部循环{list.Count}次后为：" + num.ToString());
            Console.WriteLine($"实际值为：{total + 1}");
            Console.WriteLine($"ConcurrentBag添加num，集合总数：{m.Count+1}个");
            var l = m.GroupBy(n => n).Where(o => o.Count() > 1);
            Console.WriteLine($"并发里面使用安全集合ConcurrentBag添加num，集合重复值：{l.Count()}个");
            Console.ReadKey();
        }


        /// <summary>
        /// 继续测试4   OK
        /// BlockingCollection 代替 ConcurrentBag
        /// </summary>
        public void 测试4()
        {
            var total = 0;
            Random random = new Random();

            using (var q = new BlockingCollection<int>())
            {
                list.AsParallel().ForAll(n =>
                {
                    var c = random.Next(1, 50);
                    Interlocked.Add(ref total, c);
                    for (int i = 0; i < c; i++)
                    {

                        q.Add(Interlocked.Increment(ref num));


                        //方式2  这样做其实避免了很多不必要的麻烦
                        //lock (objLock)
                        //{
                        //    num++;
                        //    q.Add(num);
                        //}
                    }

                });
                q.CompleteAdding();

                Console.WriteLine($"使用内部锁，并发+内部循环{list.Count}次后为：" + num.ToString());
                Console.WriteLine($"实际值为：{total + 1}");
                Console.WriteLine($"BlockingCollection添加num，集合总数：{q.Count + 1}个");
                var l = q.GroupBy(n => n).Where(o => o.Count() > 1);
                Console.WriteLine($"并发里面使用安全集合BlockingCollection添加num，集合重复值：{l.Count()}个");
                Console.ReadKey();
            }
        }
    }
}
