using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Arrival
{
    /// <summary>
    /// 到店记录列表返回对象
    /// </summary>
    public class GetArrivalListResponse
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// CarId
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车标
        /// </summary>
        public string CarLogo { get; set; }

        /// <summary>
        /// 车系（EG:A4L-一汽大众奥迪）
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 接待人姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ShowServiceType { get; set; }

        /// <summary>
        /// 排队类型
        /// </summary>
        public string ShowQueueType { get; set; }

        /// <summary>
        /// 排对的号码
        /// </summary>
        public string QueueNumber { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public int ShowMinute { get; set; }

        /// <summary>
        /// 到店时间
        /// </summary>
        public string ShowArrivalDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// 检测Id
        /// </summary>
        public long CheckId { get; set; }
    }
}
