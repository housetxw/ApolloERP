using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Ae.Product.Service.Common.Util
{
    public  class QiniuHelper
    {
        private readonly IConfiguration _configuration;
        private string AccessKey = "";//管理文件 认证
        private string SecretKey = "";//管理文件 认证
        private string Domain = "https://m.aerp.com.cn";//文件管理 外链默认域名
        private string Bucket = "ApolloErp";//空间名，可以是公开或者私有的
        private string Directory = "productImage/";//目录名称 默认可以空，存储到 /目录下


        public  QiniuHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
            AccessKey = _configuration["QiNiu:AccessKey"];
            SecretKey = _configuration["QiNiu:SecretKey"];
            Domain = _configuration["QiNiu:Domain"];
            Bucket = _configuration["QiNiu:Bucket"];
            Directory = _configuration["QiNiu:Directory"];
            // AK = ACCESS_KEY
            // USE_HTTPS = (true|false) 是否使用HTTPS
            // 使用前请确保AK和BUCKET正确，否则此函数会抛出异常(比如code612/631等错误)
            Qiniu.Common.Config.AutoZone(AccessKey, Bucket, true);
        }

        /// <summary>
        ///  简单上传-上传小文件
        /// </summary>
        public  string UploadFile(string filePath)
        {
         
            string extension = Path.GetExtension(filePath);
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + extension;
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = Directory + fileName;
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = Bucket ;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            var returnUrl = "";
            try
            {
                FormUploader fu = new FormUploader();
                HttpResult result = fu.UploadFile(filePath, saveKey, token);
                if (result.Code == 200)
                {
                    returnUrl = saveKey;
                }
                return returnUrl;
            }
            catch (Exception)
            {
                Console.WriteLine("图片不存在！");
            }
            return "";
        }

        
    }
}
