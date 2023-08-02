using Ae.B.FMS.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Client.Request
{
  public  class CreateAccountCheckClientRequest
    {
        public List<AccountCheckDTO> Accounts { get; set; }
    }
}
