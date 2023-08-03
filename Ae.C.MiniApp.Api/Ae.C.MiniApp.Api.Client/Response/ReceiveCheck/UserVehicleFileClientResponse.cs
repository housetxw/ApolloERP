using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.ReceiveCheck
{
    public class UserVehicleFileClientResponse
    {
        /// <summary>
        /// 主数据
        /// </summary>
        public VehicleFileMainDataDto MainData { get; set; }

        /// <summary>
        /// 车辆部件概况
        /// </summary>
        public List<CarPartsSituationDto> CarPartsSituation { get; set; }

        /// <summary>
        /// 维修记录
        /// </summary>
        public MaintenancerecordDto Maintenancerecord { get; set; }
    }

    /// <summary>
    /// 主数据
    /// </summary>
    public class VehicleFileMainDataDto
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 客户手机号界面显示  可能脱敏
        /// </summary>
        public string UserTelDisplay { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车品牌url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 最近一次到店时间
        /// </summary>
        public string LastInTime { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// 到店次数
        /// </summary>
        public int ReceiveCount { get; set; }

        /// <summary>
        /// 总消费额
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 保险公司
        /// </summary>
        public string InsuranceCompany { get; set; }

        /// <summary>
        /// 保险到期日
        /// </summary>
        public string InsureExpireDate { get; set; }
    }

    /// <summary>
    /// 车辆部件概况
    /// </summary>
    public class CarPartsSituationDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 部件信息
        /// </summary>
        public List<CarPartsItemDto> Items { get; set; }
    }

    /// <summary>
    /// 部件
    /// </summary>
    public class CarPartsItemDto
    {
        /// <summary>
        /// 部件Id
        /// </summary>
        public int PartsId { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 状态 0正常  1异常
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 维修记录
    /// </summary>
    public class MaintenancerecordDto
    {
        /// <summary>
        /// 服务大类
        /// </summary>
        public List<ServiceCategoryDto> ServiceCategory { get; set; }

        /// <summary>
        /// 服务记录
        /// </summary>
        public List<ServiceRecordDto> ServiceRecord { get; set; }
    }

    /// <summary>
    /// 服务大类
    /// </summary>
    public class ServiceCategoryDto
    {
        /// <summary>
        /// 服务Code
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 服务记录
    /// </summary>
    public class ServiceRecordDto
    {
        /// <summary>
        /// 到店时间
        /// </summary>
        public string ReceiveTime { get; set; }

        /// <summary>
        /// 门店名
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int? Mileage { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfoDto> Tags { get; set; }

        /// <summary>
        /// 服务项目
        /// </summary>
        public List<ServiceProjectDto> Projects { get; set; }
    }

    /// <summary>
    /// 标签
    /// </summary>
    public class TagInfoDto
    {
        /// <summary>
        /// 标签CODE(EG:RGRecommend)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 标签名称(EG:推荐)
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 标签颜色(EG:#F37C3E)
        /// </summary>
        public string TagColor { get; set; }
    }

    /// <summary>
    /// 服务项目
    /// </summary>
    public class ServiceProjectDto
    {
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 施工技师
        /// </summary>
        public string TechName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalMoney { get; set; }
    }
}
