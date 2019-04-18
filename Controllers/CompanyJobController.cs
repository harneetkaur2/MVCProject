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
    public class CompanyJobController : Controller
    {
        private CareerCloudContext db = new CareerCloudContext();

        // GET: CompanyJob
        public ActionResult Index(Guid? Id)
        {
            if (Id == null)
            {
                var companyJobs = db.CompanyJobs.Include(c => c.CompanyProfile);
                return View(companyJobs.ToList());
            }
            else
            {
                var companyJobs = db.CompanyJobs.Where(c=>c.Company==Id).Include(c => c.CompanyProfile);
                return View(companyJobs.ToList());

            }
        }

        // GET: CompanyJob/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobPoco companyJobPoco = db.CompanyJobs.Find(id);
            if (companyJobPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite");
            return View();
        }

        // POST: CompanyJob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobPoco.Id = Guid.NewGuid();
                db.CompanyJobs.Add(companyJobPoco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobPoco companyJobPoco = db.CompanyJobs.Find(id);
            if (companyJobPoco == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // POST: CompanyJob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyJobPoco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.CompanyProfiles, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyJobPoco companyJobPoco = db.CompanyJobs.Find(id);
            if (companyJobPoco == null)
            {
                return HttpNotFound();
            }
            return View(companyJobPoco);
        }

        // POST: CompanyJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyJobPoco companyJobPoco = db.CompanyJobs.Find(id);
            db.CompanyJobs.Remove(companyJobPoco);
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

        public ActionResult PostedJobs(Guid? id)
        {
            var companyjobs = db.CompanyProfiles;
            // ViewBag.CompanyName = db.CompanyProfile.Include(ap => ap.CompanyJob).SingleOrDefault(ap => ap.Id == id).CompanyProfile.Id;
            TempData["CompanyId"] = id;
            List<PostedJobs> PostedJobs = new List<PostedJobs>();
            //  foreach (var companyjob in companyjobs)
            //  {
            //    PostedJobs.Add(
            //      new PostedJobs
            {
                //    CompanyId = companyjob.Id,
                //      JobId = companyjob.Job,
                //      JobTitle = companyjob.CompanyJob.CompanyJobDescriptions.SingleOrDefault(cj => cj.Job == applicantJobApplication.Job).JobName,
                //    JobDescription = companyjob.CompanyJob.CompanyJobDescriptions.SingleOrDefault(cj => cj.Job == applicantJobApplication.Job).JobDescriptions,
                //        PostedDate = companyjob.ApplicationDate
                //    });
                //   }
                return View(PostedJobs);
            }
        }
    }
}
