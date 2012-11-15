using System;

namespace MVCBase.Domain.Entity
{
    public class Ba_Admin
    {
        /// <summary>
        /// Ad_ID
        /// </summary>
        public virtual int Ad_ID { get; set; }
        /// <summary>
        /// Ad_AdminName
        /// </summary>
        public virtual string Ad_AdminName { get; set; }
        /// <summary>
        /// Ad_AdminPwd
        /// </summary>
        public virtual string Ad_AdminPwd { get; set; }
        /// <summary>
        /// Ad_LastLoginTime
        /// </summary>
        public virtual DateTime? Ad_LastLoginTime { get; set; }
        /// <summary>
        /// Ad_BuildTime
        /// </summary>
        public virtual DateTime? Ad_BuildTime { get; set; }
        /// <summary>
        /// Ad_State
        /// </summary>
        public virtual int? Ad_State { get; set; }        

    }
}