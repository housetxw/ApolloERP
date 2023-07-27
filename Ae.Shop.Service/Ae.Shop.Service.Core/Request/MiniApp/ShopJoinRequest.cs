using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ShopJoinRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Required(ErrorMessage = "门店名称不能为空")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 门店详细地址
        /// </summary>
        public string ShopAddress { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区县名称
        /// </summary>
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 图片
        /// </summary>
        public string ShopPic { get; set; } = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; } = string.Empty;
        /// <summary>
        /// 门店店长
        /// </summary>
        public string ShopDirector { get; set; } = string.Empty;
        /// <summary>
        /// 店长联系电话
        /// </summary>
        public string ShopDirectorTel { get; set; } = string.Empty;
        /// <summary>
        /// 是否同意合作
        /// </summary>
        public sbyte IsAgredCooperate { get; set; }
        /// <summary>
        /// 是否同意加盟
        /// </summary>
        public sbyte IsAgreeJoin { get; set; }
        /// <summary>
        /// 月均漆面数
        /// </summary>
        public int AvgQimian { get; set; }
        /// <summary>
        /// 烤房数
        /// </summary>
        public int CountKaofang { get; set; }
        /// <summary>
        /// 美容工位数
        /// </summary>
        public int CountBeauty { get; set; }
        /// <summary>
        /// 四轮定位工位数
        /// </summary>
        public int CountAlignment { get; set; }
        /// <summary>
        /// 轮保工位数
        /// </summary>
        public int CountMaintenance { get; set; }
        /// <summary>
        /// 技师数
        /// </summary>
        public int CountTech { get; set; }
        /// <summary>
        /// 门店面积
        /// </summary>
        public int AreaShop { get; set; }
        /// <summary>
        /// 服务类型 1:轮胎保养 2:美容安装 3:洗车 4:钣钣喷漆
        /// </summary>
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 平台店名
        /// </summary>
        public string RedShopName { get; set; } = string.Empty;
        /// <summary>
        /// 门店性质 1:快修快保,2:汽车厂,3:4S店 ,4:社区服务店
        /// </summary>
        public string ShopNature { get; set; } = string.Empty;
        /// <summary>
        /// 门店资质 1:营业执照, 2:税务登记证, 3:道路运输许可证
        /// </summary>
        public string ShopCredential { get; set; } = string.Empty;
        /// <summary>
        /// 签约对象:1:非大型连锁店,2:非街边三无营业店,3:以证件齐全加盟店为主
        /// </summary>
        public string ShopContractTarget { get; set; } = string.Empty;
        /// <summary>
        /// 法人身份证号
        /// </summary>
        public string CorporateId { get; set; } = string.Empty;
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccountCode { get; set; } = string.Empty;
        /// <summary>
        /// 开户名称
        /// </summary>
        public string BankAccountName { get; set; } = string.Empty;
        /// <summary>
        /// 开户行及支行
        /// </summary>
        public string BankBranch { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 审核人
        /// </summary>
        public string ConfirmUser { get; set; } = string.Empty;
        /// <summary>
        /// 审核人手机号
        /// </summary>
        public string ConfirmUserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 创建人手机号
        /// </summary>
        public string CreatorPhone { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
