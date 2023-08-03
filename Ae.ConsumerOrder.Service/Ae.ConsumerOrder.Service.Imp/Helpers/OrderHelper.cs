using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Common.ValidateCode;
using ApolloErp.Message.Sms;

namespace Ae.ConsumerOrder.Service.Imp.Helpers
{
    /// <summary>
    /// 订单帮助类
    /// </summary>
    public static class OrderHelper
    {
        private static string GenCode(long id, string prefix, string subPrefix)
        {
            //var hashCode = Guid.NewGuid().GetHashCode();
            var validateCode = ValidateCodeHelper.CreateValidateCode(4);
            var code = $"{prefix}{subPrefix}{validateCode}{id}";
            return code;
        }

        /// <summary>
        /// 生成安装码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GenInstallCode(long id, string prefix = "RG")
        {
            var subPrefix = "AZ";
            var code = GenCode(id, prefix, subPrefix);
            return code;
        }

        /// <summary>
        /// 生成核销码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string GenVerificationCode(long id, string prefix = "RG")
        {
            var subPrefix = "HX";
            var code = GenCode(id, prefix, subPrefix);
            return code;
        }

        public static string GenPackageCode(long id,string prefix = "RG")
        {
            var subPrefix = "TC";
            var code = GenCode(id, prefix, subPrefix);
            return code;
        }
    }
}
