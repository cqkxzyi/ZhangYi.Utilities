using PWMIS.DataMap.Entity;
using PWMIS.DataProvider.Adapter;
using PWMIS.DataProvider.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PWMIS.DataMap.Entity.OQLCompare;

namespace PDF.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "测试马上开始";
            //TestCreateTable();
            //增加数据1();
            //增加数据2();

            //简单查询();
            //复杂查询();
            //分页查询();
            //连表查询();
            执行存储过程();

            //更新();

            //删除();
        }


        #region 创建
        public static void TestCreateTable()
        {

            //创建数据库和表
            LocalDbContext context = new LocalDbContext();

            ////重新指定主键，删除旧的测试数据 
            //SODUser oldUser = new SODUser();
            //oldUser.PrimaryKeys.Clear();
            //oldUser.PrimaryKeys.Add("LogName");
            //oldUser["LogName"] = "zhang san"; //索引器使用 
            //int count = context.Remove<SODUser>(oldUser);

            //SODUser zhang_san = new SODUser() { ID=1, LogName = "zhang san", LogPwd = "123" };
            //count = context.Add<SODUser>(zhang_san);//采用 DbContext 方式插入数据 
        }

        public static void 增加数据1()
        {
            LocalDbContext context = new LocalDbContext();

            SODUser zhang_san = new SODUser() { LogName = "zhang san", LogPwd = "123" };
            int count = context.Add<SODUser>(zhang_san);//采用 DbContext 方式插入数据 
            获取执行结果日志();
        }

        public static void 增加数据2() {
            SODUser user = new SODUser() {
                LogName = "wangfeng2",
                UserName = "网峰2",
                Mobile = "13888888888"
            };
            EntityQuery<SODUser>.Instance.Insert(user);
        }

        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        #region 查询

        public static void 简单查询() {
            List<SODUser> result = null;

            //方式 params
            SODUser user = new SODUser() { LogName = "zhang san" };
            OQL q = OQL.From(user)
              .Select()
              .Where(user.LogName)
              .END;
            result = EntityQuery<SODUser>.QueryList(q);

            //方式 OQLCompare
            SODUser user2 = new SODUser();
            OQL q2 = OQL.From(user2);
            OQLCompare oqlCompare = new OQLCompare(q2);
            oqlCompare.Comparer(user2.ID, ">", 0);

            //OQLCompare oqlCompare2 = new OQLCompare(user2).Comparer(user2.ID, OQLCompare.CompareType.Greater, 0);
            //q2 = q2.Select().Where(oqlCompare2).END;

            q2 = q2.Select().Where(oqlCompare).END;
            result = EntityQuery<SODUser>.QueryList(q2);
            
        }

        public static void 复杂查询()
        {
            SODUser user = new SODUser();
            OQL q = OQL.From(user)
              .Select()
              .Where(cmp => cmp.Property(user.LogName) == "zhangyi" & cmp.Comparer(user.ID, "like", 1))
              .END;

            List<SODUser> users = EntityQuery<SODUser>.QueryList(q);

            string sql = q.ToString();
            sql = q.PrintParameterInfo();
        }

        public static void 分页查询()
        {
            SODUser user = new SODUser();

            OQL q = new OQL(user);

            OQLCompareFunc resultFunc = cmp =>
            {
                OQLCompare resultCmp = cmp.Comparer(user.ID, ">", 0);
                resultCmp = resultCmp & cmp.Comparer(user.LogName, "like", "%zhang%");
                return resultCmp;
            };

            q.Select().Where(resultFunc).OrderBy(user.ID);
            //分页
            q.PageEnable = true;
            q.PageWithAllRecordCount = 10;
            q.Limit(10, 1, true);
            var outlist = EntityQuery<SODUser>.QueryList(q);
            int pageCount = q.PageWithAllRecordCount;
        }

        public static void 连表查询in()
        {
            SODUser user = new SODUser();
            OQL q = new OQL(user);
            OQLCompareFunc resultFunc = cmp =>
            {
                OQLCompare resultCmp = cmp.Comparer(user.ID, ">", 2);
                resultCmp = resultCmp & cmp.Comparer(user.LogName, "like", "%zhang%");
                return resultCmp;
            };
            q.Select(user.ID).Where(resultFunc);


            SODUserRemark userRemark = new SODUserRemark();
            OQL r = new OQL(userRemark);
            OQLCompareFunc resultFunc2 = cmp =>
            {
                OQLCompare resultCmp = cmp.Comparer(userRemark.UserId, "IN", q);

                return resultCmp;
            };
            r.Select().Where(resultFunc2).OrderBy(userRemark.ID, "desc");

            var outlist = EntityQuery<SODUserRemark>.QueryList(r);
        }

        ///// <summary>
        ///// 未实现
        ///// </summary>
        //public static void 连表查询InnerJoin()
        //{
        //    SODUser user = new SODUser();
        //    SODUserRemark userRemark = new SODUserRemark();

        //    var q = OQL.From(user)
        // .InnerJoin(userRemark)
        // .On(user.ID, userRemark.UserId)
        // .Select(user.ID, user.LogName, userRemark.Remark); //选取指定的字段

        //    AdoHelper db = MyDB.GetDBHelperByConnectionName("connStr");

        //    EntityContainer ec = new EntityContainer(q, db);
        //    ec.Execute(); //可以省略此行调用
        //    var mapUser1 = ec.Map<SODUser>().ToList();
        //    var mapGroup1 = ec.Map<SODUserRemark>().ToList();
        //}

        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~



        #region 执行
        public static void 执行存储过程()
        {
            AdoHelper db = MyDB.GetDBHelperByConnectionName("connStr");
            var ds1 = db.ExecuteDataSet("[GetSodUser]", CommandType.StoredProcedure, new System.Data.IDataParameter[] { db.GetParameter("@id", 1) });


        }
        #endregion

        #region 更新
        public static void 更新() {
            SODUser user = new SODUser();
            OQL q = OQL.From(user)
              .Select()
              .Where(cmp => cmp.Property(user.ID) == 4 )
              .END;
            SODUser updateUser = EntityQuery<SODUser>.QueryObject(q);
            updateUser.LogPwd = "111111111";

            //方式一
           int result= EntityQuery<SODUser>.Instance.Update(updateUser);


            ////方式二
            //EntityQuery<SODUser> eq = new EntityQuery<SODUser>(user);
            //            result= eq.SaveAllChanges();


            //方式三
            updateUser.LogPwd = "aaaaaaaa";
             OQL q2 = new OQL(updateUser);
            q2.Update(updateUser.LogPwd).Where(updateUser.ID);
            //结果大于0就成功
            result =  EntityQuery<SODUser>.Instance.ExecuteOql(q2);

        }
        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        #region 删除

        public static void 删除() {
            SODUser user = new SODUser();

            OQL q = OQL.From(user).Delete().Where(c => c.Comparer(user.ID, ">", 5)).END;
            int result = EntityQuery<SODUser>.Instance.ExecuteOql(q);
            获取执行结果日志();
        }

        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        #region 其他
        public static void 获取执行结果日志() {
            

            // CommandLog log = new CommandLog();
            string a = PWMIS.DataProvider.Data.CommandLog.Instance.CommandText;




           
        }
        #endregion


    }
}
