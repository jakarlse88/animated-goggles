using System.Threading.Tasks;
using Demelain.Server.Models;

// ReSharper disable once CheckNamespace
namespace Demelain.Server.Services
{
    public interface IMessageService
    {
        Task<bool> SendMessage(ContactFormDto dto);
    }
}