using Microsoft.AspNetCore.Components;
using Pheonix.OMS.Domain.Repository;
using Entity = Pheonix.OMS.Domain;

namespace Phoenix.WebApp.Components.Pages
{
	public partial class SalesOrder
	{
		private List<Entity.SalesOrder> _salesOrders = new List<Entity.SalesOrder>();

		[Inject]
		public IRepositoryBase<Entity.SalesOrder> _salesOrderRepo { get; set; }


		public SalesOrder()
		{
		}
		protected override async Task OnInitializedAsync()
		{
			IEnumerable<Entity.SalesOrder> salesOrders = await _salesOrderRepo.GetAll();
			if (salesOrders != null && salesOrders.Any())
			{
				_salesOrders = salesOrders.ToList();
			}
		}
	}
}
