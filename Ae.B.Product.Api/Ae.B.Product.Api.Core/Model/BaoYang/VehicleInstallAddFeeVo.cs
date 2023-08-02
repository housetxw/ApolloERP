using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// VehicleInstallAddFeeVo
    /// </summary>
    public class VehicleInstallAddFeeVo
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年款
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 发动机型号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 开始生产年份
        /// </summary>
        public string ListedYear { get; set; }

        /// <summary>
        /// 指导价
        /// </summary>
        public string GuidePrice { get; set; }

        /// <summary>
        /// 停止生产年份
        /// </summary>
        public string StopProductionYear { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务原价
        /// </summary>
        public string OriginalPrice { get; set; }

        /// <summary>
        /// 车型加价金额
        /// </summary>
        public string AdditionalPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 加价id
        /// </summary>
        public long InstallAddFeeId { get; set; }
    }
}
