using System.Threading.Tasks;
using Demelain.Server.Models.InputModels;

namespace Demelain.Server.Services
{
    public interface IMessageService
    {
        void SendMessage(ContactFormInputModel model);
    }
}