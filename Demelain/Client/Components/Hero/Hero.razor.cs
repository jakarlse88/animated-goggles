using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Demelain.Client.Components.Hero
{
    public class HeroBase : ComponentBase
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }

        protected const string StaticText = "I design and develop fantastic web applications using ";

        public async Task ScrollToSection(string sectionId)
        {
            await JsRuntime
                .InvokeVoidAsync(
                    "linkToPageSection",
                    sectionId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("initTyped");
            }
        }
    }
}