using System.IO;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Demelain.Server.Services
{
    public interface ILocalFileService
    {
        Task<FileStream> RetrieveLocalFileStream(string fileName);
    }
}