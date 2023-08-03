﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
  public  class AccountCheckExceptionHandleRequest
    {
        public long Id { get; set; }

        public string OrderNo { get; set; }

        public long LocationId { get; set; }

        public string LocationName { get; set; }

        public string UpdateBy { get; set; }

        public string Suggestion { get; set; }

        public string Status { get; set; }
    }
}
