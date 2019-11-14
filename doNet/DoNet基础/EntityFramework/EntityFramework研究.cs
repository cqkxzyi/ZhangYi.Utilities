using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using DoNet基础.EntityFramework.模型;
using System.Data.EntityClient;
using System.Data;

namespace DoNet基础.EntityFramework
{
    public class EntityFramework研究
    {
        HuNiEntities HuNiEntitiesDb = new HuNiEntities();
        EmptyModelContainer HuNiEntitiesEmpty = new EmptyModelContainer();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void DBFirst方式()
        {
            Base_Area _Base_Area = new Base_Area();
            _Base_Area.Code = "006";
            HuNiEntitiesDb.Base_Area.Add(_Base_Area);
            HuNiEntitiesDb.SaveChanges();

            var tempArea = from u in HuNiEntitiesDb.Base_Area select u;
            foreach (var item in tempArea)
            {
                Console.WriteLine(item);
            }

            //测试更新
            var area1 = tempArea.FirstOrDefault<Base_Area>();
            area1.Code = "122";
            HuNiEntitiesDb.SaveChanges();
        }

        public void ModelFirst方式()
        {
            //创建主实体
            Teacher _Teacher = new Teacher()
            {
                Name = "张毅",
                Phone = "12",
                Address = "重庆市"
            };
            HuNiEntitiesEmpty.Teacher.Add(_Teacher);

            //创建子实体
            Student _Student = new Student()
            {
                Name = "王同学",
                TeacherID = _Teacher.ID
            };
            HuNiEntitiesEmpty.Student.Add(_Student);
            HuNiEntitiesEmpty.SaveChanges();

        }

        public void 使用var和匿名类的查询操作()
        {
            IQueryable<Teacher> tempData1 = from t in HuNiEntitiesEmpty.Teacher select t;
            var tempData2 = from t in HuNiEntitiesEmpty.Teacher select new { myID = t.ID, t.Name };
        }

        public void 延迟加载()
        {
            var tempDate = from t in HuNiEntitiesEmpty.Teacher select t;//并没有执行SQL语句
            foreach (var teacher in tempDate)//在这里才使用SQL语句
            {
                Console.WriteLine("老师的名称：");
                Console.WriteLine(teacher.Name);

                foreach (var student in teacher.Student)
                {
                    Console.WriteLine("学生的名称：");
                    Console.WriteLine(student.Name);
                }
            }

            //下面使用JOIN左链接查询数据
            tempDate = from t in HuNiEntitiesEmpty.Teacher.Include("Student") select t;
            tempDate = from t in HuNiEntitiesEmpty.Teacher join t2 in HuNiEntitiesEmpty.Student on t.ID equals t2.TeacherID select t;

            //阻止延迟加载(使用ToList<T>()方法),数据被一次性查询到了内存中
            var tempDate1 = (from t in tempDate select t).ToList<Teacher>();
            var tempDate2 = (from t in tempDate1 where t.Name == "张毅" select t).ToList<Teacher>();
        }

        public void Lambda表达式()
        {
            var tempDate = HuNiEntitiesEmpty.Teacher.Where<Teacher>(t => t.Name == "张毅").FirstOrDefault<Teacher>();
            //分页例子,每页显示2行数据
            var tempDate2 = HuNiEntitiesEmpty.Teacher.OrderBy(t => t.Name).Skip<Teacher>(4 * 2).Take<Teacher>(2);

        }

        public void Select()
        {
            HuNiEntities huni = new HuNiEntities();
            


            //基于表达式的查询语法
            using (var edm = new HuNiEntities())
            {
                
                string esql = "select value c from NorthwindEntities.Customers as c order by c.CustomerID limit 10";
                
                //ObjectQuery<Teacher> objectQuery = edm.CreateQuery<Teacher>(esql);

                ObjectContext context = new ObjectContext("sql");
                ObjectQuery<Teacher> customers = new ObjectQuery<Teacher>(esql, context);

                IQueryable<Teacher> cust1 = from c in customers select c;

                //使用ObjectQuery类的ToTraceString()方法显示查询SQL语句
                customers.ToTraceString();
            }

            using (var edm = new EmptyModelContainer())
            {
                
                Teacher teacher = edm.Set<Teacher>().Find(3);
                Student arti = teacher.Student.ToList().Find(o => o.ID == 2);

                //取当前值
                DbPropertyValues currentValues = edm.Entry<Teacher>(teacher).CurrentValues;
                //取原值
                DbPropertyValues originalValues = edm.Entry<Teacher>(teacher).OriginalValues;
                //取数据库值
                DbPropertyValues databaseValues = edm.Entry<Teacher>(teacher).GetDatabaseValues();
             
            }

        }
    }


    public class EntityClient测试
    {
        public void Test()
        {
            string con = "name = EmptyModelContainer";

            using (EntityConnection econn = new EntityConnection(con))
            {
                
                string esql = "Select VALUE c from EmptyModelContainer.Teacher as c where c.CustomerID='ALFKI'";

                econn.Open();

                EntityCommand ecmd = new EntityCommand(esql, econn);

                EntityDataReader ereader = ecmd.ExecuteReader(CommandBehavior.SequentialAccess);

                if (ereader.Read())
                {
                    Console.WriteLine(ereader["CustomerID"]);
                }

                Console.WriteLine(ecmd.ToTraceString());

            }
        }
    }






}
