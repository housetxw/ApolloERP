using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_join")]
    public class ShopJoinDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 门店详细地址
        /// </summary>
        [Column("shop_address")]
        public string ShopAddress { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        [Column("province")]
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        [Column("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区县名称
        /// </summary>
        [Column("district")]
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 图片
        /// </summary>
        [Column("shop_pic")]
        public string ShopPic { get; set; } = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
        [Column("contact_name")]
        public string ContactName { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("contact_tel")]
        public string ContactTel { get; set; } = string.Empty;
        /// <summary>
        /// 门店店长
        /// </summary>
        [Column("shop_director")]
        public string ShopDirector { get; set; } = string.Empty;
        /// <summary>
        /// 店长联系电话
        /// </summary>
        [Column("shop_director_tel")]
        public string ShopDirectorTel { get; set; } = string.Empty;
        /// <summary>
        /// 是否同意合作
        /// </summary>
        [Column("is_agred_cooperate")]
        public sbyte IsAgredCooperate { get; set; }
        /// <summary>
        /// 是否同意加盟
        /// </summary>
        [Column("is_agree_join")]
        public sbyte IsAgreeJoin { get; set; }
        /// <summary>
        /// 月均漆面数
        /// </summary>
        [Column("avg_qimian")]
        public int AvgQimian { get; set; }
        /// <summary>
        /// 烤房数
        /// </summary>
        [Column("count_kaofang")]
        public int CountKaofang { get; set; }
        /// <summary>
        /// 美容工位数
        /// </summary>
        [Column("count_beauty")]
        public int CountBeauty { get; set; }
        /// <summary>
        /// 四轮定位工位数
        /// </summary>
        [Column("count_alignment")]
        public int CountAlignment { get; set; }
        /// <summary>
        /// 轮保工位数
        /// </summary>
        [Column("count_maintenance")]
        public int CountMaintenance { get; set; }
        /// <summary>
        /// 技师数
        /// </summary>
        [Column("count_tech")]
        public int CountTech { get; set; }
        /// <summary>
        /// 门店面积
        /// </summary>
        [Column("area_shop")]
        public int AreaShop { get; set; }
        /// <summary>
        /// 服务类型 1:轮胎保养 2:美容安装 3:洗车 4:钣钣喷漆
        /// </summary>
        [Column("service_type")]
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 平台店名
        /// </summary>
        [Column("red_shop_name")]
        public string RedShopName { get; set; } = string.Empty;
        /// <summary>
        /// 门店性质 1:快修快保,2:汽车厂,3:4S店 ,4:社区服务店
        /// </summary>
        [Column("shop_nature")]
        public string ShopNature { get; set; } = string.Empty;
        /// <summary>
        /// 门店资质 1:营业执照, 2:税务登记证, 3:道路运输许可证
        /// </summary>
        [Column("shop_credential")]
        public string ShopCredential { get; set; } = string.Empty;
        /// <summary>
        /// 签约对象:1:非大型连锁店,2:非街边三无营业店,3:以证件齐全加盟店为主
        /// </summary>
        [Column("shop_contract_target")]
        public string ShopContractTarget { get; set; } = string.Empty;
        /// <summary>
        /// 法人身份证号
        /// </summary>
        [Column("corporate_id")]
        public string CorporateId { get; set; } = string.Empty;
        /// <summary>
        /// 银行账号
        /// </summary>
        [Column("bank_account_code")]
        public string BankAccountCode { get; set; } = string.Empty;
        /// <summary>
        /// 开户名称
        /// </summary>
        [Column("bank_account_name")]
        public string BankAccountName { get; set; } = string.Empty;
        /// <summary>
        /// 开户行及支行
        /// </summary>
        [Column("bank_branch")]
        public string BankBranch { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 审核状态 1待审核 2已审核 3未通过
        /// </summary>
        [Column("is_confirmed")]
        public sbyte IsConfirmed { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("confirm_user")]
        public string ConfirmUser { get; set; } = string.Empty;
        /// <summary>
        /// 审核人手机号
        /// </summary>
        [Column("confirm_user_phone")]
        public string ConfirmUserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 审核备注
        /// </summary>
        [Column("confirm_remark")]
        public string ConfirmRemark { get; set; } = string.Empty;
        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("confir_time")]
        public DateTime ConfirTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 创建人手机号
        /// </summary>
        [Column("creator_Phone")]
        public string CreatorPhone { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
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
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}