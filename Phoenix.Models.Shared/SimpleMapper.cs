using Phoenix.Domain;
using System.Collections.Generic;

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

		public static SampleHeaderModel MapSampleHeaderToSampleHeaderModel(SampleDetail inModel) =>
			new SampleHeaderModel
			{
				Id = inModel.SampleHeader.Id,
				Code = inModel.SampleHeader.Code,
				Name = inModel.Name,
				DetailId = inModel.Id
			};
	}
}
