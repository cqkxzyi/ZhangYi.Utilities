

using PWMIS.DataMap.Entity;
using PWMIS.DataProvider.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF.Net
{
    public class DbBaseManage : LocalDbContext, IDisposable
    {
        #region 类句柄操作
        private IntPtr handle;
        private bool disposed = false;
        //关闭句柄
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Runtime.InteropServices.DllImport("Kernel32")]
        public extern static Boolean CloseHandle(IntPtr handle);

        /// <summary>
        /// 设置或获取Device的对像
        /// </summary>
        private static DbBaseManage instance;
        public static DbBaseManage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbBaseManage();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        /// <summary>
        /// 释放对象资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 释放对象资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (handle != IntPtr.Zero)
                {
                    CloseHandle(handle);
                    handle = IntPtr.Zero;
                }
            }
            disposed = true;
        }
        ~DbBaseManage()
        {
            Dispose(false);
        }
        #endregion

         


        /// <summary>
        /// 简单查询
        /// </summary>
        /// <returns></returns>
        public List<SODUser> getRegionList(string logName)
        {
            var outlist = new List<SODUser>();
            try
            {
                SODUser model = new SODUser()
                {
                    LogName = logName
                };
                OQL q = new OQL(model);
                q.Select().Where(model.LogName);
                outlist = new EntityQuery<SODUser>(CurrentDataBase).GetList(q);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return outlist;
        }

        /// <summary>
        /// 执行sql获取List<T>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public List<SODUser> GetListBySql(string sql, Dictionary<string, object> ps)
        {
            IDataParameter[] paras = new IDataParameter[ps.Count];
            int i = 0;
            foreach (string key in ps.Keys)
                paras[i++] = CurrentDataBase.GetParameter(key, ps[key]);

            var result = EntityQuery<SODUser>.QueryList(CurrentDataBase.ExecuteReader(CurrentDataBase.ConnectionString, System.Data.CommandType.Text, sql, paras));
            return result;
        }

        public void 序列号() {
            SODUser model =new SODUser();
            model.LogName = "aa";
            //序列化
            byte[] buffer = PdfNetSerialize.BinarySerialize(model);
            string tempString = Convert.ToBase64String(buffer);

            //反序列化（为实现）
            byte[] buffer2 = Convert.FromBase64String(tempString);
            SODUser customer2 = (SODUser)PdfNetSerialize.BinaryDeserialize(buffer2, typeof(SODUser));

        }




    }
}
