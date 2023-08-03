using System;
using System.Runtime.Serialization;

namespace Ae.OrderComment.Service.Common.Exceptions
{
    /// <summary>
    /// 自定义异常类型
    /// </summary>
    [Serializable]
    public class CustomException : Exception
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CustomException()
        {
        }

        /// <summary>
        /// 错误信息构造函数
        /// </summary>
        public CustomException(string message) : base(message)
        {
        }

        /// <summary>
        /// 异常构造函数
        /// </summary>
        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
