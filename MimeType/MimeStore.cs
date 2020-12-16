using System;
using System.Collections.Generic;

namespace MimeType
{
    internal class MimeStore : Dictionary<string, string>
    {
        public MimeStore() : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
