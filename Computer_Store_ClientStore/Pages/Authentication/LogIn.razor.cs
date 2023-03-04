using Computer_Store_ClientStore.Service.IService;
using Computer_Store_Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Web;

namespace Computer_Store_ClientStore.Pages.Authentication
{
    public partial class LogIn
    {
        private SignInRequestDTO SignInRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowLoginError { get; set; }
        public string Error { get; set; }
        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        private async Task LoginUser()
        {
            ShowLoginError = false;
            IsProcessing = true;
            var result = await _authService.Login(SignInRequest);
            if (result.IsAuthSuccessful)
            {
                //registration is successful
                var absoluteUri = new Uri(_navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _navigationManager.NavigateTo(ReturnUrl);
                }
            }
            else
            {
                //failure faild
                Error = result.ErrorMessage;
                ShowLoginError = true;
            }
            IsProcessing = false;
        }
    }
}
