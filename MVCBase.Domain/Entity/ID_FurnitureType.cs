using System;

namespace MVCBase.Domain.Entity
{
    //ID_FurnitureType
    public class ID_FurnitureType
    {

        /// <summary>
        /// Ft_Id
        /// </summary>
        public virtual int Ft_Id { get; set; }
        /// <summary>
        /// Ft_name
        /// </summary>
        public virtual string Ft_name { get; set; }
        /// <summary>
        /// Ft_ orderby
        /// </summary>
        public virtual int? Ft_orderby { get; set; }

    }
}