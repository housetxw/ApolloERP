using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Common.Constant
{
    public class CommonConstant
    {
        // ---------------------------------- Common --------------------------------------
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
        /// int: 29
        /// </summary>
        public static readonly int TwentyNine = 29;

        /// <summary>
        /// int: 30
        /// </summary>
        public static readonly int Thirty = 30;

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
        /// 不是预期的结果
        /// </summary>
        public static readonly string NotExpectationResult = "不是预期的结果";

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
        public static readonly string NotCatchException = "Receive:系统异常,请联系管理员！";


        /// <summary>
        /// 用户不存在
        /// </summary>
        public static readonly string NoUser = "用户不存在";
        /// <summary>
        /// 操作成功
        /// </summary>
        public static readonly string OperateSuccess = "操作成功";
        /// <summary>
        /// 操作失败
        /// </summary>
        public static readonly string OperateFailed = "操作失败";
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
        /// 用户今天已有预约
        /// </summary>
        public static readonly string ReserveExist = "今天已存在预约";
        /// <summary>
        /// 新增预约成功
        /// </summary>
        public static readonly string AddReserveSuccess = "新增预约成功";
        /// <summary>
        /// 新增预约失败
        /// </summary>
        public static readonly string AddReserveFailed = "新增预约失败";

        /// <summary>
        /// 预约信息错误
        /// </summary>
        public static readonly string ReserveInfoError = "预约信息错误";

        /// <summary>
        /// 暂无预约信息
        /// </summary>
        public static readonly string NoReserveInfo = "暂无预约信息";
        /// <summary>
        /// 订单信息错误
        /// </summary>
        public static readonly string ErrorOrderInfo = "订单信息错误";
        /// <summary>
        /// 预约订单已支付
        /// </summary>
        public static readonly string OrderAlreadyPaid = "订单已支付";
        /// <summary>
        /// 预约取消成功
        /// </summary>
        public static readonly string ReserveCancelSuccess = "取消成功";
        /// <summary>
        /// 预约取消失败
        /// </summary>
        public static readonly string ReserveCancelFailed = "取消失败";
        /// <summary>
        /// 车辆信息错误
        /// </summary>
        public static readonly string UserCarError = "车辆信息错误";

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
