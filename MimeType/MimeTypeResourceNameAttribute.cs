using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cactus.Blade.MimeType
{
    public class MimeTypeResourceNameAttribute : Attribute
    {
        public string Name { get; }

        public MimeTypeResourceNameAttribute(string name)
        {
            Name = name;
        }

        public static MimeTypeResourceNameAttribute GetOrNull(Type resourceType)
        {
            return resourceType
                .GetCustomAttributes(true)
                .OfType<MimeTypeResourceNameAttribute>()
                .FirstOrDefault();
        }

        public static Task<MimeTypeResourceNameAttribute> GetOrNullAsync(Type resourceType)
        {
            return Task.FromResult(resourceType
                .GetCustomAttributes(true)
                .OfType<MimeTypeResourceNameAttribute>()
                .FirstOrDefault());
        }

        public static string GetName(Type resourceType)
        {
            return GetOrNull(resourceType)?.Name ?? resourceType.FullName;
        }

        public static async Task<string> GetNameAsync(Type resourceType)
        {
            return (await GetOrNullAsync(resourceType))?.Name ?? resourceType.FullName;
        }
    }
}
