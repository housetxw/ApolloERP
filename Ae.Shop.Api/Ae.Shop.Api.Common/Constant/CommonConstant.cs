using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Common.Constant
{
    public class CommonConstant
    {
        // ---------------------------------- Common --------------------------------------
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
        /// yyyy.M.d
        /// </summary>
        public const string FmtDTToYearMonthDayLowerWithPeriod = "yyyy.M.d";

        /// <summary>
        /// int: 0
        /// </summary>
        public const int Zero = 0;

        /// <summary>
        /// int: 1
        /// </summary>
        public const int One = 1;

        /// <summary>
        /// int: 2
        /// </summary>
        public const int Two = 2;

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
        /// int: 10
        /// </summary>
        public const int Ten = 10;

        /// <summary>
        /// int: 20
        /// </summary>
        public const int Twenty = 20;

        /// <summary>
        /// int: 29
        /// </summary>
        public const int TwentyNine = 29;

        /// <summary>
        /// int: 30
        /// </summary>
        public const int Thirty = 30;

        /// <summary>
        /// int: 45
        /// </summary>
        public const int FortyFive = 45;

        /// <summary>
        /// int: 55
        /// </summary>
        public const int FiftyFive = 55;

        /// <summary>
        /// int: 60
        /// </summary>
        public const int Sixty = 60;

        /// <summary>
        /// int: 65
        /// </summary>
        public const int SixtyFive = 65;

        /// <summary>
        /// int: 90
        /// </summary>
        public const int Ninety = 90;

        /// <summary>
        /// true
        /// </summary>
        public const string TrueStr = "true";

        /// <summary>
        /// false
        /// </summary>
        public const string FalseStr = "false";

        /// <summary>
        /// POST
        /// </summary>
        public const string PostStr = "POST";

        /// <summary>
        /// GET
        /// </summary>
        public const string GetStr = "GET";

        /// <summary>
        /// 不是预期的结果
        /// </summary>
        public const string NotExpectationResult = "不是预期的结果";

        /// <summary>
        /// 【发生异常】
        /// </summary>
        public const string ExceptionOccured = "【发生异常】";

        /// <summary>
        /// 【参数安全性校验失败】
        /// </summary>
        public const string ArgumentValidateFailed = "【参数安全性校验失败】";

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
        /// 无参数
        /// </summary>
        public const string ParameterNone = "无参数";
        /// <summary>
        /// 未捕获异常
        /// </summary>
        public const string NotCatchException = "Receive:系统异常,请联系管理员！";


        /// <summary>
        /// 用户不存在
        /// </summary>
        public const string NoUser = "用户不存在";
        /// <summary>
        /// 操作成功
        /// </summary>
        public const string OperateSuccess = "操作成功";
        /// <summary>
        /// 操作失败
        /// </summary>
        public const string OperateFailed = "操作失败";
        /// <summary>
        /// 删除失败
        /// </summary>
        public const string DeleteFailed = "删除失败";
        /// <summary>
        /// 查询成功
        /// </summary>
        public const string QuerySuccess = "查询成功";
        /// <summary>
        /// 查询失败
        /// </summary>
        public const string QueryFailed = "查询失败";
        /// <summary>
        /// 用户今天已有预约
        /// </summary>
        public const string ReserveExist = "今天已存在预约";

        public const string AddReserveSuccess = "新增预约成功";

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

        /// <summary>
        /// 无数据
        /// </summary>
        public const string NullData = "无数据";

        #region Sign

        /// <summary>
        /// 已经被签收 ； 特殊用途 跟APP 约定 以； 分割
        /// </summary>
        public const string PackageHaveSign = "已经被签收;";

        /// <summary>
        /// 包裹号不存在  ；特殊用途 跟APP 约定 以； 分割
        /// </summary>
        public const string PackageNotExist = "包裹号不存在;";

        /// <summary>
        /// 签收的包裹号不属于此门店 ；特殊用途 跟APP 约定 以； 分割
        /// </summary>
        public const string PackageIsNotShop = "签收的包裹号不属于此门店！";

        /// <summary>
        /// 包裹号不存在
        /// </summary>
        public const string ValidatePackageIsNotExist = "包裹号不存在！";

        public const string IsNotShop = "不属于此门店！";
        #endregion
    }
}
