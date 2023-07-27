using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class BankInformationDTO
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; } = string.Empty;
        /// <summary>
        /// 银行logo
        /// </summary>
        public string IconUrl { get; set; } = string.Empty;
    }
}
