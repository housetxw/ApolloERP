﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Login.Api.Common.Constant
{
    public class CommonConstant
    {
        #region Common

        /// <summary>
        /// ,
        /// </summary>
        public static readonly string SeparatorComma = ",";

        /// <summary>
        /// |
        /// </summary>
        public static readonly string SeparatorVertical = "|";

        /// <summary>
        /// -
        /// </summary>
        public static readonly string SeparatorLineThrough = "-";

        /// <summary>
        /// int: 0
        /// </summary>
        public static readonly int Zero = 0;

        /// <summary>
        /// int: 1
        /// </summary>
        public static readonly int One = 1;

        /// <summary>
        /// int: 2
        /// </summary>
        public static readonly int Two = 2;

        /// <summary>
        /// int: 3
        /// </summary>
        public static readonly int Three = 3;

        /// <summary>
        /// int: 4
        /// </summary>
        public static readonly int Four = 4;

        /// <summary>
        /// int: 5
        /// </summary>
        public static readonly int Five = 5;

        /// <summary>
        /// int: 10
        /// </summary>
        public static readonly int Ten = 10;

        /// <summary>
        /// int: 20
        /// </summary>
        public static readonly int Twenty = 20;

        /// <summary>
        /// int: 45
        /// </summary>
        public static readonly int FortyFive = 45;

        /// <summary>
        /// int: 55
        /// </summary>
        public static readonly int FiftyFive = 55;

        /// <summary>
        /// int: 60
        /// </summary>
        public static readonly int Sixty = 60;

        /// <summary>
        /// int: 65
        /// </summary>
        public static readonly int SixtyFive = 65;

        /// <summary>
        /// int: 10000
        /// </summary>
        public static readonly int TenThousand = 10000;

        /// <summary>
        /// true
        /// </summary>
        public static readonly string TrueStr = "true";

        /// <summary>
        /// false
        /// </summary>
        public static readonly string FalseStr = "false";

        /// <summary>
        /// POST
        /// </summary>
        public static readonly string PostStr = "POST";

        /// <summary>
        /// GET
        /// </summary>
        public static readonly string GetStr = "GET";

        /// <summary>
        /// 登录成功
        /// </summary>
        public static readonly string LoginSuccess = "登录成功";

        /// <summary>
        /// 操作成功
        /// </summary>
        public static readonly string OperateSuccess = "操作成功";

        /// <summary>
        /// 操作失败
        /// </summary>
        public static readonly string OperateFailure = "操作失败";

        /// <summary>
        /// 查询成功
        /// </summary>
        public static readonly string QuerySuccess = "查询成功";

        /// <summary>
        /// ms
        /// </summary>
        public static readonly string MilliUnit = "ms";

        /// <summary>
        /// 发生错误
        /// </summary>
        public static readonly string ErrorOccured = "发生错误";

        /// <summary>
        /// 详细信息：
        /// </summary>
        public static readonly string DetailedInformation = "详细信息：";

        /// <summary>
        /// 位置详情：
        /// </summary>
        public static readonly string DetailedLocation = "位置详情：";

        /// <summary>
        /// 【异常信息】：
        /// </summary>
        public const string ExceptionMessage = "【异常信息】：";

        /// <summary>
        /// 未捕获异常
        /// </summary>
        public const string NotCatchException = "系统异常,请联系管理员！";

        /// <summary>
        /// 【发生异常】
        /// </summary>
        public const string ExceptionOccured = "【发生异常】";

        /// <summary>
        /// 【参数安全性校验失败】
        /// </summary>
        public const string ArgumentValidateFailed = "【参数安全性校验失败】";

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
        /// 【耗时】：
        /// </summary>
        public const string TimeConsume = "【耗时】：";

        /// <summary>
        /// 【ResultCode不是ResultCode.Success:10000】
        /// </summary>
        public const string ResultCodeFailed = "【ResultCode不是ResultCode.Success:10000】";

        /// <summary>
        /// 【API调用发生异常】
        /// </summary>
        public const string ResultCodeException = "【API调用发生异常】";

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        public const string InternalError = "服务器内部错误";

        /// <summary>
        /// 服务器API内部错误
        /// </summary>
        public const string InternalAPIError = "服务器API内部错误";

        /// <summary>
        /// 服务器DB内部错误
        /// </summary>
        public const string InternalDBError = "服务器DB内部错误";


        #endregion Common

        #region DataBase

        /// <summary>
        /// 【数据库查询操作异常】
        /// </summary>
        public const string DBQueryException = "【数据库查询操作异常】";

        /// <summary>
        /// 【数据库新增操作异常】
        /// </summary>
        public const string DBInsertException = "【数据库新增操作异常】";

        /// <summary>
        /// 【数据库更新操作异常】
        /// </summary>
        public const string DBUpdateException = "【数据库更新操作异常】";

        /// <summary>
        /// 【数据库删除操作异常】
        /// </summary>
        public const string DBDeleteException = "【数据库删除操作异常】";

        /// <summary>
        /// 【数据库查询操作失败】
        /// </summary>
        public const string DBQueryFailed = "【数据库查询操作失败】";

        /// <summary>
        /// 【数据库新增操作失败】
        /// </summary>
        public const string DBInsertFailed = "【数据库新增操作失败】";

        /// <summary>
        /// 【数据库更新操作失败】
        /// </summary>
        public const string DBUpdateFailed = "【数据库更新操作失败】";

        /// <summary>
        /// 【数据库更新未找到对应记录】
        /// </summary>
        public const string DBUpdateNotFound = "【数据库更新，未找到对应记录】";

        /// <summary>
        /// 【数据库删除操作失败】
        /// </summary>
        public const string DBDeleteFailed = "【数据库删除操作失败】";

        #endregion DataBase

        #region NoSQL Redis

        /// <summary>
        /// R.LAA:Login:
        /// Rg.LoginAuth.Api:Login:
        /// </summary>
        public static string RedisLoginFolder { get; set; } = "R.LAA:Login:";

        /// <summary>
        /// AC:
        /// AuthCode:
        /// </summary>
        public static string RedisLoginAuthCode { get; set; } = "AC:";

        /// <summary>
        /// PwdE:
        /// </summary>
        public static string RedisLoginPwdErr { get; set; } = "PwdE:";

        /// <summary>
        /// NL:
        /// Normal:
        /// </summary>
        public static string RedisLoginNormal { get; set; } = "NL:";

        /// <summary>
        /// NLSMS:
        /// NormalSMS:
        /// </summary>
        public static string RedisLoginNormalSMS { get; set; } = "NLSMS:";

        /// <summary>
        /// SMSChk:
        /// RedisSMSChk:
        /// </summary>
        public static string RedisSMSChk { get; set; } = "SMSChk:";

        /// <summary>
        /// N_FOUND_ACCT
        /// NOT_FOUND_ACCOUNT
        /// </summary>
        public static string RedisNotFoundAccount { get; set; } = "N_FOUND_ACCT";


        #endregion NoSQL Redis 

    }
}
