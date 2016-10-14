using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoloSignups.Helpers
{
    public static class UrlHelper
    {
        public static string UrlEncodeBase64String(string base64String)
        {
            return base64String
                .Replace('+', '.')
                .Replace('/', '_')
                .Replace('=', '-');
        }

        public static string UrlDecodeBase64String(string urlEncodedString)
        {
            return urlEncodedString
                .Replace('.', '+')
                .Replace('_', '/')
                .Replace('-', '=');
        }
    }
}
