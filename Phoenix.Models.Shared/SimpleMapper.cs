using Phoenix.Domain;

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
