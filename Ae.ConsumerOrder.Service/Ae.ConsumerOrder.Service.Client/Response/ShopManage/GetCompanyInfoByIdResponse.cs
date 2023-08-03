using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Response
{
    public class GetCompanyInfoByIdResponse
    {
        /// <summary>
        /// 父级公司id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 公司等级 1总公司 2省公司 3市公司
        /// </summary>
        public byte Level { get; set; }
        /// <summary>
        /// 类型；1：公司；2：仓库；
        /// </summary>
        public byte Type { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 省id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区/县id
        /// </summary>
        public int DistrictId { get; set; }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName { get; set; } = string.Empty;
        /// <summary>
        /// 区名称
        /// </summary>
        public string DistrictName { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 许可证
        /// </summary>
        public int License { get; set; }
        /// <summary>
        /// 状态 0正常 1终止 2暂停
        /// </summary>
        public int Status { get; set; }
    }
}
