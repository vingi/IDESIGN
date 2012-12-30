using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBase.Common
{
    public class MVCCommon
    {
        /// <summary>
        /// 前台绑定checkbox
        /// </summary>
        /// <param name="istrue"></param>
        /// <returns></returns>
        public static string BindCheckBox_Html(bool istrue)
        {
            string checkstr = string.Empty;
            if (istrue)
                checkstr = "checked = \"checked\"";
            return checkstr;
        }

        /// <summary>
        /// controller接收checkbox值并转换
        /// </summary>
        /// <returns></returns>
        public static bool BindCheckBox_Entity(object obj)
        {
            bool istrue = true;
            if (obj == null)
                istrue = false;
            return istrue;
        }

        /// <summary>
        /// 前台绑定DDL对象.select
        /// </summary>
        /// <param name="selectid"></param>
        /// <param name="thisid"></param>
        /// <returns></returns>
        public static string BindSelect_Html(int selectid,int thisid)
        {
            string str = string.Empty;
            if (selectid.Equals(thisid))
                str = "selected=\"selected\"";
            return str;
        }
    }
}