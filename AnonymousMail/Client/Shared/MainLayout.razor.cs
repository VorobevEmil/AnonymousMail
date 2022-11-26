using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace AnonymousMail.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        private HubConnection _hubConnection = default!;
        private ClaimsPrincipal _user = default!;
        private bool _userIsAuthenticated;
        private bool _drawerOpen;

        protected override async Task OnInitializedAsync()
        {
            _user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            _userIsAuthenticated = _user.Identity!.IsAuthenticated;
            _drawerOpen = _userIsAuthenticated;
            StateHasChanged();
            await ConfigureHubConnection();
        }

        private async Task ConfigureHubConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/mailMessageHub"))
                .Build();

            _hubConnection.On<string>("ReceiveMailNotification", (fromUser) =>
            {
                Snackbar.Add($"Новое сообщение от {fromUser}", Severity.Info);
            });

            await _hubConnection.StartAsync();
        }


        private void DrawerToggle() => _drawerOpen = !_drawerOpen;
        public async Task RefreshStateAsync() => await OnInitializedAsync();
        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
        public void Dispose() => _ = _hubConnection.DisposeAsync();
    }
}
