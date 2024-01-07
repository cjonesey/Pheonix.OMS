using Microsoft.AspNetCore.Components;
using Pheonix.OMS.Domain.Repository;
using Phoenix.WebApp.Components.Parts;
using Entity = Pheonix.OMS.Domain;

namespace Phoenix.WebApp.Components.Pages
{
    public partial class CountryEdit
    {
        private Modal modal { get; set; }
        [Parameter]
        public string? Id { get; set; }
        [Parameter]
        public Entity.Country Country { get; set; } = new();

        [Inject]
        public IRepositoryBase<Entity.Country> _countryRepo { get; set; }


        protected override async Task OnInitializedAsync()
        {
            int idSearch = 0;
            if (!string.IsNullOrEmpty(Id) && int.TryParse(Id, out idSearch))
            {
                var country = await _countryRepo.Get(idSearch);
                if (country != null)
                {
                    this.Country = country;
                }
            }
        }

        bool countryLookup = false;

        private void OnValidSubmit()
        {
            // Perform the desired action on form submission
        }
    }
}
