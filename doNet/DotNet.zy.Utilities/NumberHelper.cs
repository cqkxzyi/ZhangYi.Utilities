using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities
{
    public class NumberHelper
    {
        /// <summary>
        /// 保留两位小数 （截取）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal Save2Point(decimal value)
        {
            decimal temp = value * 100;
            string tempStr = temp.ToString();
            if (tempStr.IndexOf('.') > 0)
            {
                tempStr = tempStr.Substring(0, tempStr.IndexOf('.'));
            }
            return decimal.Parse(tempStr) / 100;
        }
    }
}
