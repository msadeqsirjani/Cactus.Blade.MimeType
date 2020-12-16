using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType.VirtualFile
{
    internal class VirtualFileInfo
    {
        private readonly VirtualFileReader _virtualFileReader;
        private readonly Dictionary<string, MimeStore> _mimeStore;

        public VirtualFileInfo(Assembly assembly)
        {
            _mimeStore = new Dictionary<string, MimeStore>(StringComparer.OrdinalIgnoreCase);
            _virtualFileReader = new VirtualFileReader(assembly);
        }

        public VirtualFileInfo Get(params string[] extension)
        {
            var fileNames = extension.Length > 0
                ? _virtualFileReader.GetFileNames(e => extension.Any(e.EndsWith))
                : _virtualFileReader.GetFileNames();

            foreach (var fileName in fileNames)
                _mimeStore[fileName] = null;

            return this;
        }

        public async Task<VirtualFileInfo> GetAsync(params string[] extension)
        {
            var fileNames = extension.Length > 0
                ? await _virtualFileReader.GetFileNamesAsync(e => extension.Any(e.EndsWith))
                : await _virtualFileReader.GetFileNamesAsync();

            foreach (var fileName in fileNames)
                _mimeStore[fileName] = null;

            return this;
        }

        public VirtualFileInfo Read(string path, string extension)
        {
            for (var keyIndex = 0; keyIndex < _mimeStore.Count; keyIndex++)
            {
                var fileName = _mimeStore.ElementAt(keyIndex).Key;
                if (fileName.IsMatch($@"^*.{path}.*-*.({extension})") ||
                    fileName.IsMatch($@"^*.{path}.*.({extension})"))
                {
                    _mimeStore[fileName] = Deserialize<MimeStore>(_virtualFileReader.Read(fileName));
                }
            }

            return this;
        }

        public async Task<VirtualFileInfo> ReadAsync(string path, string extension)
        {
            for (var keyIndex = 0; keyIndex < _mimeStore.Count; keyIndex++)
            {
                var fileName = _mimeStore.ElementAt(keyIndex).Key;
                if (fileName.IsMatch($@"^*.{path}.*-*.({extension})") ||
                    fileName.IsMatch($@"^*.{path}.*.({extension})"))
                {
                    _mimeStore[fileName] =
                        await DeserializeAsync<MimeStore>(await _virtualFileReader.ReadAsync(fileName));
                }
            }

            return this;
        }

        public static T Deserialize<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(reader);
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<T>(jsonReader);
        }

        public static Task<T> DeserializeAsync<T>(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(reader);
            var jsonSerializer = new JsonSerializer();
            return Task.FromResult(jsonSerializer.Deserialize<T>(jsonReader));
        }

        public string GetValue(string name)
        {
            var value = string.Empty;
            foreach (var mimeStore in _mimeStore)
            {
                _mimeStore[mimeStore.Key]?.TryGetValue(name, out value);
                if (value.IsNotNullOrEmpty())
                    break;
            }

            return value;
        }

        public Task<string> GetValueAsync(string name)
        {
            var value = string.Empty;
            foreach (var mimeStore in _mimeStore)
            {
                _mimeStore[mimeStore.Key]?.TryGetValue(name, out value);
                if (value.IsNotNullOrEmpty())
                    break;
            }

            return Task.FromResult(value);
        }
    }
}
