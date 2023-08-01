using System;
using System.Collections.Generic;
using System.Text;
using Ae.BaoYang.Service.Common.Exceptions;

namespace Ae.BaoYang.Service.Common.Common
{
    public static class ParameterChecker
    {
        /// <summary>
        /// Checks string whether it's null or empty.
        /// </summary>
        /// <param name="parameter">Parameter to check null or empty.</param>
        /// <param name="parameterName">Parameter name. <B>It should NOT be null.</B></param>
        public static void CheckNullOrEmpty(string parameter, string parameterName)
        {
            CheckParameterName(parameterName);

            if (string.IsNullOrEmpty(parameter))
            {
                throw new CustomException(string.Format("String {0} is null or empty.", parameterName));
            }
        }

        #region Private Methods

        /// <summary>
        /// Checks parameter name whether it's null or empty.
        /// </summary>
        /// <param name="parameterName"></param>
        private static void CheckParameterName(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new CustomException("parameterName is invalid in ParameterChecker.CheckNull");
            }
        }

        #endregion
    }
}
