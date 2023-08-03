using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    /// <summary>
    /// 临时Model！
    /// </summary>
   public class ProductTransferDTO
    {
        public bool IsDamage { get; set; }

        public int ActualNum { get; set; }

        public long InOutId { get; set; }

        public string CreateBy { get; set; }

    }
}
