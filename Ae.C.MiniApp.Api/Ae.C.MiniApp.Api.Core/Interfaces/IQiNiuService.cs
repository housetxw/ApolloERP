using Microsoft.AspNetCore.Http;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Interfaces
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
        string UploadStream(IFormFile formFile, string directory);
    }
}
