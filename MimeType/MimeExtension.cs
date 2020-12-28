using System.Threading.Tasks;

namespace Cactus.Blade.MimeType
{
    public static class MimeExtension
    {
        public static string Get(this IMime mime, string fileOrExtension)
        {
            return mime[fileOrExtension];
        }

        public static Task<string> GetAsync(this IMime mime, string fileOrExtension)
        {
            return Task.FromResult(mime[fileOrExtension]);
        }
    }
}