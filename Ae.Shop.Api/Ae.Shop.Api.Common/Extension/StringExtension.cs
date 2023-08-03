using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Api.Common.Constant;

namespace Ae.Shop.Api.Common.Extension
{
    public static class StringExtension
    {
        // ---------------------------------- Base Type Extension --------------------------------------
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool EqualsIgnoreCase(this string value, string destination)
        {
            return value.Equals(destination, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool NotEqualsIgnoreCase(this string value, string destination)
        {
            return !value.Equals(destination, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool CheckIsGuidFormat(this string guidStr)
        {
            return !guidStr.IsNullOrWhiteSpace() && Guid.TryParse(guidStr, out Guid newGuid);
        }

        public static string GenArgumentErrorInfo(this string value, string expString = "")
        {
            if (expString.IsNullOrWhiteSpace()) expString = CommonConstant.ArgumentValidateFailed;
            return $"{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenExceptionInfo(this string value, string expType = "")
        {
            if (expType.IsNullOrWhiteSpace()) expType = CommonConstant.ExceptionOccured;
            return $"{expType}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenDBExceptionInfo(this string value, string expString)
        {
            return $"{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        
    }
}
