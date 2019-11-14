using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DoNet基础.反射
{
    //Type类
    public class MyClass
    {
        public int MyField;
        public void MyMethod() { }
        public string MyProperty { get; set; }
    }
    class Program
    {
        static void Main2(string[] args)
        {
            MyClass obj = new MyClass(); //创建对象

            //获取类型对象
            Type typ = obj.GetType();
            //输出类的名字
            Console.WriteLine(typ.Name);
            //判断其是否公有的
            Console.WriteLine(typ.IsPublic);
            //判断两个对象是否属于同一类型
            Console.WriteLine(IsTheSameType(obj, new MyClass()));
            Console.ReadKey();
        }
        static bool IsTheSameType(Object obj1, Object obj2)
        {
            return (obj1.GetType() == obj2.GetType());
        }


        //获取MyClass.dll中UserInfo类的各个成员
        public void ReflecForClass()
        {
            Assembly assembly = Assembly.LoadFrom("MyClass.dll");
            object obj = assembly.CreateInstance("MyClass.UserInfo");
            Type type = obj.GetType();
            //构造函数
            ConstructorInfo[] cons = type.GetConstructors();
            string constructTemp = "";
            foreach (ConstructorInfo con in cons)
            {
                constructTemp += con.ToString() + "\r\n";
            }
            
            //方法
            MethodInfo[] meths = type.GetMethods();
            string methodTemp = "";
            foreach (MethodInfo meth in meths)
            {
                methodTemp += meth.ToString() + "\r\n";
            }
           
            //属性
            PropertyInfo[] props = type.GetProperties();
            string propertyTemp = "";
            foreach (PropertyInfo prop in props)
            {
                propertyTemp += prop.ToString() + "\r\n";
            }
            
            //字段
            FieldInfo[] fields = type.GetFields();
            string fieldTemp = "";
            foreach (FieldInfo field in fields)
            {
                fieldTemp += field.ToString() + "\r\n";
            }
           
        }
    }

}




//MyClass定义如下
namespace MyClass
{
    public class UserInfo
    {
        string strT;
        public UserInfo()
        {
            strT = "aaa";
        }
        public UserInfo(string a, string b)
        {
            strT = a + b;
        }
        public int Add(int i)
        {
            return i;
        }
        public int Subtract(int i, int j)
        {
            return i - j;
        }
        public string Name
        {
            get;
            set;
        }
        public string Age
        {
            get;
            set;
        }
        public string Tel;
        public string Mobile;

    }
}
