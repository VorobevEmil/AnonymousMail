using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonymousMail.Client.Shared
{
    public partial class MainLayout
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        private bool _userIsAuthenticated;
        private bool _drawerOpen;

        protected override async Task OnInitializedAsync()
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            _userIsAuthenticated = user.Identity!.IsAuthenticated;
            _drawerOpen = _userIsAuthenticated;
            StateHasChanged();
        }

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;
        public async Task RefreshStateAsync() => await OnInitializedAsync();
    }
}
