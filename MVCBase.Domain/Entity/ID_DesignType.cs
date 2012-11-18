using System;

namespace MVCBase.Domain.Entity
{
    //ID_DesignType
    public class ID_DesignType
    {

        /// <summary>
        /// Dt_Id
        /// </summary>
        public virtual int Dt_Id { get; set; }
        /// <summary>
        /// Dt_name
        /// </summary>
        public virtual string Dt_name { get; set; }
        /// <summary>
        /// Dt_orderby
        /// </summary>
        public virtual int? Dt_orderby { get; set; }

    }
}