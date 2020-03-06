using System.Threading.Tasks;
using Demelain.Client.Models;
using Demelain.Client.Models.InputModels;
using Demelain.Client.Models.ResultModels;

namespace Demelain.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResult> RegisterAsync(RegisterInputModel model);
        Task<LoginResult> LoginAsync(LoginInputModel model);
        Task LogoutAsync();
    }
}