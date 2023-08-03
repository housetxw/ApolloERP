using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.ShopManage
{
    public class CompanyLessInfoDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 类型；0：公司；1门店；2：仓库；
        /// </summary>
        public int Type { get; set; }

        public int SystemType { get; set; }
    }
}
