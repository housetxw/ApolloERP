using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ae.C.MiniApp.Api.Client.Request.OrderComment
{
    public class GetTechCommentListClientRequest
    {
        /// <summary>
        /// 技师员工Id
        /// </summary>
        [Required(ErrorMessage = "技师员工Id不能为空")]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页大小必须大于0")]
        public int PageSize { get; set; } = 20;
    }
}
