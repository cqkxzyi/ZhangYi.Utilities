using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础
{
    public class LazyTest
    {
        public LazyTest() {
            Lazy<Student> stu = new Lazy<Student>();
            if (!stu.IsValueCreated)
                Console.WriteLine("Student isn't init!");
            Console.WriteLine(stu.Value.Name);
            stu.Value.Name = "Tom";
            stu.Value.Age = 21;
            Console.WriteLine(stu.Value.Name);
            Console.Read();
        }
    }

    public class Student
    {
        public Student()
        {
            this.Name = "DefaultName";
            this.Age = 0;
            Console.WriteLine("Student is init...");
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }

}
