using Microsoft.AspNetCore.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class QiNiuService: IQiNiuService
    {
        private readonly IConfiguration _configuration;
        private string AccessKey = "";//管理文件 认证
        private string SecretKey = "";//管理文件 认证
        private string Domain = "http://m.aerp.com.cn";//文件管理 外链默认域名
        private string Bucket = "ApolloErp";//空间名，可以是公开或者私有的

        public QiNiuService(IConfiguration configuration)
        {
            this._configuration = configuration;
            AccessKey = _configuration["QiNiu:AccessKey"];
            SecretKey = _configuration["QiNiu:SecretKey"];
            Domain = _configuration["QiNiu:Domain"];
            Bucket = _configuration["QiNiu:Bucket"];
            // AK = ACCESS_KEY
            // USE_HTTPS = (true|false) 是否使用HTTPS
            // 使用前请确保AK和BUCKET正确，否则此函数会抛出异常(比如code612/631等错误)
            Qiniu.Common.Config.AutoZone(AccessKey, Bucket, true);
        }

        /// <summary>
        /// 获取七牛GetToken 
        /// </summary>
        /// <param name="directory">上传目录名称  eg： ProductImage</param>
        /// <param name="fileName">文件名称 eg： 265651.jpg</param>
        /// <param name="extensionName">文件扩展名称 jpg png </param>
        /// <returns></returns>
        public ApiResult<QiNiuTokenVo> GetQiNiuToken(string directory, string fileName, string extensionName)
        {
            if (string.IsNullOrEmpty(fileName)&&string.IsNullOrEmpty(extensionName))
            {
                return Result.Failed<QiNiuTokenVo>("文件名称和扩展名称必须填写一个！");
            }
            if (!ImageUtil.CheckImageFormat(fileName))
            {
                return  Result.Failed<QiNiuTokenVo>("不允许的图片格式");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString().ToLower() + "." + extensionName;
            }
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = directory +"/"+ fileName;
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = Bucket;//+ ":" + saveKey;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            var data = new QiNiuTokenVo() { 
             Host= Domain, Token=token
            };
            return Result.Success<QiNiuTokenVo>(data);
        }


        /// <summary>
        /// 文件流上传
        /// </summary>
        public string UploadStream(IFormFile formFile, string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new CustomException("directory 必填！");
            }
            if (string.IsNullOrEmpty(formFile.FileName))
            {
                throw new CustomException("没有获取到文件");
            }
            string extension = Path.GetExtension(formFile.FileName);
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + extension;
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = directory +"/"+ fileName;
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
                using (var stream = formFile.OpenReadStream())
                {
                    // 请不要使用UploadManager的UploadStream方法，因为此流不支持查找(无法获取Stream.Length)
                    // 请使用FormUploader或者ResumableUploader的UploadStream方法
                    FormUploader fu = new FormUploader();
                    var result = fu.UploadStream(stream, saveKey, token);
                    if (result.Code == 200)
                    {
                        returnUrl = saveKey;
                    }
                }
                return returnUrl;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
            return "";
        }
    }
}
