using Microsoft.AspNetCore.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using ApolloErp.Web.WebApi;
using Ae.FileUpload.Api.Common.Exceptions;
using Ae.FileUpload.Api.Common.Util;
using Ae.FileUpload.Api.Core.Interfaces;
using Ae.FileUpload.Api.Core.Model;
using Ae.FileUpload.Api.Core.Request.QiNiu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FileUpload.Api.Imp.Services
{
    public class QiNiuService : IQiNiuService
    {
        private const string AccessKey = "6lAkQuitIJayl-***";//管理文件 认证
        private const string SecretKey = "wGiDzEoH995Wwx-***";//管理文件 认证
       // private const string Domain = "https://m.aerp.com.cn";//文件管理 外链默认域名

        private string ImageDomain = "http://image.aerp.com.cn";
        private string VideoDomain = "http://video.aerp.com.cn";



        private string ImageBucket = "image";//空间名，可以是公开或者私有的
        private string VideoBucket = "video";
        public QiNiuService()
        {
            // AK = ACCESS_KEY
            // USE_HTTPS = (true|false) 是否使用HTTPS
            // 使用前请确保AK和BUCKET正确，否则此函数会抛出异常(比如code612/631等错误)
            Qiniu.Common.Config.AutoZone(AccessKey, ImageBucket, true);
            Qiniu.Common.Config.AutoZone(AccessKey, VideoBucket, true);
        }

        //public Tuple<string, string> GetBucketAndHostByFileName(string fileName)
        //{
        //    if (fileName.ToLower().Contains(".jpg") ||
        //        fileName.ToLower().Contains(".gif") ||
        //        fileName.ToLower().Contains(".png") ||
        //        fileName.ToLower().Contains(".bmp") ||
        //        fileName.ToLower().Contains(".jpeg"))
        //    {
        //        return new Tuple<string, string>(ImageDomain, ImageBucket);
        //    }
        //    else if (fileName.ToLower().Contains(".mp4") ||
        //        fileName.ToLower().Contains(".mov"))
        //    {
        //        return new Tuple<string, string>(VideoDomain, VideoBucket);
        //    };
        //    return new Tuple<string, string>(ImageDomain, ImageBucket);
        //}


        /// <summary>
        /// 获取七牛GetToken 
        /// </summary>
        /// <param name="directory">上传目录名称  eg： ProductImage</param>
        /// <param name="fileName">文件名称 eg： 265651.jpg</param>
        /// <param name="extensionName">文件扩展名称 jpg png </param>
        /// <returns></returns>
        public ApiResult<QiNiuTokenVo> GetQiNiuToken(string directory, string fileName, string extensionName)
        {
            if (string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(extensionName))
            {
                return Result.Failed<QiNiuTokenVo>("文件名称和扩展名称必须填写一个！");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString().ToLower() + "." + extensionName;
            }

            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = directory + "/" + fileName;
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = ImageBucket;//+ ":" + saveKey;
                                     // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            var data = new QiNiuTokenVo()
            {
                Host = ImageDomain,
                Token = token
            };
            return Result.Success<QiNiuTokenVo>(data);
        }


        /// <summary>
        /// 图片文件流上传
        /// </summary>
        public string UploadImageStream(IFormFile formFile, string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new CustomException("directory 必填！");
            }

            if (string.IsNullOrEmpty(formFile.FileName))
            {
                throw new CustomException("没有获取到文件");
            }

            if (!ImageUtil.CheckImageFormat(formFile.FileName))
            {
                throw new CustomException("不允许的图片格式");
            }

            long fileSize = formFile.Length / 1024;
            if (fileSize > 1024)
            {
                throw new CustomException("图片大小不允许超过1M");
            }

            string fileName = formFile.FileName;

            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = directory + "/" + fileName;
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = ImageBucket;
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

        public string UploadImageBytes(UploadByteRequest request)
        {
            if (string.IsNullOrEmpty(request.FileName))
            {
                throw new CustomException("directory 必填！");
            }

            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = request.FileName;

            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = ImageBucket;
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
                var result = fu.UploadData(request.Bytes, saveKey, token);
                if (result.Code == 200)
                {
                    returnUrl = saveKey;
                }
                return returnUrl;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
            return "";

        }

        public async Task<string> UploadUrl(UploadUrlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url))
            {
                throw new CustomException("Url不可为空");
            }

            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = $"{Guid.NewGuid().ToString("N")}";

            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = ImageBucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            var returnUrl = string.Empty;
            try
            {
                Stream stream = null;
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(request.Url).Result;
                    stream = await response.Content.ReadAsStreamAsync();
                    //var mediaType = response.Content.Headers.ContentType.MediaType;
                }
                if (stream == null)
                {
                    throw new CustomException("未获取到原资源");
                }
                FormUploader fu = new FormUploader();
                var result = await fu.UploadStreamAsync(stream, saveKey, token);
                if (result.Code == 200)
                {
                    returnUrl = saveKey;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
            return returnUrl;
        }

        public async Task<ApiResult<string>> UploadVideoStream(UploadVideoRequest request)
        {
            var res = new ApiResult<string>();
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac(AccessKey, SecretKey);
            string saveKey = request.FileName;
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = VideoBucket;
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
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(request.Url);
                var tokenRes = await response.Content.ReadAsStreamAsync();
                if (string.IsNullOrEmpty(request.FileName))
                {
                    throw new CustomException("directory 必填！");
                }

                FormUploader fu = new FormUploader();
                var result = await fu.UploadStreamAsync(tokenRes, saveKey, token);
                if (result.Code == 200)
                {
                    returnUrl = saveKey;
                }
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                res.Message = ex.ToString();
            }
            return res;
        }
    }
}
