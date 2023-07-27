using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
   public class UploadByteRequest
    {
        public byte[] Bytes { get; set; }

        public string FileName { get; set; }

    }
}
