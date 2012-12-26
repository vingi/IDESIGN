using System;

namespace MVCBase.Domain.Entity{
	 	//Ba_News
		public class Ba_News
	{
	
      	/// <summary>
		/// Ns_ID
        /// </summary>
        public virtual int Ns_ID { get; set; }        
		/// <summary>
		/// Ns_Type
        /// </summary>
        public virtual int? Ns_Type { get; set; }        
		/// <summary>
		/// Ns_Title
        /// </summary>
        public virtual string Ns_Title { get; set; }        
		/// <summary>
		/// Ns_Content
        /// </summary>
        public virtual string Ns_Content { get; set; }        
		/// <summary>
		/// Ns_PostTime
        /// </summary>
        public virtual DateTime? Ns_PostTime { get; set; }        
		/// <summary>
		/// Ns_State
        /// </summary>
        public virtual bool Ns_State { get; set; }        
		   
	}
}