using System;

namespace MVCBase.Domain.Entity{
	 	//Fr_TopKnowAlls
		public class Fr_TopKnowAlls
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