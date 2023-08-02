using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 修改零件号
    /// </summary>
    public class UpdatePartCodeRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Id必须大于0")]
        public int Id { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [Required(ErrorMessage = "PartCode不能为空")]
        public string PartCode { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
    }
}
