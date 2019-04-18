using System;
using System.ComponentModel.DataAnnotations;
namespace MVCProject.Models
{
    public class AppliedJobs
    {
        public Guid ApplicantId { get; set; }
        public Guid AppliedId { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "Application Date")]
        public DateTime? ApplicationDate { get; set; }
    }
}