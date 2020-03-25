using System.IO;
using System.Threading.Tasks;

namespace Demelain.Server.Utilities
{
    public class LocalFileUtility : ILocalFileUtility
    {
        /// <summary>
        /// Retrieves a local file as a <see cref="FileStream"/>.
        /// </summary>
        /// <param name="fileName">Name of the file to retrieve.</param>
        /// <returns>The specified file as a <see cref="FileStream"/>.</returns>
        public Task<FileStream> RetrieveLocalFileStream(string fileName)
        {
            var currentDir = Directory.GetCurrentDirectory();
            
            var assetsDir= currentDir + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar;
            
            var filePath = Path.Combine(assetsDir, fileName);

            FileStream stream = null;

            stream = File.Exists(filePath) ? 
                File.OpenRead(filePath) :
                null;
            
            return Task.FromResult(stream);
        }
    }
}