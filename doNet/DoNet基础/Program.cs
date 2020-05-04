using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DoNet基础;
using DoNet基础.Linq;
using DoNet基础.多线程;
using DoNet基础.多线程_异步_并行;
using DoNet基础.异步;
using DoNet基础.泛型集合;

namespace DoNet基础
{
   static class Program
   {
      /// <summary>
      /// 应用程序的主入口点。
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //return;

            //数组比较算法.ValidateArrayElement1();
            //数组比较算法.ValidateArrayElement2();
            //数组比较算法.ValidateArrayElement3();
            //数组比较算法.ValidateArrayElement4();

            //简单了解线程池 temp = new 简单了解线程池();
            //temp.CallAsyncSleep30Times();
            //Console.Read();
            // return;

            // 生产者消费者 ASDF = new 生产者消费者();
            // ASDF.Main();

            // //Interlocked1 ceshi = new Interlocked1();
            // //ceshi.Main();

            // Product a = new Product();
            // a.Id = "1";
            // Product b = new Product();
            // b.Id = "1";

            // bool mm = (a == b);
            // mm = a.Equals(b);//Equals是用来判断两个对象（除string类型外）是否相等，相等的条件是：(值，地址，引用全相等），因为String类重写了Equals方法，所以当string类型的对象用Equals方法比较时只比较两个对象有值 相等返回true 不相等返回false。

            //bool b1= System.Object.ReferenceEquals(a, b);  //returns true
            //bool b2 = Object.Equals(a,b);

            //安全集合.ConcurrentBagWithPallel();

            //控制并发的高级用法 temp = new 控制并发的高级用法();
            //temp.测试4();


            SelectManyTest test = new SelectManyTest();
            test.Test();


            Console.Read();
            //Console.ReadKey();


        }
      


   }
}
