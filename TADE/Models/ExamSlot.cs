//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TADE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExamSlot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamSlot()
        {
            this.AvailableSeatsForSlots = new HashSet<AvailableSeatsForSlot>();
            this.CandidateExamBookingDetails = new HashSet<CandidateExamBookingDetail>();
        }
    
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public string Timefrom { get; set; }
        public string TimefromMinutes { get; set; }
        public string TimeTo { get; set; }
        public string TimeToMinutes { get; set; }
        public Nullable<bool> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AvailableSeatsForSlot> AvailableSeatsForSlots { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateExamBookingDetail> CandidateExamBookingDetails { get; set; }
    }
}
