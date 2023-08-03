using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Common.Constant
{
    public class CommonConstant
    {
        #region Common

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
        /// 未捕获异常
        /// </summary>
        public static readonly string NotCatchException = "Comment:系统异常,请联系管理员！";


        #endregion Common

        #region 门店评论

        /// <summary>
        /// 商家已经评论过
        /// </summary>
        public const string ShopCommentIsExist = "商家已经评论完成，不允许重复评论！";

        /// <summary>
        /// 商家已经评论过
        /// </summary>
        public const string ShopAppendCommentIsExist = "商家已追评完成，不允许重复追评！";

        /// <summary>
        /// 用户点评信息不存在,不允许点评
        /// </summary>
        public const string CommentIsNotExist = "用户点评信息不存在,不允许点评！";

        /// <summary>
        /// 用户追评的信息不存在,不允许追评
        /// </summary>
        public const string CustomeAppendIsNotExist = "用户追评的信息不存在,不允许追评！";


        /// <summary>
        /// 提交成功
        /// </summary>
        public const string CommentSubmitSuccess = "提交成功！";

        /// <summary>
        /// 提交失败
        /// </summary>
        public const string CommentSubmitFailure = "提交失败！";
        #endregion


    }
}
