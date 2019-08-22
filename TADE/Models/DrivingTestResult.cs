using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TADE.Models
{
    public class DrivingTestResult
    {
        //FullName = x.CandidateDetail.FirstName + " " + x.CandidateDetail.LastName,
        //        DrivingLicense = x.CandidateDetail.DrivingLicenseNumber,
        //        TotalScore=x.Score,
        //        Grade=x.Grade
        public string Date { get; set; }
        public string IPAddress { get; set; }
        public int ExamId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string DrivingLicense { get; set; }
        public string Address { get; set; }
        public int? TotalScore { get; set; }
        public int? Grade { get; set; }
       public string Explanation { get; set; }
    }
}