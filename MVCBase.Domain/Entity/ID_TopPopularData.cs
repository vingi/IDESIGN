using System;

namespace MVCBase.Domain.Entity
{
    //ID_TopPopularData
    public class ID_TopPopularData
    {

        /// <summary>
        /// Tp_Id
        /// </summary>
        public virtual int Tp_Id { get; set; }
        /// <summary>
        /// Tp_DcID_FK
        /// </summary>
        public virtual int? Tp_DcID_FK { get; set; }
        /// <summary>
        /// Tp_place
        /// </summary>
        public virtual int Tp_place { get; set; }

    }
}