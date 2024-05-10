using Phoenix.Domain;

namespace Phoenix.Models.Shared
{
	public static class SimpleMapper
	{
		public static PaymentTermModel MapPaymentTermToPaymentTermModel(PaymentTerm inModel) => new PaymentTermModel
		{
			Id = inModel.Id,
			Code = inModel.Code,
			Name = inModel.Name,
			PaymentDayCalculation = inModel.PaymentDayCalculation,
			PaymentDay = inModel.PaymentDay,
			PaymentDate = inModel.PaymentDate,
			NullableDate = inModel.NullableDate,
			NullableDecimal = inModel.NullableDecimal,
			NullableInt=inModel.NullableInt
		};
	}
}
