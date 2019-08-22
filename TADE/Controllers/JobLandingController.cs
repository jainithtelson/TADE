using CaptchaMvc.HtmlHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TADE.Models;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace TADE.Controllers
{
    public class JobLandingController : Controller
    {
        private TADEDBEntities db = new TADEDBEntities();
        // GET: JobLanding
        public ActionResult ViewCompanies()
        {
           if(User.Identity.GetUserId() !=null && Convert.ToInt32(Session["CompanyId"]) !=0)
            {
                string role = Convert.ToString(Session["role"]);
                if (role == "WorkSeeker")
                {
                    return RedirectToAction("ViewJobs", "JobLanding");
                }
                var companyDetails = db.CompanyDetails.Where(j => j.RoleType != role);
                ViewBag.Role = role;
                return View(companyDetails.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public ActionResult ViewJobs()
        {
            if (User.Identity.GetUserId() != null && Convert.ToInt32(Session["CompanyId"]) != 0)
            {
                string role = Convert.ToString(Session["role"]);
                if (role == "WorkProvider")
                {
                    return RedirectToAction("ViewCompanies", "JobLanding");
                }
                int companyId = Convert.ToInt32(Session["CompanyId"]);
                var jobDetails = db.JobDetails.Where(j => j.Status == true);
                return View(jobDetails.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult CompanyDetails(int? companyId)
        {
            if (companyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = db.CompanyDetails.Find(companyId);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }
        public ActionResult PostJob()
        {
            return View();
        }
        //JobId,JobTile,Location,Fees,JobDescription,ContactPerson,ContactNumber,Email,Status,NatureOfWork,JobDetailsAttachmentUrl,ExpectedDuration
        [HttpPost]
        public ActionResult PostJob(JobDetail jobDetail)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {

                if (ModelState.IsValid)
                {
                    int CompanyId = Convert.ToInt32(Session["CompanyId"]);
                    if (CompanyId != 0)
                    {
                        CompanyDetail companyDetail = db.CompanyDetails.Find(CompanyId);
                        var path = "~/JobDetailAttachments/"+ companyDetail.CompanyName+"_"+CompanyId;
                        Directory.CreateDirectory(Server.MapPath(path));

                        foreach (HttpPostedFileBase file in jobDetail.files)
                        {
                            //Checking file is available to save.  
                            if (file != null)
                            {
                                var InputFileName = Path.GetFileName(file.FileName);

                                var ServerSavePath = Path.Combine(Server.MapPath(path+"/") + InputFileName);
                                //Save file to server folder  
                                file.SaveAs(ServerSavePath);
                                //assigning file uploaded status to ViewBag for showing message to user.  
                                ViewBag.UploadStatus = jobDetail.files.Count().ToString() + " files uploaded successfully.";
                            }

                        }

                        jobDetail.CompanyId = CompanyId;
                        jobDetail.Status = true;
                        jobDetail.JobDetailsAttachmentUrl = companyDetail.CompanyName + "_" + CompanyId;



                        db.JobDetails.Add(jobDetail);
                        db.SaveChanges();
                        //Session["CandidateId"] = Convert.ToInt32(candidateDetail.CandidateId);
                        // SendMail(candidateDetail);
                        //
                        return RedirectToAction("ManageJob", "JobLanding");

                    }



                    //   return RedirectToAction("Register", "Account", rModel);
                    //  return RedirectToAction("StartExam", "DrivingTest");
                }
            }

            ViewBag.ErrMessage = "Error: captcha is not valid.";

            return View(jobDetail);
        }

       
        public ActionResult ManageJob()
        {
            int companyId = Convert.ToInt32(Session["CompanyId"]);
            var jobDetails = db.JobDetails.Where(j=>j.CompanyId==companyId).Include(j => j.CompanyDetail);
            return View(jobDetails.ToList());
        }

        public ActionResult Details(int? id)
        {
            ViewBag.Role = Convert.ToString(Session["role"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDetail jobDetail = db.JobDetails.Find(id);
            if (jobDetail == null)
            {
                return HttpNotFound();
            }
            return View(jobDetail);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDetail jobDetail = db.JobDetails.Find(id);
            if (jobDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = jobDetail.CompanyId;
            return View(jobDetail);
        }

        // POST: JobDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,JobTile,Location,Fees,JobDescription,ContactPerson,ContactNumber,Email,NatureOfWork,JobDetailsAttachmentUrl,ExpectedDuration")] JobDetail jobDetail)
        {
            if (ModelState.IsValid)
            {
                jobDetail.CompanyId = Convert.ToInt32(Session["CompanyId"]);
                jobDetail.Status = true;
                db.Entry(jobDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageJob");
            }
            ViewBag.CompanyId = jobDetail.CompanyId;
            return View(jobDetail);
        }

        // GET: JobDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobDetail jobDetail = db.JobDetails.Find(id);
            if (jobDetail == null)
            {
                return HttpNotFound();
            }
            return View(jobDetail);
        }

        // POST: JobDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobDetail jobDetail = db.JobDetails.Find(id);
            db.JobDetails.Remove(jobDetail);
            db.SaveChanges();
            return RedirectToAction("ManageJob");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}