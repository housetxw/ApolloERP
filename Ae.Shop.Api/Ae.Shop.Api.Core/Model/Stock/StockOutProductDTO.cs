using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
  public  class StockOutProductDTO
    {
        public long BatchNo { get; set; }

        /// <summary>
        /// 扣减数量
        /// </summary>
        public long Num { get; set; }
    }
}
