using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Common.Extension
{
    public static class StringExtension
    {
        public static bool EqualsIgnoreCase(this string value, string destination)
        {
            return destination.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
