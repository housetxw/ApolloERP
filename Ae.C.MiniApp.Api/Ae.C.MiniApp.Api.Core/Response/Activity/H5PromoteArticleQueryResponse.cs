using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Activity
{
    public class H5PromoteArticleQueryResponse
    {
        /// <summary>
        /// 推广文章ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 富文本
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 小程序码Base64字符串
        /// </summary>
        public string CodeBase64String { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 门店开通的服务项目名称集合
        /// </summary>
        public List<string> ShopServiceProjectNames { get; set; } = new List<string>();

        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeId { get; set; }
    }
}
