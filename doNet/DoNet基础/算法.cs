using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DoNet基础
{
    public class 数组比较算法
    {
        public static void ValidateArrayElement1()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();//开始计时
            Random rand = new Random();
            Int32 maxValue = 120000;//元素最大值，是一个假定值

            Int32 length = 70000;// A,B的长度
            Int32[] A = new Int32[length];
            Int32[] B = new Int32[length];
            Boolean[] C = new Boolean[length];
            //随机初始化A，B数组
            for (int i = 0; i < length; i++)
            {
                A[i] = rand.Next(maxValue);
                B[i] = rand.Next(maxValue);
            }
            //循环A，验证是否存在，将C对应位置标记为true
            for (int i = 0; i < A.Length; i++)
            {
                if (B.Contains(A[i])) C[i] = true;
            }
            sp.Stop();
            Console.WriteLine(sp.ElapsedMilliseconds);
        }

        public static void ValidateArrayElement2()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            Random rand = new Random();
            Int32 maxValue = 120000;//元素最大值，是一个假定值

            Int32 length = 70000;// A,B的长度
            Int32[] A = new Int32[length];
            Int32[] B = new Int32[length];
            Boolean[] C = new Boolean[length];
            Boolean[] Atemp = new Boolean[maxValue];//临时的辅助变量
            //随机初始化A，B数组
            for (int i = 0; i < length; i++)
            {
                A[i] = rand.Next(maxValue);
                B[i] = rand.Next(maxValue);
            }
            //循环B，验证元素是否存在
            foreach (var item in B) 
                Atemp[item] = true;
            //循环A，验证是否存在，将C对应位置标记为true
            for (int i = 0; i < A.Length; i++)
            {
                if (Atemp[A[i]])
                    C[i] = true;
            }
            sp.Stop();//停止计时
            Console.WriteLine(sp.ElapsedMilliseconds);
        }
        public static void ValidateArrayElement3()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            Random rand = new Random();
            Int32 maxValue = 120000;//元素最大值，是一个假定值

            Int32 length = 70000;// A,B的长度
            Int32[] A = new Int32[length];
            Int32[] B = new Int32[length];
            Boolean[] C = new Boolean[length];
            var tmp = new HashSet<int>();
            //随机初始化A，B数组
            for (int i = 0; i < length; i++)
            {
                A[i] = rand.Next(maxValue);
                B[i] = rand.Next(maxValue);

                if (!tmp.Contains(B[i]))
                    tmp.Add(B[i]);
            }

            //循环A，验证是否存在，将C对应位置标记为true
            for (int i = 0; i < A.Length; i++)
                C[i] = tmp.Contains(A[i]);

            sp.Stop();//停止计时
            Console.WriteLine(sp.ElapsedMilliseconds);
        }

        public static void ValidateArrayElement4()
        {
            Stopwatch sp = new Stopwatch();
            sp.Start();
            Random rand = new Random();
            Int32 maxValue = 120000;//元素最大值，是一个假定值

            Int32 length = 70000;// A,B的长度
            System.Collections.BitArray A = new System.Collections.BitArray(maxValue, false);
            System.Collections.BitArray B = new System.Collections.BitArray(maxValue, false);
            System.Collections.BitArray C = new System.Collections.BitArray(maxValue, false);
            for (int i = 0; i < length; i++)
            {
                A.Set(rand.Next(maxValue - 1), true);
                B.Set(rand.Next(maxValue - 1), true);
            }
            C = A.And(B);
            sp.Stop();//停止计时
            Console.WriteLine(sp.ElapsedTicks);//计时周期。因为毫秒太大了无法比较
        }

        public static void ValidateArrayElement5()
        {
            int i = 10000000;
            int[] a = new int[i];
            for (int x = 0; x < a.Length; x++)
            {
                a[x] = x;
            }
            Random ran = new Random();
            int[] b = new int[i];
            for (int y = 0; y < b.Length; y++)
            {
                b[y] = ran.Next(0, i);
            }
            List<int> la = new List<int>(a);
            List<int> lb = new List<int>(b);

            var jiaoji = la.Intersect(lb);
        }
    }
}
