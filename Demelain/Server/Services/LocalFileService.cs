using System.IO;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Demelain.Server.Services
{
    public class LocalFileService : ILocalFileService
    {
        /// <summary>
        /// Attempts to retrieve a local file as a FileStream.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Task<FileStream> RetrieveLocalFileStream(string fileName)
        {
            var currentDir = Directory.GetCurrentDirectory();
            
            currentDir += Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar;
            
            var filePath = Path.Combine(currentDir, fileName);

            FileStream stream = null;

            stream = File.Exists(filePath) ? 
                File.OpenRead(filePath) :
                null;
            
            return Task.FromResult(stream);
        }
    }
}