﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
	{
		public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
		{
		}

		public override void Add(CompanyJobDescriptionPoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}

		public override void Update(CompanyJobDescriptionPoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

		protected override void Verify(CompanyJobDescriptionPoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (CompanyJobDescriptionPoco item in pocos)
			{
				if (item.JobName==null)
				{
					exceptions.Add(new ValidationException(300, $"{item.Id}"));
				}
				if (item.JobDescriptions==null)
				{
					exceptions.Add(new ValidationException(301, $"{item.Id}"));
				}
				
			}
			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}
