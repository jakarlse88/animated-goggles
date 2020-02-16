using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Demelain.Client.Models;
using Microsoft.AspNetCore.Components;

namespace Demelain.Client.Components.Contact
{
    
    public class ContactBase : ComponentBase
    {
        protected enum SubmitStateEnum
        {
            Initial = 0,
            Sending = 1,
            Success = 2,
            Failed = 3
        }
        
        // ReSharper disable once InconsistentNaming
        protected readonly ContactFormInputModel _contactFormInput = new ContactFormInputModel();

        protected SubmitStateEnum SubmitState { get; set; }
        
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

            SubmitState = SubmitStateEnum.Sending;
            
            StateHasChanged();

            try
            {
                await HttpClient.PostJsonAsync("api/message", contactInfo);

                SubmitState = SubmitStateEnum.Success;
                
                StateHasChanged();
            }
            catch (Exception e)
            {
                SubmitState = SubmitStateEnum.Failed;
                
                StateHasChanged();
                throw;  
            }
        }
    }
}