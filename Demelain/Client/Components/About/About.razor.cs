using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Demelain.Client.Components.About
{
    public class AboutBase : ComponentBase
    {
        [Inject] private HttpClient HttpClient { get; set; }
        protected AboutSectionData AboutSectionData { get; set; }

        // protected override async Task OnInitializedAsync()
        // {
        //     AboutSectionData = 
        //         await HttpClient.GetJsonAsync<AboutSectionData>("https://localhost:5003/api/personaldetails/1");
        // }

        protected async Task GetResumeFileFromServerAsync()
        {
            await HttpClient.GetAsync("https://localhost:5003/api/file/resume");
        }
    }

    public class AboutSectionData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Biography { get; set; }
    }
}