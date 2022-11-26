using System.Net;
using AnonymousMail.Shared.Command.Response.Mail;
using AnonymousMail.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components.Authorization;

namespace AnonymousMail.Client.Pages
{
    public partial class WriteMessage
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        private MailMessageResponse MailMessage { get; set; } = new();
        private HubConnection _hubConnection = default!;
        private ClaimsPrincipal _user = default!;
        private bool _sendMessage = false;

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
        public void Dispose() => _ = _hubConnection.DisposeAsync();
        protected override async Task OnInitializedAsync()
        {
            _user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            await ConfigureHubConnection();
        }

        private async Task ConfigureHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/mailMessageHub"))
                .Build();

            await _hubConnection.StartAsync();
        }

        private async Task<IEnumerable<User>> SearchUsersAsync(string value)
        {
            var httpMessageResponse = await HttpClient.GetAsync($"api/MailMessage/SearchUsers/{value}");
            if (httpMessageResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return (await httpMessageResponse.Content.ReadFromJsonAsync<IEnumerable<User>>())!;
            }

            return new List<User>();
        }

        private async Task SendMessageAsync()
        {
            _sendMessage = true;

            var httpResponseMessage = await HttpClient.PostAsJsonAsync("api/MailMessage/Save", MailMessage);
            
            if (IsConnected)
            {
                await _hubConnection.SendAsync("SendMailNotification", _user.Identity!.Name, MailMessage.ToUserId);
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var mailMessage = await httpResponseMessage.Content.ReadFromJsonAsync<MailMessageResponse>();
                    await _hubConnection.SendAsync("SendMailMessage", mailMessage, MailMessage.ToUserId);
                }

            }

            MailMessage = null!;
            StateHasChanged();
            MailMessage = new();
            _sendMessage = false;

            Snackbar.Add("Сообщение отправлено", Severity.Success);
        }
    }
}
