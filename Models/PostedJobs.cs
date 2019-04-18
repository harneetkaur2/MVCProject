using System;
using System.ComponentModel.DataAnnotations;
namespace MVCProject.Models
{
    public class PostedJobs
    {
        public Guid CompanyId { get; set; }
        public Guid JobId { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "Posted Date")]
        public DateTime? PostedDate { get; set; }
    }
}