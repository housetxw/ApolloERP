using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Common.Constant
{
    public class CommonConstant
    {
        // ---------------------------------- Common --------------------------------------

        public static readonly string NotCatchException = "C.MiniApp:系统异常";

        /// <summary>
        /// ,
        /// </summary>
        public const string SeparatorComma = ",";

        /// <summary>
        /// |
        /// </summary>
        public const string SeparatorVertical = "|";

        /// <summary>
        /// -
        /// </summary>
        public const string SeparatorLineThrough = "-";

        /// <summary>
        /// int: -1
        /// </summary>
        public const int NegOne = -1;

        /// <summary>
        /// int: 0
        /// </summary>
        public const int Zero = 0;

        /// <summary>
        /// int: 1
        /// </summary>
        public const int One = 1;

        /// <summary>
        /// int: 3
        /// </summary>
        public const int Three = 3;

        /// <summary>
        /// int: 4
        /// </summary>
        public const int Four = 4;

        /// <summary>
        /// int: 5
        /// </summary>
        public const int Five = 5;

        /// <summary>
        /// int: 8
        /// </summary>
        public const int Eight = 8;

        /// <summary>
        /// int: 10
        /// </summary>
        public const int Ten = 10;

        /// <summary>
        /// int: 20
        /// </summary>
        public const int Twenty = 20;

        /// <summary>
        /// int: 36
        /// </summary>
        public const int ThirtySix = 36;

        /// <summary>
        /// int: 45
        /// </summary>
        public const int FortyFive = 45;

        /// <summary>
        /// int: 50
        /// </summary>
        public const int Fifty = 50;

        /// <summary>
        /// int: 60
        /// </summary>
        public const int Sixty = 60;

        /// <summary>
        /// true
        /// </summary>
        public const string TrueStr = "true";

        /// <summary>
        /// false
        /// </summary>
        public const string FalseStr = "false";

        /// <summary>
        /// 发生错误
        /// </summary>
        public const string ErrorOccured = "发生错误";

        /// <summary>
        /// 详细信息：
        /// </summary>
        public const string DetailedInformation = "详细信息：";

        /// <summary>
        /// 所有参数详情：
        /// </summary>
        public const string ParameterModelDetail = "所有参数详情：";

        /// <summary>
        /// 请求参数详情：
        /// </summary>
        public const string ParameterReqDetail = "请求参数详情：";

        /// <summary>
        /// 【发生异常】
        /// </summary>
        public const string ExceptionOccured = "【发生异常】";

        /// <summary>
        /// 无参数
        /// </summary>
        public const string ParameterNone = "无参数";

        /// <summary>
        /// 【ResultCode不是ResultCode.Success:10000】
        /// </summary>
        public const string ResultCodeFailed = "【ResultCode不是ResultCode.Success:10000】";

        /// <summary>
        /// 【API调用发生异常】
        /// </summary>
        public const string ResultCodeException = "【API调用发生异常】";

        /// <summary>
        /// 未开启认证
        /// </summary>
        public const string UnenabledAuthectication = "未开启认证";

        // ---------------------------------- Check Format Pattern --------------------------------------
        /// <summary>
        /// 检查是否是GUID格式字符串
        /// </summary>
        public const string PatternGuid = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";

        /// <summary>
        /// 小程序ID
        /// </summary>
        public const string AppId = "wx2a3a6d4c560d5288";

        /// <summary>
        /// 
        /// </summary>
        public const string User = "管理员";
    }
}
