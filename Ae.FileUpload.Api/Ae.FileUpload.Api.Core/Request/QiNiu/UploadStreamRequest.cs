using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FileUpload.Api.Core.Request.QiNiu
{
    public class UploadStreamRequest
    {
        public IFormFile File { get; set; }
        /// <summary>
        /// 文件目录
        /// </summary>
        public string Directory { get; set; }
    }
}
