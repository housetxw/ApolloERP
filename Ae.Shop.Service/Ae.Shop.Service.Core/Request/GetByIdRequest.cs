using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetByIdRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID不能为空")]
        public long Id { get; set; }
    }
}
