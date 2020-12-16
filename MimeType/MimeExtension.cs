using System.Threading.Tasks;

namespace Cactus.Blade.MimeType
{
    public static class MimeExtension
    {
        public static string Get(this IMime mime, string fileNameOrExtension)
        {
            return mime[fileNameOrExtension];
        }

        public static Task<string> GetAsync(this IMime mime, string fileNameOrExtension)
        {
            return Task.FromResult(mime[fileNameOrExtension]);
        }
    }
}