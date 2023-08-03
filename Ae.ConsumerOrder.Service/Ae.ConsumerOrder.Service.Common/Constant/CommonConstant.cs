using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Common.Constant
{
    public class CommonConstant
    {
        #region Common

        public static readonly string NotCatchException = "ConsumerOrder:系统异常";

        /// <summary>
        /// 发生错误
        /// </summary>
        public static readonly string ErrorOccured = "发生错误";

        /// <summary>
        /// 详细信息：
        /// </summary>
        public static readonly string DetailedInformation = "详细信息：";

        /// <summary>
        /// 所有参数详情：
        /// </summary>
        public const string ParameterModelDetail = "所有参数详情：";

        /// <summary>
        /// 请求参数详情：
        /// </summary>
        public const string ParameterReqDetail = "请求参数详情：";

        /// <summary>
        /// 无参数
        /// </summary>
        public const string ParameterNone = "无参数";

        /// <summary>
        /// 无数据
        /// </summary>
        public const string NullData = "无数据";

        #endregion Common

        #region Product

        /// <summary>
        /// 产品性质-实物产品
        /// </summary>
        public const sbyte PhysicalProduct = 0;
        /// <summary>
        /// 产品性质-套餐产品
        /// </summary>
        public const sbyte PackageProduct = 1;
        /// <summary>
        /// 产品性质-服务产品
        /// </summary>
        public const sbyte ServiceProduct = 2;
        /// <summary>
        /// 产品性质-数字产品
        /// </summary>
        public const sbyte DigitalProduct = 3;

        #endregion

        #region 更新成本

        /// <summary>
        /// 更新成本成功
        /// </summary>
        public const string UpdateProductCostSuccess = "更新成本成功!";

        /// <summary>
        /// 更新成本成功
        /// </summary>
        public const string UpdateProductCostFailure = "更新成本失败!";

        #endregion
    }
}
