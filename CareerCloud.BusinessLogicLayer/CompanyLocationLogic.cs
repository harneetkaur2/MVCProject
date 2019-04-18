using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
	{
		public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
		{
		}

		public override void Add(CompanyLocationPoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}

		public override void Update(CompanyLocationPoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

		protected override void Verify(CompanyLocationPoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (CompanyLocationPoco item in pocos)
			{
				if (item.CountryCode==null)
				{
					exceptions.Add(new ValidationException(500, $"{item.Id}"));
				}
				if (item.Province==null)
				{
					exceptions.Add(new ValidationException(501, $"{item.Id}"));
				}
				if (item.Street==null)
				{
					exceptions.Add(new ValidationException(502, $"{item.Id}"));
				}
				if (item.City ==null)
				{
					exceptions.Add(new ValidationException(503, $"{item.Id}"));
				}
				if (item.PostalCode ==null)
				{
					exceptions.Add(new ValidationException(504, $"{item.Id}"));
				}
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}
