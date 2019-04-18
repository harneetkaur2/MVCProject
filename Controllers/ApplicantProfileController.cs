using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace MVCProject.Controllers
{
    public class ApplicantProfileController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantProfile
        public ActionResult Index(Guid? Id)
        {
            if (Id != null)
            {
                var applicantProfiles = db.ApplicantProfiles.Where(a => a.Login == Id).Include(a => a.SecurityLogin).Include(a => a.SystemCountryCode);
                return View(applicantProfiles.ToList());
            }
            else
            {
                var applicantProfiles = db.ApplicantProfiles.Include(a => a.SecurityLogin).Include(a => a.SystemCountryCode);
                return View(applicantProfiles.ToList());
            }
        }

       // public ActionResult Index(String Id)
       //{
        //    var applicantProfiles = db.ApplicantProfiles.Where(a => a.Country == Id).Include(a => a.SecurityLogin).Include(a => a.SystemCountryCode);
         //   return View(applicantProfiles.ToList());
        //}

        // GET: ApplicantProfile/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfile/Create
        public ActionResult Create()
        {
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login");
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name");
            return View();
        }

        // POST: ApplicantProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (ModelState.IsValid)
            {
                applicantProfilePoco.Id = Guid.NewGuid();
                db.ApplicantProfiles.Add(applicantProfilePoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", applicantProfilePoco.Login);
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfile/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", applicantProfilePoco.Login);
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantProfilePoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Login = new SelectList(db.SecurityLogins, "Id", "Login", applicantProfilePoco.Login);
            ViewBag.Country = new SelectList(db.SystemCountryCodes, "Code", "Name", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfile/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            if (applicantProfilePoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantProfilePoco applicantProfilePoco = db.ApplicantProfiles.Find(id);
            db.ApplicantProfiles.Remove(applicantProfilePoco);
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
