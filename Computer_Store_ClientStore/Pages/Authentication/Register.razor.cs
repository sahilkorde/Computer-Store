using Computer_Store_ClientStore.Service.IService;
using Computer_Store_Models;
using Microsoft.AspNetCore.Components;
using System;

namespace Computer_Store_ClientStore.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDTO SignupRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject] 
        public NavigationManager _navigationManager { get; set; }
        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await _authService.RegisterUser(SignupRequest);
            if (result.IsRegistrationSuccessful)
            {
                //registration is successful
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //failure faild
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            IsProcessing = false;
        }
    }
}
