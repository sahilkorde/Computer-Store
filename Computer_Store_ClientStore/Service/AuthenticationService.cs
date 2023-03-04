using Blazored.LocalStorage;
using Computer_Store_ClientStore.Service.IService;
using Computer_Store_Common;
using Computer_Store_Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Computer_Store_ClientStore.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }
        public async Task<SignInresponseDTO> Login(SignInRequestDTO signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInresponseDTO>(contentTemp);
            if(response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInresponseDTO() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogOut();

            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequest)
        {
            var content = JsonConvert.SerializeObject(signUpRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(contentTemp);
            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO() { IsRegistrationSuccessful = true };
            }
            else
            {
                return new SignUpResponseDTO() { IsRegistrationSuccessful = false, Errors = result.Errors };
            }
        }
    }
}
