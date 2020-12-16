using Cactus.Blade.MimeType.Resources;
using Cactus.Blade.MimeType.VirtualFile;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType
{
    public class Mime : IMime
    {
        private static readonly MimeTypeStore StoreMimeType = new MimeTypeStore();

        public string this[string fileNameOrExtension] => Get(fileNameOrExtension);

        public static void AddMimeResources(params Type[] resourceNames)
        {
            var newResourceNames = resourceNames.ToList();
            newResourceNames.Add(typeof(AllMimeType));

            foreach (var mimeType in newResourceNames)
            {
                var folderOrFileName = MimeTypeResourceNameAttribute.GetName(mimeType);
                StoreMimeType.Add(VirtualFileFinder.Find(mimeType, folderOrFileName));
            }
        }

        public static async Task AddMimeResourcesAsync(params Type[] resourceNames)
        {
            var newResourceNames = resourceNames.ToList();
            newResourceNames.Add(typeof(AllMimeType));

            foreach (var mimeType in newResourceNames)
            {
                var folderOrFileName = await MimeTypeResourceNameAttribute.GetNameAsync(mimeType);
                StoreMimeType.Add(await VirtualFileFinder.FindAsync(mimeType, folderOrFileName));
            }
        }

        public static string Get(string fileNameOrExtension)
        {
            if (!StoreMimeType.Any())
                AddMimeResources();

            return StoreMimeType.GetValue(GetExtension(fileNameOrExtension));
        }

        public static async Task<string> GetAsync(string fileNameOrExtension)
        {
            if (!StoreMimeType.Any())
                await AddMimeResourcesAsync();

            return await StoreMimeType.GetValueAsync(await GetExtensionAsync(fileNameOrExtension));
        }

        public static string GetExtension(string fileNameOrExtension)
        {
            var dotIndex = fileNameOrExtension.LastIndexOf(".", StringComparison.Ordinal);

            if (dotIndex < 0)
                return fileNameOrExtension;

            fileNameOrExtension = fileNameOrExtension.Substring(dotIndex);
            fileNameOrExtension = fileNameOrExtension.StartsWith(".")
                ? fileNameOrExtension.Replace(".", "")
                : fileNameOrExtension;

            return fileNameOrExtension;
        }

        public static Task<string> GetExtensionAsync(string fileNameOrExtension)
        {
            var dotIndex = fileNameOrExtension.LastIndexOf(".", StringComparison.Ordinal);

            if (dotIndex < 0)
                return Task.FromResult(fileNameOrExtension);

            fileNameOrExtension = fileNameOrExtension.Substring(dotIndex);
            fileNameOrExtension = fileNameOrExtension.StartsWith(".")
                ? fileNameOrExtension.Replace(".", "")
                : fileNameOrExtension;

            return Task.FromResult(fileNameOrExtension);
        }
    }
}
