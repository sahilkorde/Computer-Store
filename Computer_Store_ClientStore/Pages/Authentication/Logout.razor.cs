using Computer_Store_ClientStore.Service.IService;
using Microsoft.AspNetCore.Components;

namespace Computer_Store_ClientStore.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _authService.Logout();
            _navigationManager.NavigateTo("/");
        }
    }
}
