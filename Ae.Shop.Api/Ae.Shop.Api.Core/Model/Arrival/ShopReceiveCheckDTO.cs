using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Arrival
{
    public class ShopReceiveCheckDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long ReceiveId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        public string PersonId { get; set; } = string.Empty;
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string PersonName { get; set; } = string.Empty;
        /// <summary>
        /// 检查状态0待提交 1已提交
        /// </summary>
        public int CheckStatus { get; set; }
        /// <summary>
        /// 客户描述
        /// </summary>
        public string Narration { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; } = string.Empty;
        /// <summary>
        /// 公里数
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// VIN码
        /// </summary>
        public string VinCode { get; set; } = string.Empty;
        /// <summary>
        /// 仪表盘
        /// </summary>
        public string DashboardImg { get; set; } = string.Empty;
        /// <summary>
        /// 技师签字
        /// </summary>
        public string TechnicianSignature { get; set; } = string.Empty;
        /// <summary>
        /// 客户签字
        /// </summary>
        public string CustomerSignature { get; set; } = string.Empty;
        /// <summary>
        /// 提交渠道0门店管家 1小程序
        /// </summary>
        public int SubmitChannel { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int VersionNum { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
