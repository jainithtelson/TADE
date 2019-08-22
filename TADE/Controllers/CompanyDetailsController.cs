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
    public class CompanyDetailsController : Controller
    {
        private TADEDBEntities db = new TADEDBEntities();

        // GET: CompanyDetails
        public ActionResult Index()
        {
            var companyDetails = db.CompanyDetails.Include(c => c.AspNetUser);
            return View(companyDetails.ToList());
        }

        // GET: CompanyDetails/Details/5
        public ActionResult Details()
        {
            int? id = Convert.ToInt32(Session["CompanyId"]);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = db.CompanyDetails.Find(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }

        // GET: CompanyDetails/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: CompanyDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,UserId,CompanyName,AddressLIne1,AddressLIne2,AddressLIne3,PostCode,Email,Password,PhoneNumber,ContactPerson,Position,CompanyLogo,NatureofBusiness,CompanySize,Status,RoleType,AboutTheCompany,DetailedDescriptionAboutCompany")] CompanyDetail companyDetail)
        {
            if (ModelState.IsValid)
            {
                db.CompanyDetails.Add(companyDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", companyDetail.UserId);
            return View(companyDetail);
        }

        // GET: CompanyDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = db.CompanyDetails.Find(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", companyDetail.UserId);
            return View(companyDetail);
        }

        // POST: CompanyDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,CompanyName,AddressLIne1,AddressLIne2,AddressLIne3,PostCode,Email,PhoneNumber,ContactPerson,Position,CompanyLogo,NatureofBusiness,CompanySize,AboutTheCompany,DetailedDescriptionAboutCompany")] CompanyDetail companyDetail)
        {
            if (ModelState.IsValid)
            {
                CompanyDetail companyDetailUp = db.CompanyDetails.Find(companyDetail.CompanyId);
                companyDetailUp.CompanyName = companyDetail.CompanyName;
                
                companyDetailUp.AddressLIne1 = companyDetail.AddressLIne1;
                companyDetailUp.AddressLIne2 = companyDetail.AddressLIne2;
                companyDetailUp.AddressLIne3 = companyDetail.AddressLIne3;
                companyDetailUp.PostCode = companyDetail.PostCode;
                companyDetailUp.Email = companyDetail.Email;
                companyDetailUp.PhoneNumber = companyDetail.PhoneNumber;
                companyDetailUp.ContactPerson = companyDetail.ContactPerson;
                companyDetailUp.Position = companyDetail.Position;
                companyDetailUp.CompanyLogo = companyDetail.CompanyLogo;
                companyDetailUp.NatureofBusiness = companyDetail.NatureofBusiness;
                companyDetailUp.CompanySize = companyDetail.CompanySize;
                companyDetailUp.AboutTheCompany = companyDetail.AboutTheCompany;
                companyDetailUp.DetailedDescriptionAboutCompany = companyDetail.DetailedDescriptionAboutCompany;
                db.Entry(companyDetailUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
           // ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", companyDetail.UserId);
            return View(companyDetail);
        }

        // GET: CompanyDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetail companyDetail = db.CompanyDetails.Find(id);
            if (companyDetail == null)
            {
                return HttpNotFound();
            }
            return View(companyDetail);
        }

        // POST: CompanyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyDetail companyDetail = db.CompanyDetails.Find(id);
            db.CompanyDetails.Remove(companyDetail);
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
