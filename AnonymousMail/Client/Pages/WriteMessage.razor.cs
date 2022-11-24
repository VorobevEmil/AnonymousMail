using AnonymousMail.Shared.Command.Response.Mail;
using AnonymousMail.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace AnonymousMail.Client.Pages
{
    public partial class WriteMessage
    {
        private MailMessageResponse MailMessage { get; set; } = new();
        private bool _sendMessage = false;
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

            await HttpClient.PostAsJsonAsync("api/MailMessage/Save", MailMessage);

            MailMessage = null!;
            StateHasChanged();
            MailMessage = new();
            _sendMessage = false;

            Snackbar.Add("Сообщение отправлено", Severity.Success);
        }
    }
}
