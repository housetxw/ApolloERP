using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request.QiNiu;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    ///  七牛接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(QiNiuController))]
    [ApiController]
    public class QiNiuController : ControllerBase
    {
        private readonly IQiNiuService _qiNiuService;
        /// <summary>
        /// 构造方法
        /// </summary>
        public QiNiuController(IQiNiuService qiNiuService)
        {
            _qiNiuService = qiNiuService;
        }

        /// <summary>
        /// 获取七牛GetToken 
        /// </summary>
        /// <param name="directory">上传目录名称  eg： ProductImage</param>
        /// <param name="fileName">文件名称 eg： 265651.jpg</param>
        /// <param name="extensionName">文件扩展名称 jpg png </param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<QiNiuTokenVo> GetQiNiuToken(string directory, string fileName,string extensionName)
        {
            return _qiNiuService.GetQiNiuToken(directory, fileName, extensionName);
        }

        /// <summary>
        /// 文件流上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> UploadStream([FromForm]UploadStreamRequest request)
        {
            var result = _qiNiuService.UploadStream(request.File, request.Directory);
            return Result.Success<string>(result);
        }
    }
}