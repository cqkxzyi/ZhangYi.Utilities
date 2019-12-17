using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DoNet基础.委托
{
    public delegate int mydelegate1(int a, int b);

    /// <summary>
    /// 对泛型设置约束
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SomethingFactory<T> where T : StringComparer, new()
    { }

    public class 委托学习
    {
        public event mydelegate1 mydb1;//声明一个事件不过类似于声明一个进行了封装的 委托类型的变量而已

        public int Sum(int a, int b)
        {
            return a + b;
        }
        public int Sub(int a, int b)
        {
            return a - b;
        }
        public int oper(int a, int b, mydelegate1 db)
        {
            return db(a, b);
        }


        //1.0 委托简介
        public void ExecDelegate()
        {
            //定义委托变量也可以用new : mydb mydb1=new mydb(Sum);
            mydb1 = Sum;
            
            //多个方法绑定到同一个委托 
            mydb1 += Sum;

            //可以绕过方法oper，直接执行委托。
            int returnsum2 = mydb1(1, 2);

            //也可以直接传入方法名Sum
            int retnSum = oper(1, 2, mydb1);
        }

        //2.0 委托组合与分解(委托可以同时绑定执行多个方法)
        public void 委托组合与分解()
        {
            mydelegate1 a, b, c, d;
            a = Sum;
            b = Sub;
            c = a + b;
            d = c - a;

            d(11,22);
        }


        //3.泛型委托
        public delegate T mydelegate2<T,M>(T obj1,M obj2);

        public void 泛型委托()
        {
            mydelegate2<int, int> del = Sum;

            int retnVal = del(11, 22);

            //.NET基类库针对在实际开发中最常用的情形提供了几个预定义好的委托

            //1、Func系列委托用于引用一个有返回值的方法
            Func<int, int, int> func = Sum;
            retnVal = func(123, 321);
            //或者
            var d1 = new Func<int, int, int>(Sum);
            retnVal = d1(123, 321);

            //2、Predicate委托：引用一个返回bool值的方法
            var d2 = new Predicate<int>(More);
            bool istrue = Print(new List<int> { 1, 2, 3 }, d2);

            //3、Action系列委托：接收返回为void的方法
            var d4 = new Action<int, string>(twoParamNoReturnAction);

            d4(1,"asdf");
        }

        #region 委托辅助类容
       
        static bool More(int item)
        {
            if (item > 3)
            {
                return true;
            }
            return false;
        }

        public bool Print(List<int> arr, Predicate<int> dl)
        {
            foreach (var item in arr)
            {
                if (dl(item))
                {
                    Console.WriteLine(item);
                }
            }
            return false;
        }

        static void twoParamNoReturnAction(int a, string b)
        {
            //do what you want
        }
        #endregion

        // 4.匿名方法和Lambda表达式
        public void 匿名方法和Lambda表达式()
        {
            //方式一
            mydelegate1 del = delegate(int i, int j){ return i + j; };
           int retnVal= del(11,22);

            //方式二
           mydelegate1 del2 = (argument1,argument2) => { return argument1+argument2; };
           del2(1,2);

           //方式三
           Func<int, bool> del3 = (x) => { return x > 0; };
           bool istrue = del3(10);
         
        }
    }

}
