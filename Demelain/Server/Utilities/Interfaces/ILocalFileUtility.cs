using System.IO;
using System.Threading.Tasks;

namespace Demelain.Server.Utilities
{
    public interface ILocalFileUtility
    {
        Task<FileStream> RetrieveLocalFileStream(string fileName);
    }
}