using System;
using System.Collections.Generic;

namespace Cactus.Blade.MimeType
{
    internal class MimeStore : Dictionary<string, string>
    {
        public MimeStore() : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
