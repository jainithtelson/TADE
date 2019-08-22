using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
namespace TADE.Models
{
    public class ValidateFilesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
    {
        var files = value as IEnumerable<HttpPostedFileBase>;
        if (files == null)
        {
            return false;
        }
            bool returnType = true;
            foreach (var file in files)
            {
                if (file.ContentLength > 1 * 1024 * 1024)
                {
                    return false;
                }
                
                if (file.ContentType != "application/msword" && file.ContentType != "application/vnd.ms-excel" && file.ContentType != "text/plain" && file.ContentType != "application/pdf" && !file.ContentType.Contains("image"))
                {
                    returnType = false;
                }
                

                //if (file.ContentType=="Image")
                //{
                //    try
                //    {
                //        using (var img = Image.FromStream(file.InputStream))
                //        {
                //            return img.RawFormat.Equals(ImageFormat.Png);
                //        }
                //    }
                //    catch { }
                //}
                
               
            }
       

       
        return returnType;
    }
}
}