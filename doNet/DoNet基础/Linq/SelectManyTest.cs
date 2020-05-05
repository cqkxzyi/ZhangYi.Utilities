using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础.Linq
{
    public class SelectManyTest
    {

        public void Test()
        {
            //使用集合初始化器初始化Teacher集合
            List<Teacher> teachers = new List<Teacher> {
               new Teacher("徐老师",
               new List<Student>(){
                 new Student("宋江",80),
                new Student("卢俊义",11),
                new Student("朱武",12)
               }
               ),
                new Teacher("姜老师",
               new List<Student>(){
                 new Student("林冲",90),
                new Student("花荣",21),
                new Student("柴进",22)
               }
               ),
                new Teacher("樊老师",
               new List<Student>(){
                 new Student("关胜",100),
                new Student("阮小七",31),
                new Student("时迁",32)
               }
               )
            };

            //问题：查询Score小于60的学生
            //方法1：循环遍历、会有性能的损失
            foreach (Teacher t in teachers)
            {
                foreach (Student s in t.Students)
                {
                    if (s.Score < 60)
                    {
                        Console.WriteLine("1姓名:" + s.StudentName + ",成绩:" + s.Score);
                    }
                }
            }

            //查询表达式
            //方法2：使用SelectMany  延迟加载：在不需要数据的时候，就不执行调用数据，能减轻程序和数据库的交互，可以提供程序的性能，执行循环的时候才去访问数据库取数据            
            //直接返回学生的数据
            var query = from t in teachers
                        from s in t.Students
                        where s.Score < 60
                        select s;
            foreach (var item in query)
            {
                Console.WriteLine("2姓名:" + item.StudentName + ",成绩:" + item.Score);
            }
            //只返回老师的数据
            var query1 = from t in teachers
                         from s in t.Students
                         where s.Score < 60
                         select new
                         {
                             teacherName = t.TeacherName,
                             student = t.Students.Where(p => p.Score < 60).ToList()
                         };
            foreach (var item in query1)
            {
                Console.WriteLine("3老师姓名:" + item.teacherName + ",学生姓名:" 
                    + item.student.Select(c=>c.StudentName).Aggregate( (a,b) => a+","+b ) + ",成绩:" + item.student.Select(c=>c.Score.ToString()).Aggregate((a,b)=>a+","+b));
            }
            // 使用匿名类 返回老师和学生的数据
            var query2 = from t in teachers
                         from s in t.Students
                         where s.Score < 60
                         select new { teacherName = t.TeacherName, studentName = s.StudentName, studentScore = s.Score };
            foreach (var item in query2)
            {
                Console.WriteLine("4老师姓名:" + item.teacherName + ",学生姓名:" + item.studentName + ",成绩:" + item.studentScore);
            }

            //使用查询方法
            var query3 = teachers.SelectMany(p => p.Students.Where(t => t.Score < 60).ToList());
            foreach (var item in query3)
            {
                Console.WriteLine("5姓名:" + item.StudentName + ",成绩:" + item.Score);
            }


            string[] text = { "Today is 2018-06-06", "weather is sunny", "I am happy" };
            var tokens = text.Select(s => s.Split(' '));
            foreach (string[] line in tokens)
                foreach (string token in line)
                    Console.Write("{0}.", token);

            var tokens1 = text.SelectMany(s => s.Split(' '));
            foreach (string token in tokens1)
                    Console.Write("{0}.", token);


        }

        
    }



    /// <summary>
    /// 学生类
    /// </summary>
    public class Student
     {
         //姓名
         public string StudentName { get; set; }
         //成绩
         public int Score { get; set; }
         //构造函数
         public Student(string name, int score)
         {
             this.StudentName = name;
             this.Score = score;
         }
     }

    /// <summary>
    /// Teacher类
    /// </summary>
    public class Teacher
    {
        //姓名
        public string TeacherName { get; set; }
        //学生集合
        public List<Student> Students { get; set; }

        public Teacher(string name, List<Student> students)
        {
            this.TeacherName = name;
            this.Students = students;
        }
    }



}
