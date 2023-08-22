using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.FileUpload.Api.Core.Interfaces;
using Ae.FileUpload.Api.Core.Model;
using Ae.FileUpload.Api.Core.Request.QiNiu;

namespace Ae.FileUpload.Api.Controllers
{
    /// <summary>
    ///  七牛接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(QiNiuController))]
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
        public ApiResult<QiNiuTokenVo> GetQiNiuToken(string directory, string fileName, string extensionName)
        {
            return _qiNiuService.GetQiNiuToken(directory, fileName, extensionName);
        }

        /// <summary>
        /// 文件流上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> UploadStream([FromBody]UploadStreamRequest request)
        {
            var result = _qiNiuService.UploadImageStream(request.File, request.Directory);
            return Result.Success<string>(result);
        }
        /// <summary>
        /// 二进制字节上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> UploadBytes([FromBody]UploadByteRequest request)
        {
            var result = _qiNiuService.UploadImageBytes(request);
            return Result.Success<string>(result);
        }

        /// <summary>
        /// 钉钉获取用户信息
        /// </summary>
        /// <param name="appkey"></param>
        /// <param name="appsecret"></param>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<OapiUserGetResponse> GetUserInfo(string appkey, string appsecret, string authCode)
        {
            //gettoken
            DefaultDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/gettoken");
            var req = new OapiGettokenRequest();
            req.Appkey = appkey;
            req.Appsecret = appsecret;
            req.SetHttpMethod("GET");
            OapiGettokenResponse res = client.Execute(req);
            var accessToken = res.AccessToken;
            //取得用户ID
            var c2 = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/getuserinfo");
            var req2 = new OapiUserGetuserinfoRequest();
            req2.Code = authCode;
            req2.SetHttpMethod("GET");
            var res2 = c2.Execute(req2, accessToken);
            String userId = res2.Userid;
            //取得用户详情
            var c3 = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/get");
            OapiUserGetRequest req3 = new OapiUserGetRequest();
            req3.Userid = userId;
            req3.SetHttpMethod("GET");
            OapiUserGetResponse res3 = c3.Execute(req3, accessToken);
            return Result.Success(res3);
        }

        /// <summary>
        /// 统一资源定位地址上传
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UploadUrl([FromBody]ApiRequest<UploadUrlRequest> request)
        {
            var result = await _qiNiuService.UploadUrl(request.Data);
            return Result.Success<string>(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UploadVideoStream([FromBody]UploadVideoRequest request)
        {
            return await _qiNiuService.UploadVideoStream(request);
        }
    }
}