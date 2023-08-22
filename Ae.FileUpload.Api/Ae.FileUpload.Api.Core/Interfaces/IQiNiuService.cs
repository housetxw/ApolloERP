using Microsoft.AspNetCore.Http;
using ApolloErp.Web.WebApi;
using Ae.FileUpload.Api.Core.Model;
using Ae.FileUpload.Api.Core.Request.QiNiu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FileUpload.Api.Core.Interfaces
{
    public interface IQiNiuService
    {
        /// <summary>
        /// 获取七牛GetToken 
        /// </summary>
        /// <param name="directory">上传目录名称  eg： ProductImage</param>
        /// <param name="fileName">文件名称 eg： 265651.jpg</param>
        /// <param name="extensionName">文件扩展名称 jpg png </param>
        /// <returns></returns>
        ApiResult<QiNiuTokenVo> GetQiNiuToken(string directory, string fileName, string extensionName);

        /// <summary>
        /// 文件流上传
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <param name="directory">目录</param>
        /// <returns></returns>
        string UploadImageStream(IFormFile formFile, string directory);

        /// <summary>
        /// 二进制数据上传
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <param name="directory">目录</param>
        /// <returns></returns>
        string UploadImageBytes(UploadByteRequest request);

        /// <summary>
        /// 统一资源定位地址上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> UploadUrl(UploadUrlRequest request);

        Task<ApiResult<string>> UploadVideoStream(UploadVideoRequest request);
    }
}
