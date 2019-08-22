using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TADE.Models;
using TADE.CustomResult;

using System.Net.Mail;
using System.Web.UI;

namespace TADE.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult TestRecordVideo()
        {
            //https://www.pubnub.com/blog/2014-10-21-building-a-webrtc-video-and-voice-chat-application/
            //http://www.html5rocks.com/en/tutorials/getusermedia/intro/
            //http://www.html5rocks.com/en/tutorials/webrtc/basics/
            return View();
        }
        public ActionResult Index()
        {
            return new VideoResult();
        }

        public ActionResult TestVideo()
        {
            return View();
        }
        public ActionResult TestVideoChat()
        {
            return View();
        }
        public ActionResult TestPDFConvertor()
        {
           
            DrivingTestResult DR = new Models.DrivingTestResult();

            DR.Date = DateTime.Now.ToString();
            DR.IPAddress = "1:168";
            DR.ExamId = 3424;
            DR.DateOfBirth = "21/12/19986";
            DR.FirstName = "Ava";
            DR.MiddleName = "Mac";
            DR.LastName = "Donald";
            DR.Email = "jainith@gmail.com";
            DR.Address = "AddressLine1< br />AddressLine2 < br /> AddressLine3  < br /> PostCode";
            DR.DrivingLicense = "fgdgfd";
            DR.Explanation = "You have failed to answer an important question What is the meaning of this symbol on a motorway?. <br /> You have answered cBright sun ahead, difficult to drive. <br /> The correct answer is You must travel only on this lane. <br /> Unfortunately this time you lost your score as you have to ..... ";
            DR.TotalScore = 0;
            DR.Grade = 0;
            SendMail(DR);
            return View();
        }
        public byte[] GetPDF(string pHTML)
        {
            byte[] bPDF = null;
            
            MemoryStream ms = new MemoryStream();
            //FileStream fs = new FileStream(thesavePath, FileMode.Create);
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
              PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);
            // PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("CandidateExamResults") + "PDFfile.pdf", FileMode.Create));
            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }
        public ActionResult datepickerdemo()
        {
            DateandTimeExtract();
            return View();
        }
        public void DateandTimeExtract()
        {
            string Timefrom = "6";
            string dtex = DateTime.Now.ToShortDateString();
            TimeSpan tcurrent = DateTime.Now.TimeOfDay;
            int thbookedrange = Convert.ToInt32(Timefrom) - 1;
            int thbookedExact = Convert.ToInt32(Timefrom);
            TimeSpan tbookedrange = new TimeSpan(thbookedrange, 0, 0);
            TimeSpan tbookedExact = new TimeSpan(thbookedExact, 0, 0);
            if (tbookedrange <= tcurrent && tcurrent <= tbookedExact)
            {
                string testpass = "success";
            }
        }
        //mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "iTextSharpPDF.pdf"));

        public bool SendMail(DrivingTestResult dr)
        {
            string message = "";
            //Here we will save data to the database

            //check username available
     
            if (dr.Email != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                       
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<h1 style='text - align: center; '>TADE EXAM RESULT</h1><p>&nbsp; &nbsp; &nbsp; &nbsp;</p>");
                        sb.Append("<table style='width: 100 % '><tbody><tr><td colspan='2'><h2>APPLICANT DETAILS</h2></td></tr><tr><td>1. FIRST NAME:</td><td>");
                        sb.Append(dr.FirstName);
                        sb.Append("</td></tr><tr><td>2. MIDDLE NAME:</td><td>");
                        sb.Append(dr.MiddleName);

                        sb.Append("</td></tr><tr><td>3. LAST NAME:</td><td>");
                        sb.Append(dr.LastName);

                        sb.Append("</td></tr><tr><td>4. DATE OF BIRTH</td><td>");
                        sb.Append(dr.DateOfBirth);
                        sb.Append("</td></tr><tr><td>5. DRIVING LICENCE No.</td><td>");
                        sb.Append(dr.DrivingLicense);

                        sb.Append("</td></tr><tr><td>6. ADDRESS</td><td>");
                        sb.Append(dr.Address);
                        sb.Append(" </td></tr><tr><td colspan='2'><h2>EXAM DETAILS</h2></td></tr><tr><td>7. REGISTRATION No.</td><td>");
                        sb.Append(dr.ExamId);
                        sb.Append("</td></tr><tr><td>8. IP ADDRESS OF COMPUTER TEST ATTENDED</td><td>");
                        sb.Append(dr.IPAddress);

                        sb.Append("</td></tr><tr><td>9. EXAMINER ID</td><td>");
                        sb.Append("&nbsp;");
                        sb.Append("</td></tr><tr><td>10. TIME OF EXAM</td><td>");
                        sb.Append("&nbsp;");

                        sb.Append("</td></tr><tr><td>11. DURATION OF EXAM</td><td>");
                        sb.Append("&nbsp;");
                        sb.Append(" </td></tr><tr><td>12. EXAM GRADE</td><td>");
                        sb.Append(dr.Grade);

                        sb.Append("</td></tr><tr><td>13. EXAM RESULT VALID TILL</td><td>");
                        sb.Append("&nbsp;");
                        sb.Append(" </td></tr><tr><td>14. COMMENTS</td><td>");
                        sb.Append(dr.Explanation);
                        sb.Append("</td></tr></tbody></table>");

                      
                        StringReader sr = new StringReader(sb.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                            pdfDoc.Open();
                            htmlparser.Parse(sr);
                            pdfDoc.Close();
                            byte[] bytes = memoryStream.ToArray();
                            memoryStream.Close();
                            string thesavePath = Server.MapPath("~/CandidateExamResults") + "\\" + dr.ExamId.ToString() + ".pdf";
                            System.IO.File.WriteAllBytes(thesavePath, bytes);
                            MailModel mm = new Models.MailModel();
                            mm.To = dr.Email;
                            mm.From = "jainith@gmail.com";
                            mm.Body = "Dear " + dr.FirstName + ", <br /> <br />Congratulations! You have successfully completed TADE exam. Please find attached certificate for future reference. <br /><br /> Kind Regards <br /> TAPI Admin Team";
                            mm.Subject = "TADE exam result";
                            message = "Success";
                            SendMailBLL sm = new SendMailBLL();
                            sm.SendMail(mm, bytes);
                        }

                    }
                }


            }
            else
            {
                message = "Failed!";
            }
            return true;
        }
        //public void DownloadPDF(string HTMLContent)
        //{
        //    // string HTMLContent = "Hello <b>World</b>";
        //    string thesavePath = Server.MapPath("~/CandidateExamResults") + "\\" + "PDFfile.pdf";
        //    Response.Clear();
        //    Response.ContentType = "application/pdf";

        //    Response.AddHeader("content-disposition", "attachment;filename=" + "PDFfile.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.BinaryWrite(GetPDF(HTMLContent));
        //    Response.WriteFile(thesavePath);
        //    Response.End();

        //    //Response.ClearContent();
        //    //Response.ClearHeaders();
        //    //Response.ContentType = "Application/pdf";
        //    //try
        //    //{
        //    //    Response.WriteFile(MapPath("" +
        //    //                  Request.Params["File"].ToString()));
        //    //    Response.Flush();
        //    //    Response.Close();
        //    //}
        //    //catch
        //    //{
        //    //    Response.ClearContent();
        //    //}
        //}
        //private void SendPDFEmail(DataTable dt)
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //        {
        //            string companyName = "ASPSnippets";
        //            int orderNo = 2303;
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
        //            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Order Sheet</b></td></tr>");
        //            sb.Append("<tr><td colspan = '2'></td></tr>");
        //            sb.Append("<tr><td><b>Order No:</b>");
        //            sb.Append(orderNo);
        //            sb.Append("</td><td><b>Date: </b>");
        //            sb.Append(DateTime.Now);
        //            sb.Append(" </td></tr>");
        //            sb.Append("<tr><td colspan = '2'><b>Company Name :</b> ");
        //            sb.Append(companyName);
        //            sb.Append("</td></tr>");
        //            sb.Append("</table>");
        //            sb.Append("<br />");
        //            sb.Append("<table border = '1'>");
        //            sb.Append("<tr>");
        //            foreach (DataColumn column in dt.Columns)
        //            {
        //                sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>");
        //                sb.Append(column.ColumnName);
        //                sb.Append("</th>");
        //            }
        //            sb.Append("</tr>");
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                sb.Append("<tr>");
        //                foreach (DataColumn column in dt.Columns)
        //                {
        //                    sb.Append("<td>");
        //                    sb.Append(row[column]);
        //                    sb.Append("</td>");
        //                }
        //                sb.Append("</tr>");
        //            }
        //            sb.Append("</table>");
        //            StringReader sr = new StringReader(sb.ToString());

        //            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        //                pdfDoc.Open();
        //                htmlparser.Parse(sr);
        //                pdfDoc.Close();
        //                byte[] bytes = memoryStream.ToArray();
        //                memoryStream.Close();

        //                MailMessage mm = new MailMessage("sender@gmail.com", "receiver@gmail.com");
        //                mm.Subject = "iTextSharp PDF";
        //                mm.Body = "iTextSharp PDF Attachment";
        //                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "iTextSharpPDF.pdf"));
        //                mm.IsBodyHtml = true;
        //                SmtpClient smtp = new SmtpClient();
        //                smtp.Host = "smtp.gmail.com";
        //                smtp.EnableSsl = true;
        //                NetworkCredential NetworkCred = new NetworkCredential();
        //                NetworkCred.UserName = "sender@gmail.com";
        //                NetworkCred.Password = "<password>";
        //                smtp.UseDefaultCredentials = true;
        //                smtp.Credentials = NetworkCred;
        //                smtp.Port = 587;
        //                smtp.Send(mm);
        //            }
        //        }
        //    }
        //}
    }
}