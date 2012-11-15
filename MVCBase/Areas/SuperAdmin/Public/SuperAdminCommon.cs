using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCBase.Areas.SuperAdmin.Public
{
    public class SuperAdminCommon
    {
        /// <summary>
        /// 生成初始化JS,用于初始化左侧栏焦点位置
        /// </summary>
        /// <param name="LI_ID"></param>
        /// <param name="A_ID"></param>
        /// <returns></returns>
        public static string JSInit(string LI_ID, string A_ID)
        {
            StringBuilder sbjs = new StringBuilder();
            sbjs.Append("$(\"#" + LI_ID + "\").addClass(\"current\");" + System.Environment.NewLine);
            sbjs.Append("$(\"#" + A_ID + "\").addClass(\"current\");" + System.Environment.NewLine);
            sbjs.Append("$(\"#main-nav li a.current\").parent().find(\"ul\").slideToggle(\"slow\");" + System.Environment.NewLine);
            return sbjs.ToString();
        }
    }
}