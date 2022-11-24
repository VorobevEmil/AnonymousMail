using AnonymousMail.Shared.Command.Response.Mail;
using AnonymousMail.Shared.Models;
using System.Net.Http.Json;

namespace AnonymousMail.Client.Pages
{
    public partial class InputMessages
    {
        private List<MailMessageResponse> mailMessages = default!;
        protected override async Task OnInitializedAsync()
        {
            mailMessages = (await HttpClient.GetFromJsonAsync<List<MailMessageResponse>>("api/MailMessage/GetAllInputMessages"))!;
        }
    }
}
