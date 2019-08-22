using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TADE.Models;

namespace TADE.Controllers
{
    public class JobDetailsController : Controller
    {
        private TADEDBEntities db = new TADEDBEntities();

        // GET: JobDetails
        public ActionResult Index()
        {
            var jobDetails = db.JobDetails.Include(j => j.CompanyDetail);
            return View(jobDetails.ToList());
        }

        // GET: JobDetails/Details/5
        public ActionResult Details(int? id)
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

        // GET: JobDetails/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.CompanyDetails, "CompanyId", "UserId");
            return View();
        }

        // POST: JobDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,CompanyId,JobTile,Location,Fees,JobDescription,ContactPerson,ContactNumber,Email,Status,NatureOfWork,JobDetailsAttachmentUrl,ExpectedDuration")] JobDetail jobDetail)
        {
            if (ModelState.IsValid)
            {
                db.JobDetails.Add(jobDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.CompanyDetails, "CompanyId", "UserId", jobDetail.CompanyId);
            return View(jobDetail);
        }

        // GET: JobDetails/Edit/5
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
            ViewBag.CompanyId = new SelectList(db.CompanyDetails, "CompanyId", "UserId", jobDetail.CompanyId);
            return View(jobDetail);
        }

        // POST: JobDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,CompanyId,JobTile,Location,Fees,JobDescription,ContactPerson,ContactNumber,Email,Status,NatureOfWork,JobDetailsAttachmentUrl,ExpectedDuration")] JobDetail jobDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.CompanyDetails, "CompanyId", "UserId", jobDetail.CompanyId);
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
            return RedirectToAction("Index");
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
