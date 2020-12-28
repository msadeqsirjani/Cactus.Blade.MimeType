using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType
{
    public class Mime : IMime
    {
        private const string DefaultExtension = "bin";
        private const string DefaultMimeType = "application/octet-stream";

        private static readonly Lazy<MimeStore> MimeTypeMap = new Lazy<MimeStore>(() => new MimeStore());


        public static string GetExtension(string mime)
        {
            var extension = MimeTypeMap.Value.FirstOrDefault(x => x.Value.Contains(mime)).Key;

            return extension ?? DefaultExtension;
        }

        public static string GetMimeType(string file)
        {
            var extension = file;
            var index = extension.LastIndexOf('.');

            if (index != -1 && extension.Length > index + 1) extension = file[(index + 1)..].ToLower();

            return MimeTypeMap.Value.TryGetValue(extension, out var result) ? result : DefaultMimeType;
        }

        public static Task<string> GetMimeTypeAsync(string file)
        {
            var extension = file;
            var index = extension.LastIndexOf('.');

            if (index != -1 && extension.Length > index + 1) extension = file[(index + 1)..].ToLower();

            var mime = MimeTypeMap.Value.TryGetValue(extension, out var result) ? result : DefaultMimeType;

            return Task.FromResult(mime);
        }

        public static void Upsert(string mime, string extension)
        {
            MimeTypeMap.Value[extension] = mime;
        }

        public static Task UpsertAsync(string mime, string extension)
        {
            MimeTypeMap.Value[extension] = mime;

            return Task.CompletedTask;
        }

        public string this[string key] => MimeTypeMap.Value[key];
    }
}
