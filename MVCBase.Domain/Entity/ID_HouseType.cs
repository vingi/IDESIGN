using System;

namespace MVCBase.Domain.Entity
{
    //ID_HouseType
    public class ID_HouseType
    {

        /// <summary>
        /// Ht_Id
        /// </summary>
        public virtual int Ht_Id { get; set; }
        /// <summary>
        /// Ht_name
        /// </summary>
        public virtual string Ht_name { get; set; }
        /// <summary>
        /// Ht_orderby
        /// </summary>
        public virtual int? Ht_orderby { get; set; }

    }
}