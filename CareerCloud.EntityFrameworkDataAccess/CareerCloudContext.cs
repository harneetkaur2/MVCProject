using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CareerCloud.Pocos;
namespace CareerCloud.EntityFrameworkDataAccess
{
	public class CareerCloudContext : DbContext
	{
		public CareerCloudContext(bool createProxy = true) :  
                base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
		{
			//Database.Log = l => System.Diagnostics.Debug.WriteLine(l);
			//Configuration.ProxyCreationEnabled = false;
			Configuration.ProxyCreationEnabled = createProxy;
		}
        public CareerCloudContext():
            base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {

        }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			
		modelBuilder.Entity<ApplicantProfilePoco>().HasMany(a => a.ApplicantEducations).WithRequired(e => e.ApplicantProfile).HasForeignKey(e => e.Applicant);
		modelBuilder.Entity<ApplicantProfilePoco>().HasMany(a => a.ApplicantJobApplications).WithRequired(e => e.ApplicantProfile).HasForeignKey(e => e.Applicant);
		modelBuilder.Entity<ApplicantProfilePoco>().HasMany(a => a.ApplicantResumes).WithRequired(e => e.ApplicantProfile).HasForeignKey(e => e.Applicant);
		modelBuilder.Entity<ApplicantProfilePoco>().HasMany(a => a.ApplicantSkills).WithRequired(e => e.ApplicantProfile).HasForeignKey(e => e.Applicant);
		modelBuilder.Entity<ApplicantProfilePoco>().HasMany(a => a.ApplicantWorkHistory).WithRequired(e => e.ApplicantProfile).HasForeignKey(e => e.Applicant);

			modelBuilder.Entity<CompanyJobPoco>().HasMany(a => a.ApplicantJobApplications).WithRequired(e => e.CompanyJob).HasForeignKey(e => e.Job);
			modelBuilder.Entity<CompanyJobPoco>().HasMany(a => a.CompanyJobDescriptions).WithRequired(e => e.CompanyJob).HasForeignKey(e => e.Job);
			modelBuilder.Entity<CompanyJobPoco>().HasMany(a => a.CompanyJobSkills).WithRequired(e => e.CompanyJob).HasForeignKey(e => e.Job);
			modelBuilder.Entity<CompanyJobPoco>().HasMany(a => a.CompanyJobEducations).WithRequired(e => e.CompanyJob).HasForeignKey(e => e.Job);

			modelBuilder.Entity<CompanyProfilePoco>().HasMany(a => a.CompanyDescriptions).WithRequired(e => e.CompanyProfile).HasForeignKey(e => e.Company);
			modelBuilder.Entity<CompanyProfilePoco>().HasMany(a => a.CompanyLocations).WithRequired(e => e.CompanyProfile).HasForeignKey(e => e.Company);
			modelBuilder.Entity<CompanyProfilePoco>().HasMany(a => a.CompanyJobs).WithRequired(e => e.CompanyProfile).HasForeignKey(e => e.Company);

			modelBuilder.Entity<SecurityLoginPoco>().HasMany(a => a.ApplicantProfile).WithRequired(e => e.SecurityLogin).HasForeignKey(e => e.Login);
			modelBuilder.Entity<SecurityLoginPoco>().HasMany(a => a.SecurityLoginsLogs).WithRequired(e => e.SecurityLogin).HasForeignKey(e => e.Login);
			modelBuilder.Entity<SecurityLoginPoco>().HasMany(a => a.SecurityLoginsRoles).WithRequired(e => e.SecurityLogin).HasForeignKey(e => e.Login);

			modelBuilder.Entity<SecurityRolePoco>().HasMany(a => a.SecurityLoginsRoles).WithRequired(e => e.SecurityRole).HasForeignKey(e => e.Role);

			modelBuilder.Entity<SystemCountryCodePoco>().HasMany(a => a.ApplicantProfile).WithRequired(e => e.SystemCountryCode).HasForeignKey(e => e.Country);
			modelBuilder.Entity<SystemCountryCodePoco>().HasMany(a => a.ApplicantWorkHistory).WithRequired(e => e.SystemCountryCode).HasForeignKey(e => e.CountryCode);
			modelBuilder.Entity<SystemLanguageCodePoco>().HasMany(a => a.CompanyDescriptions).WithRequired(e => e.SystemLanguageCode).HasForeignKey(e => e.LanguageId);


			modelBuilder.Entity<ApplicantEducationPoco>()
				.Ignore(o => o.TimeStamp);

			modelBuilder.Entity<ApplicantJobApplicationPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<ApplicantProfilePoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<ApplicantSkillPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<ApplicantWorkHistoryPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyDescriptionPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyJobSkillPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyJobPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyJobEducationPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyJobDescriptionPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyLocationPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<CompanyProfilePoco>()
			 .Ignore(o => o.TimeStamp);
			modelBuilder.Entity<SecurityLoginPoco>()
				.Ignore(o => o.TimeStamp);
			modelBuilder.Entity<SecurityLoginsRolePoco>()
				.Ignore(o => o.TimeStamp);


			base.OnModelCreating(modelBuilder);
		}
		public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
	    public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
	    public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
	    public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
		public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
		public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
		public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
		public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
		public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
		public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
		public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
		public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
		public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
		public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
		public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
		public DbSet<SecurityLoginsRolePoco> SecurityLoginRoles { get; set; }
		public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
		public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
		public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }
		
	}
}
