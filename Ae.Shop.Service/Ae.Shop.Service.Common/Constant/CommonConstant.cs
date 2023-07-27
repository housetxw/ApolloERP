using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Common.Constant
{
    public class CommonConstant
    {
        // ---------------------------------- Common --------------------------------------
        /// <summary>
        /// ;
        /// </summary>
        public static readonly string SeparatorSemicolon = ";";

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
        /// '
        /// </summary>
        public static readonly string SeparatorSingleQuotes = "'";

        /// <summary>
        /// 00000000-0000-0000-0000-000000000000
        /// </summary>
        public static readonly string DefaultGuidStr = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// 0-0-0-0-0
        /// </summary>
        public static readonly string DefaultCustomGuidStr = "0-0-0-0-0";

        /// <summary>
        /// int: -1
        /// </summary>
        public static readonly int OneNeg = -1;

        /// <summary>
        /// int: 0
        /// </summary>
        public static readonly int Zero = 0;

        /// <summary>
        /// int: 1
        /// </summary>
        public static readonly int One = 1;

        /// <summary>
        /// int: 3
        /// </summary>
        public static readonly int Three = 3;

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
        /// int: 36
        /// </summary>
        public static readonly int ThirtySix = 36;

        /// <summary>
        /// int: 45
        /// </summary>
        public static readonly int FortyFive = 45;

        /// <summary>
        /// int: 50
        /// </summary>
        public static readonly int Fifty = 50;

        /// <summary>
        /// int: 60
        /// </summary>
        public static readonly int Sixty = 60;

        /// <summary>
        /// int:100
        /// </summary>
        public static readonly int OneHundred = 100;

        /// <summary>
        /// int:500
        /// </summary>
        public static readonly int FiveHundred = 500;

        /// <summary>
        /// int:1000
        /// </summary>
        public static readonly int OneThousand = 1000;

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
        /// 操作成功
        /// </summary>
        public static readonly string OperateSuccess = "操作成功";
        /// <summary>
        /// 操作失败
        /// </summary>
        public static readonly string OperateFailed = "操作失败";
        /// <summary>
        ///删除成功
        /// </summary>
        public static readonly string DeleteSuccess = "删除成功";
        /// <summary>
        /// 删除失败
        /// </summary>
        public static readonly string DeleteFailed = "删除失败";
        /// <summary>
        /// 查询成功
        /// </summary>
        public static readonly string QuerySuccess = "查询成功";
        /// <summary>
        /// 查询失败
        /// </summary>
        public static readonly string QueryFailed = "查询失败";

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
        /// 【发生异常】
        /// </summary>
        public const string ExceptionOccured = "【发生异常】";

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
        /// 请求参数错误
        /// </summary>
        public static readonly string ParameterMistake = "请求参数错误";
        /// <summary>
        /// 系统异常，请联系平台
        /// </summary>
        public static readonly string FriendlyMistake = "系统异常，请联系平台";
        /// <summary>
        /// 暂无数据
        /// </summary>
        public static readonly string NoData = "暂无数据";
        /// <summary>
        /// 数据已存在
        /// </summary>
        public static readonly string ExistData = "数据已存在";

        /// <summary>
        /// 未捕获异常
        /// </summary>
        public static readonly string NotCatchException = "Comment:系统异常,请联系管理员！";

        #region Check Format Pattern

        /// <summary>
        /// 检查是否是GUID格式字符串
        /// </summary>
        public const string PatternGuid = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";


        #endregion Check Format Pattern

        #region SHOP



        #endregion 

        #region SHOPService
        /// <summary>
        /// 服务已存在
        /// </summary>
        public static readonly string BaseServiceExistData = "服务已存在";


        #endregion 

        #region Employee
        /// <summary>
        /// 员工已经添加，请勿重复添加
        /// </summary>
        public static readonly string EmployeeExist = "员工已经存在，请勿重复添加";
        /// <summary>
        /// 添加员工失败
        /// </summary>
        public static readonly string AddEmployeeFailed = "添加员工失败";
        /// <summary>
        /// 编辑员工失败
        /// </summary>
        public static readonly string EditEmployeeFailed = "编辑员工失败";

        #endregion

        // ---------------------------------- DataBase --------------------------------------
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

        /// <summary>
        /// 服务器API内部错误
        /// </summary>
        public const string InternalAPIError = "服务器API内部错误";

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        public const string InternalError = "服务器内部错误";

        /// <summary>
        /// 服务器DB内部错误
        /// </summary>
        public const string InternalDBError = "服务器DB内部错误";


    }
}
