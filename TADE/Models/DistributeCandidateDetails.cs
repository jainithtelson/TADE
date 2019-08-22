using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TADE.Models
{
    public class DistributeCandidateDetails
    {
        public int candidateId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string bookeddate { get; set; }
        public int SlotId { get; set; }
        public string slot { get; set; }
        public string examiner { get; set; }
    }
}