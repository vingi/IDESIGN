using System;

namespace MVCBase.Domain.Entity
{
    //ID_DesignStyleType
    public class ID_DesignStyleType
    {

        /// <summary>
        /// St_Id
        /// </summary>
        public virtual int St_Id { get; set; }
        /// <summary>
        /// St_name
        /// </summary>
        public virtual string St_name { get; set; }
        /// <summary>
        /// St_orderby
        /// </summary>
        public virtual int? St_orderby { get; set; }

    }
}