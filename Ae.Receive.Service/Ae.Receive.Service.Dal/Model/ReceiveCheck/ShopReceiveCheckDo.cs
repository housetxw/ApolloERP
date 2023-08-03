using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_check")]
    public class ShopReceiveCheckDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 到店记录Id
        /// </summary>
        [Column("receive_id")]
        public long ReceiveId { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 员工Id
        /// </summary>
        [Column("person_id")]
        public string PersonId { get; set; } = string.Empty;
        /// <summary>
        /// 员工姓名
        /// </summary>
        [Column("person_name")]
        public string PersonName { get; set; } = string.Empty;
        /// <summary>
        /// 检查状态0待提交 1已提交
        /// </summary>
        [Column("check_status")]
        public int CheckStatus { get; set; }
        /// <summary>
        /// 客户描述
        /// </summary>
        [Column("narration")]
        public string Narration { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Column("user_tel")]
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Column("car_id")]
        public string CarId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        [Column("car_plate")]
        public string CarPlate { get; set; } = string.Empty;
        /// <summary>
        /// 公里数
        /// </summary>
        [Column("mileage")]
        public int Mileage { get; set; }
        /// <summary>
        /// VIN码
        /// </summary>
        [Column("vin_code")]
        public string VinCode { get; set; } = string.Empty;
        /// <summary>
        /// 仪表盘
        /// </summary>
        [Column("dashboard_img")]
        public string DashboardImg { get; set; } = string.Empty;
        /// <summary>
        /// 技师签字
        /// </summary>
        [Column("technician_signature")]
        public string TechnicianSignature { get; set; } = string.Empty;
        /// <summary>
        /// 客户签字
        /// </summary>
        [Column("customer_signature")]
        public string CustomerSignature { get; set; } = string.Empty;
        /// <summary>
        /// 质检签字
        /// </summary>
        [Column("zhijian_signature")]
        public string ZhiJianSignature { get; set; } = string.Empty;
        /// <summary>
        /// 提交渠道0门店管家 1小程序
        /// </summary>
        [Column("submit_channel")]
        public int SubmitChannel { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Column("version_num")]
        public int VersionNum { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
