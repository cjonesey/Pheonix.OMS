using Microsoft.AspNetCore.Components;
using Pheonix.OMS.Domain.Repository;
using Entity = Pheonix.OMS.Domain;

namespace Phoenix.WebApp.Components.Pages
{
    public partial class Country
    {
        private List<Entity.Country> _countries = new List<Entity.Country>();

        [Inject]
        public IRepositoryBase<Entity.Country> _countryRepo { get; set; }


        public Country()
        {
        }
        protected override async Task OnInitializedAsync()
        {
            IEnumerable<Entity.Country> coutries = await _countryRepo.GetAll();
            if (coutries != null && coutries.Any())
            {
                _countries = coutries.ToList();
            }
        }
    }
}
