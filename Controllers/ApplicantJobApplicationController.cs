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
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class ApplicantJobApplicationController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: ApplicantJobApplication
        public ActionResult Index(Guid? Id)
        {
            if (Id == null)
            {
                var applicantJobApplications = db.ApplicantJobApplications.Include(a => a.ApplicantProfile);
                return View(applicantJobApplications.ToList());
            }
            else
            {
                var applicantJobApplications = db.ApplicantJobApplications.Where(a => a.Applicant == Id).Include(a => a.ApplicantProfile);
                return View(applicantJobApplications.ToList());

            }

        }

        // GET: ApplicantJobApplication/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            if (applicantJobApplicationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplication/Create
        public ActionResult Create()
        {
            ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency");
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: ApplicantJobApplication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Job,ApplicationDate")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (ModelState.IsValid)
            {   
                applicantJobApplicationPoco.Id = Guid.NewGuid();
                db.ApplicantJobApplications.Add(applicantJobApplicationPoco);
                db.SaveChanges();
                return RedirectToAction("Index", new { id= applicantJobApplicationPoco.Applicant});
            }

            ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency", applicantJobApplicationPoco.Applicant);
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplication/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            if (applicantJobApplicationPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency", applicantJobApplicationPoco.Applicant);
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Job,ApplicationDate")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantJobApplicationPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency", applicantJobApplicationPoco.Applicant);
            ViewBag.Job = new SelectList(db.CompanyJobs, "Id", "Id", applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplication/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            if (applicantJobApplicationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicantJobApplicationPoco applicantJobApplicationPoco = db.ApplicantJobApplications.Find(id);
            db.ApplicantJobApplications.Remove(applicantJobApplicationPoco);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = applicantJobApplicationPoco.Applicant });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AppliedJobs(Guid? id)
        {
            var applicantJobApplications = db.ApplicantJobApplications.Include(a => a.ApplicantProfile).Include(a=>a.CompanyJob).Where(ap=>ap.Applicant==id).ToList();
            ViewBag.ApplicantName = db.ApplicantProfiles.Include(ap => ap.SecurityLogin).SingleOrDefault(ap => ap.Id == id).SecurityLogin.FullName;
            TempData["ApplicantId"] = id;
            List<AppliedJobs> AppliedJobs = new List<AppliedJobs>();
            foreach (var applicantJobApplication in applicantJobApplications)
            {
                AppliedJobs.Add(
                    new AppliedJobs
                    {
                        ApplicantId = applicantJobApplication.Applicant,
                        AppliedId = applicantJobApplication.Job,
                        JobTitle = applicantJobApplication.CompanyJob.CompanyJobDescriptions.SingleOrDefault(cj=>cj.Job==applicantJobApplication.Job).JobName,
                        JobDescription = applicantJobApplication.CompanyJob.CompanyJobDescriptions.SingleOrDefault(cj=>cj.Job==applicantJobApplication.Job).JobDescriptions,
                        ApplicationDate = applicantJobApplication.ApplicationDate
                    });
            }
            return View(AppliedJobs);
        }
    }
}
