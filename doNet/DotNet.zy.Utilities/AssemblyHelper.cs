using System;
using System.Web;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Data;



namespace DotNet.zy.Utilities
{ 
public class 反射示例
{
    String assemblyName = ConfigurationManager.AppSettings["DAL"];
    String className = "BLL.RoleBLL";
    public void 反射静态函数示例()
    {

        Assembly assembly = Assembly.Load(assemblyName);
        Type type = assembly.GetType(assemblyName + "." + className);
        //MethodInfo method = type.GetMethod("GetUserRoleList", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        MethodInfo method = type.GetMethod("GetUserRoleList");


        Object[] param = new Object[] { 111, "asdf" };
        method.Invoke(null, new object[] { param }); //第一个参数忽略
    }

    public void 反射函数示例()
    { 
        Assembly assembly = Assembly.Load(assemblyName); 
        Type type = assembly.GetType(assemblyName + "." + className);

        Object[] obj = new Object[] { "param1", "param2" };//实例化Object类型里面的两个参数作为实例化该类的构造函数的参数。
        var t = Activator.CreateInstance(type, obj); //通过本类的类型和构造函数的参数，实例化了一个新的对象。
  
        MethodInfo method = type.GetMethod("GetData", BindingFlags.Instance | BindingFlags.Public);//获取该类的所调用方法的索引  
        Object[] param = new Object[] { 111, "asdf" };
        DataTable db = (DataTable)method.Invoke(t, new object[] { param });//执行调用方法，然后把调用的值返回。

    }



    public void 反射示例3()
    {
        Assembly assembly = Assembly.Load(assemblyName);
        Type type = assembly.GetType(assemblyName + "." + className);
        //使用无参构造函数动态创建对象
        var objNull = type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);

        //调用使用了两个string参数的构造函数动态创建对象
        var frankJob = type.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { "job", "frank" });

        //调用公有成员属性get方法
        var fileName = type.InvokeMember("FirstName", BindingFlags.GetProperty, null, frankJob, null);

        //调用公有成员属性set方法
        type.InvokeMember("Email", BindingFlags.SetProperty, null, frankJob, new object[] { "gyzdfasddfsafhao@vervidian.com" });

        //调用无参数方法
        var objStr = type.InvokeMember("ToString", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, frankJob, null);
      
        //调用带参数的方法
        var email = type.InvokeMember("GetEmail", BindingFlags.InvokeMethod, null, frankJob, new object[] { "sunshine" });
    }

    static void GetProperties<T>(T t)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);//获取T的类型，然后通过BindingFlags获取类型的Public属性  
        foreach (var p in properties)
        {
            var result = p.Name + ": " + p.GetValue(t, null).ToString();//循环该properties输出每一项的name和相应的值。  
            Console.WriteLine(result);//进行页面终端输出
        }
    }  


}




    public sealed class DataProviderInitializer
    {
        private readonly String assemblyName;
        private readonly Assembly assembly;
        private static readonly Object mutex = new Object();


        public DataProviderInitializer()
        {
            assemblyName = ConfigurationManager.AppSettings["DAL"];
            assembly = Assembly.Load(assemblyName);
        }

        /// <summary>
        /// 使用反射创建普通实例
        /// </summary>
        /// <typeparam name="T">被反射对象实现的接口</typeparam>
        /// <param name="className">被创建实例的类名</param>
        /// <returns>被反射对象实现的接口</returns>
        public T CreateInstance<T>(String className) where T : class
        {

            var fullyQualifiedName = assemblyName + "." + className;
            return (T)assembly.CreateInstance(fullyQualifiedName);
        }


        /// <summary>
        /// 使用反射创建泛型实例
        /// </summary>
        /// <typeparam name="T">被反射对象实现的接口</typeparam>
        /// <param name="className">被创建实例的类名</param>
        /// <param name="typeArguments">创建泛型实例所需的一个或多个泛型类型</param>
        /// <returns>被反射对象实现的接口</returns>
        public T CreateInstance<T>(String className, params Type[] typeArguments) where T : class
        {
            var fullyQualifiedName = assemblyName + "." + className + "`" + typeArguments.Length;

            var genericType = Assembly.Load(assemblyName)
                .GetType(fullyQualifiedName)
                .MakeGenericType(typeArguments);
            var instance = (T)Activator.CreateInstance(genericType);


            return instance;
        }
    }
}