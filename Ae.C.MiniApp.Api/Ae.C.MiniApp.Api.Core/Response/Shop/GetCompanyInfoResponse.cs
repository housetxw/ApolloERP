using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetCompanyInfoResponse
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型；0：公司；1门店；2：仓库；
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; }
    }
}
