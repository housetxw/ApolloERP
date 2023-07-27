using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
  public  class FirstConsumeDTO
    {
        public decimal Point { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// ShopUserRelation的主键
        /// </summary>
        public long RelationId { get; set; }
    }
}
