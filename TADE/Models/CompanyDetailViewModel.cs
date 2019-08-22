using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TADE.Models
{
    public class CompanyDetailViewodel : CompanyDetail
    {
        [ValidateImageAttribute(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        public HttpPostedFileBase File { get; set; }
    }
}