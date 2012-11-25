using System;

namespace MVCBase.Domain.Entity
{
    //ID_TopStarData
    public class ID_TopStarData
    {

        /// <summary>
        /// Ts_Id
        /// </summary>
        public virtual int Ts_Id { get; set; }
        /// <summary>
        /// Ts_DcID_FK
        /// </summary>
        public virtual int? Ts_DcID_FK { get; set; }
        /// <summary>
        /// Ts_place
        /// </summary>
        public virtual int Ts_place { get; set; }

    }
}