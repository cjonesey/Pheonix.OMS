using Microsoft.AspNetCore.Components;
using Phoenix.Infrastructure;
using Entity = Phoenix.Domain;

namespace Phoenix.WebApp.Components.Pages
{
    public partial class Warehouse
    {
        private List<Entity.Warehouse> _warehouses = new List<Entity.Warehouse>();

        [Inject]
        public IRepositoryBase<Entity.Warehouse> _warehouseRepo { get; set; }


        public Warehouse()
        {
        }
        protected override async Task OnInitializedAsync()
        {
            IEnumerable<Entity.Warehouse> warehouses = await _warehouseRepo.GetAll();
            if (warehouses != null && warehouses.Any())
            {
                _warehouses = warehouses.ToList();
            }
        }
    }
}
