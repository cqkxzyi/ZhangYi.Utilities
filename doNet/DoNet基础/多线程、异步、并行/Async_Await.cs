using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础.多线程_异步
{
    public class MyClass
    {
        public MyClass()
        {
            DisplayValue(); //这里不会阻塞  
            System.Diagnostics.Debug.WriteLine("MyClass() End.");
        }

        public async void DisplayValue()
        {
            double result = await GetValueAsync(1234.5, 1.01);//此处会开新线程处理GetValueAsync任务，然后方法马上返回  

            //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
            System.Diagnostics.Debug.WriteLine("Value is : " + result);
        }

        public Task<double> GetValueAsync(double num1, double num2)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }


        /// <summary>
        /// 上面的DisplayValue实际执行效果是下面这样的。
        /// </summary>
        public void DisplayValue2()
        {
            System.Runtime.CompilerServices.TaskAwaiter<double> awaiter = GetValueAsync(1234.5, 1.01).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                double result = awaiter.GetResult();
                System.Diagnostics.Debug.WriteLine("Value is : " + result);
            });
        }
    }



    /// <summary>
    /// 可以作为公共函数使用
    /// </summary>
    public static class TaskAsyncHelper
    {
        /// <summary>  
        /// 将一个方法function异步运行，在执行完毕时执行回调callback  
        /// </summary>  
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是void</param>  
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法没有参数，返回类型必须是void</param>  
        public static async void RunAsync(Action function, Action callback)
        {
            Func<Task> taskFunc = () =>
            {
                return Task.Run(() =>
                {
                    function();
                });
            };
            await taskFunc();
            if (callback != null)
                callback();
        }

        /// <summary>  
        /// 将一个方法function异步运行，在执行完毕时执行回调callback  
        /// </summary>  
        /// <typeparam name="TResult">异步方法的返回类型</typeparam>  
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是TResult</param>  
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法参数为TResult，返回类型必须是void</param>  
        public static async void RunAsync<TResult>(Func<TResult> function, Action<TResult> callback)
        {
            Func<Task<TResult>> taskFunc = () =>
            {
                return Task.Run(() =>
                {
                    return function();
                });
            };
            TResult rlt = await taskFunc();
            callback?.Invoke(rlt);
        }
    }
}
