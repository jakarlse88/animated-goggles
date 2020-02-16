using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Demelain.Client.Components.Navbar
{
    public class NavbarBase : ComponentBase
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }
        protected bool ShowSidebar { get; set; } = false;
        protected string NavbarHeading { get; }

        public NavbarBase()
        {
            NavbarHeading = "Jon Karlsen";
        }

        protected void ToggleSidebar() => ShowSidebar = !ShowSidebar;

        protected async Task ScrollToSection(string sectionId)
        {
            await JsRuntime
                .InvokeVoidAsync(
                    "linkToPageSection",
                    sectionId);
            
            if (ShowSidebar) 
                ToggleSidebar();
        }
    }
}