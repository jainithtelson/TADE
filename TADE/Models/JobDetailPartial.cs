using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TADE.Models
{
    public partial class JobDetail
    {
        [ValidateFiles(ErrorMessage = "Please select files(only word,excel,pdf, text and image) smaller than 1MB")]
       // [File(AllowedFileExtensions = new string[] { ".jpg", ".gif", ".tiff", ".png", ".pdf", ".wav" }, MaxContentLength = 1024 * 1024 * 8, ErrorMessage = "Invalid File")]
        public HttpPostedFileBase[] files { get; set; }
    }
}