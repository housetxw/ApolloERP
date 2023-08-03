using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
  public  class UpdateStoragePlanRequest
    {
        public long PlanId { get; set; }
        public List<StoragePlanProductDTO> Products { get; set; } = new List<StoragePlanProductDTO>();
    }
}
