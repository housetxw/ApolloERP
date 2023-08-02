using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    public class GetConfigAdvertisementsRequest : BasePageRequest
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Title { get; set; }

        public string TerminalType { get; set; }
    }
}
