using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础
{

    #region 测试例子1
    /// <summary>
    /// 测试例子1
    /// </summary>

    public static class Test
    {
        public static void Ge()
        {
            Model model = new Model();
            foreach (int i in Feige.Fei(model))
            {
                model.A += "返回的结果是：" + i;
                Console.WriteLine(model.A);
            }
        }
        class Feige
        {
            public static IEnumerable<int> Fei(Model rtxt)
            {
                for (int i = 0; i < 5; i++)
                {
                    //Thread.Sleep(500);
                    rtxt.A += "循环：" + i;
                    yield return i;
                }
                yield break;
            }
        }

    }

    public class Model
    {
        public string A { get; set; }
    }

    #endregion


    #region 测试例子2

    static class TestYield
    {
        static private List<int> _numArray; //用来保存1-100 这100个整数

        static TestYield() //构造函数。我们可以通过这个构造函数往待测试集合中存入1-100这100个测试数据
        {
            _numArray = new List<int>(); //给集合变量开始在堆内存上开内存，并且把内存首地址交给这个_numArray变量

            for (int i = 1; i <= 20; i++)
            {
                _numArray.Add(i);  //把1到100保存在集合当中方便操作
            }
        }

        //测试求1到100之间的全部偶数
        static public void TestMethod()
        {
            foreach (var item in GetAllEvenNumber())
            {
                Console.WriteLine(item); //输出偶数测试
            }
        }

        //测试我们使用Yield Return情况下拿到全部偶数的方法
        static IEnumerable<int> GetAllEvenNumber()
        {

            foreach (int num in _numArray)
            {
                if (num % 2 == 0) //判断是不是偶数
                {
                    yield return num; //返回当前偶数
                }
            }
            yield break;  //当前集合已经遍历完毕，我们就跳出当前函数，其实你不加也可以
                          //这个作用就是提前结束当前函数，就是说这个函数运行完毕了。
        }


    }

    #endregion
}
