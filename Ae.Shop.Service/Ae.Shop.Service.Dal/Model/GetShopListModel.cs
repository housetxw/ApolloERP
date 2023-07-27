using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class GetShopListModel
    {
        /// <summary>
        /// 父级公司ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 门店简单名
        /// </summary>
        public string SimpleName { get; set; }

        /// <summary>
        /// 对外联系人手机号
        /// </summary>
        public string ExternalPhone { get; set; }
        /// <summary>
        /// 老板手机号
        /// </summary>
        public string OwnerPhone { get; set; }
        /// <summary>
        /// 门店类型  0所有  4上门养护
        /// </summary>
        public int ShopType { get; set; }
        /// <summary>
        /// 门店状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public int ServiceType { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;


        #region  新加字段
        
        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市ID
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区ID
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 0 下架 1 上架 2所有
        /// </summary>
        public int OnLineStatus { get; set; } = 2;

        /// <summary>
        /// 1待审核 2已通过审核 3未通过审核 4所有
        /// </summary>
        public int CheckStatus { get; set; } = 4;
        /// <summary>
        /// 门店负责人
        /// </summary>
        public string Head { get; set; } = string.Empty;
        #endregion

        /// <summary>
        /// 系统版本
        /// </summary>
        public int SystemType { get; set; } = -1;
    }
}
