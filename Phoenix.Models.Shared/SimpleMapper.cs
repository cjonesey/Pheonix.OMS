using Phoenix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models.Shared
{
	public static class SimpleMapper
	{
		public static PaymentTermModel MapPaymentTermToPaymentTermModel(PaymentTerm inModel) => new PaymentTermModel
		{
			Id = inModel.Id,
			Name = inModel.Name,
			PaymentDays = inModel.PaymentDays
		};
	}
}
