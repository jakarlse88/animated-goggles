using System.Net.Http;
using System.Threading.Tasks;
using Demelain.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Demelain.Client.Components.Contact
{
    public class ContactBase : ComponentBase
    {
        // ReSharper disable once InconsistentNaming
        protected readonly ContactFormInputModel _contactFormInput = new ContactFormInputModel();

        [Inject] private HttpClient HttpClient { get; set; }

        protected async Task HandleValidSubmit()
        {
            var contactInfo = new ContactFormInputModel()
            {
                Name = _contactFormInput.Name,
                Subject = _contactFormInput.Subject,
                Email = _contactFormInput.Email,
                Message = _contactFormInput.Message
            };

            await HttpClient.PostJsonAsync("https://localhost:5002/api/contact", contactInfo);
        }
    }
}