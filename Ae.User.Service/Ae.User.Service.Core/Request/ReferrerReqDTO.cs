using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    public class ReferrerCustomerRequest
    {
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }
    }

    public class ReferrerCustomerListRequest: BasePageRequest
    {
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }
    }

    public class ShopReferrerCustomerReqDTO
    {
        [Required(ErrorMessage = "门店Id不能为空")]
        [Range(1, long.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public long ShopId { get; set; }

        //[Required(ErrorMessage = "当月日期不能为空")]
        //public string CurrentMonthDate { get; set; }
    }

    public class EmployeeReferrerCustomerPageReqDTO : BaseOnlyPageRequest
    {
        [Required(ErrorMessage = "门店Id不能为空")]
        [Range(1, long.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public long ShopId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }

}
