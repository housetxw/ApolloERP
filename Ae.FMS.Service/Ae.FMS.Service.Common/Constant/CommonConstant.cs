using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Common.Constant
{

    public class CommonConstant
    {
        #region Common

        /// <summary>
        /// 查询成功
        /// </summary>
        public static readonly string QuerySuccess = "查询成功";

        /// <summary>
        /// 未捕获异常
        /// </summary>
        public static readonly string NotCatchException = "FMS:系统异常";

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
        /// 【参数安全性校验失败】
        /// </summary>
        public const string ArgumentValidateFailed = "【参数安全性校验失败】";

        /// <summary>
        /// 【ResultCode不是ResultCode.Success:10000】
        /// </summary>
        public const string ResultCodeFailed = "【ResultCode不是ResultCode.Success:10000】";

        /// <summary>
        /// 【API调用发生异常】
        /// </summary>
        public const string ResultCodeException = "【API调用发生异常】";

        /// <summary>
        /// 无参数
        /// </summary>
        public const string ParameterNone = "无参数";

        public static readonly string ParameterMistake = "请求参数出错";

        public static readonly string FriendlyMistake = "系统异常，请联系总部";

        public static readonly string ValidateCodeFailure = "验码失败,请扫描正确码";

        public static readonly string NoData = "无数据";


        #endregion Common

        #region MyRegion Settlement
        /// <summary>
        /// 无可提现的数据,请刷新重进
        /// </summary>
        public static readonly string NoApplyData = "您暂无可提现的余额！";

        /// <summary>
        /// 无可提现的数据,请刷新重进
        /// </summary>
        public static readonly string ApplyAmountChange = "提现的金额已变动,请刷新重试！";

        /// <summary>
        /// 查询不到银行卡信息,请刷新重试！
        /// </summary>
        public static readonly string NoSearchBank = "查询不到门店银行卡信息,请刷新重试！";

        /// <summary>
        /// 银行卡信息已经改变,请刷新重试！
        /// </summary>
        public static readonly string BankChange = "门店银行卡信息已经改变,请刷新重试！";

        /// <summary>
        /// 发起提现成功!
        /// </summary>
        public static readonly string ApplySuccess = "发起提现成功！";

        /// <summary>
        /// 发起提现成功!
        /// </summary>
        public static readonly string PayFailure= "发起提现失败！";

        #endregion
    }
}
