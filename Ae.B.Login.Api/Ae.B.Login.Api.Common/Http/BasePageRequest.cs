using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Login.Api.Common.Http
{
    public class BasePageRequest : BaseRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, 1000, ErrorMessage = "每页数据必须在1至1000")]
        public int PageSize { get; set; } = 10;
    }

    public class BaseOnlyPageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, 1000, ErrorMessage = "每页数据必须在1至1000")]
        public int PageSize { get; set; } = 10;
    }

}
