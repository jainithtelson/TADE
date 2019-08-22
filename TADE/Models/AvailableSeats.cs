using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TADE.Models
{
    public class AvailableSeats
    {
        public int AvailableSeatsForSlotsId { get; set; }
        public int? SlotId { get; set; }
        public string SlotName { get; set; }
        public int? AvailableDatesId { get; set; }
        public int? TotalSeats { get; set; }
        public int? RemainingSeats { get; set; }
        public bool? Status { get; set; }
    }
}