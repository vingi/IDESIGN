using System;

namespace MVCBase.Domain.Entity
{
    public class Ba_News
    {
        /// <summary>
        /// Ns_ID
        /// </summary>
        public virtual int Ns_ID { get; set; }
        /// <summary>
        /// Ns_Title
        /// </summary>
        public virtual string Ns_Title { get; set; }
        /// <summary>
        /// Ns_SubTitle
        /// </summary>
        public virtual string Ns_SubTitle { get; set; }
        /// <summary>
        /// Ns_Content
        /// </summary>
        public virtual string Ns_Content { get; set; }
        /// <summary>
        /// Ns_BuildTime
        /// </summary>
        public virtual DateTime? Ns_BuildTime { get; set; }
        /// <summary>
        /// Ns_State
        /// </summary>
        public virtual bool Ns_State { get; set; }       
    }
}
