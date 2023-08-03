using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.SharingPromotion
{
    public class GetSharingSummaryRequest:BaseGetRequest
    {
     
        public  string UserId { get; set; }
    }
}
