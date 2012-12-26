using System;

namespace MVCBase.Domain.Entity{
	 	//Ba_NewsType
		public class Ba_NewsType
	{
	
      	/// <summary>
		/// Nt_ID
        /// </summary>
        public virtual int Nt_ID { get; set; }        
		/// <summary>
		/// Nt_TypeName
        /// </summary>
        public virtual string Nt_TypeName { get; set; }        
		/// <summary>
		/// Nt_ListOrder
        /// </summary>
        public virtual int? Nt_ListOrder { get; set; }        
		   
	}
}