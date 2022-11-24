using AnonymousMail.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousMail.Client.Services.Authorization
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        public HostAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpResponseMessage = (await _httpClient.GetAsync("api/Account/GetCurrentUser"))!;
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var user = await httpResponseMessage.Content.ReadFromJsonAsync<User>();
                if (user != null)
                {
                    claimsIdentity = new ClaimsIdentity(
                        new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Username)
                        },
                        "MailUser");
                }
            }

            return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
        }

        public void RefreshState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
