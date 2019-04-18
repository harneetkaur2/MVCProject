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
using CareerCloud.DataAccessLayer;

namespace MVCProject.Controllers
{
    public class ApplicantEducationController : Controller
    {
        //private CareerCloudContext db = new CareerCloudContext();
      private  EFGenericRepository<ApplicantEducationPoco> repo =
                new EFGenericRepository<ApplicantEducationPoco>();

        // GET: ApplicantEducation
        public ActionResult Index(Guid Id)
        {
            // var applicantEducations = db.ApplicantEducations.Where(a=>a.Applicant==Id).Include(a => a.ApplicantProfile);
            // return View(applicantEducations.ToList());
            IList<ApplicantEducationPoco> pocos = repo.GetList(a => a.Applicant == Id);
            return View(repo.All());
        }

        // GET: ApplicantEducation/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            ApplicantEducationPoco applicantEducationPoco = repo.GetSingle(a=>a.Id==id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Create
        public ActionResult Create()
        {
           // ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency");
            return View();
        }

        // POST: ApplicantEducation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                // applicantEducationPoco.Id = Guid.NewGuid();
                // db.ApplicantEducations.Add(applicantEducationPoco);
                // db.SaveChanges();
                repo.Add(applicantEducationPoco);
                return RedirectToAction("Index");
            }
            return View();
            // ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency", applicantEducationPoco.Applicant);
            //  return View(applicantEducationPoco);
        }
     

        // GET: ApplicantEducation/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            ApplicantEducationPoco applicantEducationPoco = repo.GetSingle(a=>a.Id==id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
          
            // ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency", applicantEducationPoco.Applicant);
             return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Applicant,Major,CertificateDiploma,StartDate,CompletionDate,CompletionPercent")] ApplicantEducationPoco applicantEducationPoco)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(applicantEducationPoco).State = EntityState.Modified;
                // db.SaveChanges();
                repo.Update(applicantEducationPoco);
                return RedirectToAction("Index");
            }
           // ViewBag.Applicant = new SelectList(db.ApplicantProfiles, "Id", "Currency", applicantEducationPoco.Applicant);
            return View(applicantEducationPoco);
        }

        // GET: ApplicantEducation/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            ApplicantEducationPoco applicantEducationPoco = repo.GetSingle(a=>a.Id==id);
            if (applicantEducationPoco == null)
            {
                return HttpNotFound();
            }
            return View(applicantEducationPoco);
        }

        // POST: ApplicantEducation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            // ApplicantEducationPoco applicantEducationPoco = db.ApplicantEducations.Find(id);
            //db.ApplicantEducations.Remove(applicantEducationPoco);
            //db.SaveChanges();
            ApplicantEducationPoco applicantEducationPoco = repo.GetSingle(a => a.Id == id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
