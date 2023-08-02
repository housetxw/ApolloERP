using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// EditGrouponProductRequest
    /// </summary>
    public class EditGrouponProductRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public sbyte? IsDeleted { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }
}
