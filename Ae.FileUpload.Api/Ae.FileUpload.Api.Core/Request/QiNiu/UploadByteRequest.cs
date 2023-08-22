using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FileUpload.Api.Core.Request.QiNiu
{
    public class UploadByteRequest
    {
        public byte[] Bytes { get; set; }

        public string FileName { get; set; }
    }

    public class UploadVideoRequest
    {
        public string FileName { get; set; }

        public string Url { get; set; }
    }
}
