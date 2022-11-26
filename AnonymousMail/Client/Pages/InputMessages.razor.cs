using AnonymousMail.Shared.Command.Response.Mail;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace AnonymousMail.Client.Pages
{
    public partial class InputMessages
    {
        private List<MailMessageResponse> mailMessages = default!;
        private HubConnection _hubConnection = default!;
        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
        public void Dispose() => _ = _hubConnection.DisposeAsync();
        protected override async Task OnInitializedAsync()
        {
            mailMessages = (await HttpClient.GetFromJsonAsync<List<MailMessageResponse>>("api/MailMessage/GetAllInputMessages"))!;
            await ConfigureHubConnection();
        }

        private async Task ConfigureHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/mailMessageHub"))
                .Build();

            _hubConnection.On<MailMessageResponse>("ReceiveMailMessage", (message) =>
            {
                mailMessages.Add(message);
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }
    }
}
