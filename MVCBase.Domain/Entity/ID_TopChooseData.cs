using System;

namespace MVCBase.Domain.Entity
{
    //ID_TopChooseData
    public class ID_TopChooseData
    {

        /// <summary>
        /// Tc_Id
        /// </summary>
        public virtual int Tc_Id { get; set; }
        /// <summary>
        /// Tc_DcID_FK
        /// </summary>
        public virtual int? Tc_DcID_FK { get; set; }
        /// <summary>
        /// Tc_place
        /// </summary>
        public virtual int Tc_place { get; set; }

    }
}