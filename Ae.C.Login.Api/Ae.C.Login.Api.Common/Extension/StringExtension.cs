using System;
using System.Collections.Generic;
using System.Text;
using Ae.C.Login.Api.Common.Constant;

namespace Ae.C.Login.Api.Common.Extension
{
    public static class StringExtension
    {
        #region Base Type Extension

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
            if (destination == null && value == null) return true;
            return destination != null && destination.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        public static bool NotEqualsIgnoreCase(this string value, string destination)
        {
            if (destination == null && value == null) return false;
            return destination != null && !destination.Equals(value, StringComparison.OrdinalIgnoreCase);
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

        public static string GenExceptionInfo(this string value, string expString = "")
        {
            if (expString.IsNullOrWhiteSpace()) expString = CommonConstant.ExceptionOccured;
            return $"{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }

        public static string GenDBExceptionInfo(this string value, string expString)
        {
            return $"{expString}\n{CommonConstant.ParameterReqDetail}\n{value}";
        }


        #endregion Base Type Extension

    }
}
