using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
	{
		public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
		{
		}

		public override void Add(ApplicantSkillPoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}

		public override void Update(ApplicantSkillPoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

		protected override void Verify(ApplicantSkillPoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (ApplicantSkillPoco item in pocos)
			{
				if (item.StartMonth >12)
				{
					exceptions.Add(new ValidationException(101, $"{item.Id}"));
				}
				if (item.EndMonth > 12)
				{
					exceptions.Add(new ValidationException(102, $"{item.Id}"));
				}
				if (item.StartYear < 1900)
				{
					exceptions.Add(new ValidationException(103, $"{item.Id}"));
				}
				if (item.EndYear < item.StartYear)
				{
					exceptions.Add(new ValidationException(104, $"{item.Id}"));
				}
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}
