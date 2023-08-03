using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class OrderCommentDetailTechSubmitDTO : BaseOrderCommentDetailSubmitDTO
    {
        /// <summary>
        /// 技师员工Id
        /// </summary>
        public long EmployeeId { get; set; }
    }
}
