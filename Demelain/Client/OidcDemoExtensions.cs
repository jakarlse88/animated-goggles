using Sotsera.Blazor.Oidc;

namespace Demelain.Client
{
    public static class OidcDemoExtensions
    {
        public static FlowExtensions UseDemoFlow(this OidcSettings settings)
        {
            return new FlowExtensions(settings);
        }

        public class FlowExtensions
        {
            private OidcSettings Settings { get; }

            public FlowExtensions(OidcSettings settings)
            {
                Settings = settings;
            }

            public void Code()
            {
                Settings.ClientId = "spa";
                Settings.ResponseType = "code";
            }

            public void CodeWithShortLivedToken()
            {
                Settings.ClientId = "spa.short";
                Settings.ResponseType = "code";
            }

            public void Implicit()
            {
                Settings.ClientId = "implicit";
                Settings.ResponseType = "id_token token";
            }

            public void ImplicitReference()
            {
                Settings.ClientId = "implicit.reference";
                Settings.ResponseType = "id_token token";
            }

            public void ImplicitWithShortLivedToken()
            {
                Settings.ClientId = "implicit.shortlived";
                Settings.ResponseType = "id_token token";
            }
        }
    }
}