using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType.VirtualFile
{
    internal class VirtualFileReader
    {
        private readonly Assembly _assembly;

        public VirtualFileReader(Assembly assembly)
        {
            _assembly = assembly;
        }

        public string[] GetFileNames() => _assembly.GetManifestResourceNames();

        public string[] GetFileNames(Func<string, bool> predicate) =>
            _assembly.GetManifestResourceNames()
                .Where(predicate)
                .ToArray();

        public Task<string[]> GetFileNamesAsync() => Task.FromResult(_assembly.GetManifestResourceNames());

        public Task<string[]> GetFileNamesAsync(Func<string, bool> predicate) =>
            Task.FromResult(_assembly.GetManifestResourceNames().Where(predicate).ToArray());

        public Stream Read(string fileName) => _assembly.GetManifestResourceStream(fileName);

        public Task<Stream> ReadAsync(string fileName) =>
            Task.FromResult(_assembly.GetManifestResourceStream(fileName));
    }
}
