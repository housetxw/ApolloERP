using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{

    /// <summary>
    /// 业务状态搜索订单请求对象
    /// </summary>
    public class GetOrderBaseInfoForBusinessStatusRequest : BasePageRequest
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        [Required(ErrorMessage = "业务状态不能为空")]
        public OrderBusinessStatusEnum OrderBusinessStatus { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Required(ErrorMessage = "门店Id不能为空")]
        public int ShopId { get; set; }

        /// <summary>
        /// 订单渠道
        /// </summary>
        public List<int> Channels { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 开始安装时间
        /// </summary>
        public string StarInstallTime { get; set; }

        /// <summary>
        /// 结束安装时间
        /// </summary>
        public string EndInstallTime { get; set; }

        /// <summary>
        /// 其他条件
        /// </summary>
        public string OtherSqlWhere { get; set; }
    }





    public enum OrderBusinessStatusEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 待签收
        /// </summary>
        WaitingSign = 1,
        /// <summary>
        /// 待派工
        /// </summary>
        WaitingDispatch = 2,
        /// <summary>
        /// 施工中
        /// </summary>
        ABuilding = 3,
        /// <summary>
        /// 待安装
        /// </summary>
        WaitingInstall = 4,
        /// <summary>
        /// 已安装
        /// </summary>
        Installed = 5,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 6,
        /// <summary>
        /// 待对账
        /// </summary>
        WaitingReconciliation = 7,
        /// <summary>
        /// 未知
        /// </summary>
        None = 8,

    }


    public class BasePageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页大小必须大于0")]
        public int PageSize { get; set; } = 20;
    }
}