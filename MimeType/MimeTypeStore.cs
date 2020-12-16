using Cactus.Blade.MimeType.VirtualFile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType
{
    internal class MimeTypeStore : List<VirtualFileInfo>
    {
        public string GetValue(string name)
        {
            var value = string.Empty;
            foreach (var virtualFileInfo in this)
            {
                value = virtualFileInfo.GetValue(name);
                if (!value.IsNotNullOrEmpty())
                    break;
            }

            return value;
        }

        public async Task<string> GetValueAsync(string name)
        {
            var value = string.Empty;
            foreach (var virtualFileInfo in this)
            {
                value = await virtualFileInfo.GetValueAsync(name);
                if (!value.IsNotNullOrEmpty())
                    break;
            }

            return value;
        }
    }
}
