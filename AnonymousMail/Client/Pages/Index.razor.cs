using AnonymousMail.Client.Services.Authorization;
using AnonymousMail.Client.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Reflection;

namespace AnonymousMail.Client.Pages
{
    public partial class Index
    {
        [Inject] private HostAuthenticationStateProvider HostAuthenticationStateProvider { get; set; } = default!;
        [CascadingParameter] public MainLayout Parent { get; set; } = default!;

        private string _username = default!;
        private async Task LoginAsync()
        {
            await HttpClient.PostAsJsonAsync($"api/Account/Login", _username);
            HostAuthenticationStateProvider.RefreshState();
            await Parent.RefreshStateAsync();

        }
    }
}
