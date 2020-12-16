using System;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType.VirtualFile
{
    internal class VirtualFileFinder
    {
        public static VirtualFileInfo Find(Type resourceType, string filePath)
        {
            var virtualFileInfo = new VirtualFileInfo(resourceType.Assembly);
            virtualFileInfo.Get(".json").Read(filePath, "json");
            return virtualFileInfo;
        }

        public static async Task<VirtualFileInfo> FindAsync(Type resourceType, string filePath)
        {
            var virtualFileInfo = new VirtualFileInfo(resourceType.Assembly);
            await (await virtualFileInfo.GetAsync(".json")).ReadAsync(filePath, "json");
            return virtualFileInfo;
        }
    }
}
