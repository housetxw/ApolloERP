using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Common.Log;
using StackExchange.Redis;

namespace Ae.B.Login.Api.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly ApolloErpLogger<BaseController> logger;

        public BaseController() { }

        public BaseController(ApolloErpLogger<BaseController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// StackExchange.Redis Instance
        /// </summary>
        //protected IDatabase RedisClient { get; } = RedisClientManager.RedisClient;


        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string GetClientIpAddress(IHttpContextAccessor accessor)
        {
            var ip = accessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip ?? "";
        }

        /// <summary>
        /// 获取客户端机器名
        /// </summary>
        /// <returns></returns>
        protected string GetClientMachineName(IHttpContextAccessor accessor)
        {
            string hostName;
            try
            {
                hostName = Dns.GetHostEntry(GetClientIpAddress(accessor)).HostName;
                if (hostName.IsNotNullOrWhiteSpace())
                {
                    IPAddress ip = IPAddress.Parse(GetClientIpAddress(accessor));
                    IPHostEntry hosts = Dns.GetHostEntry(ip);
                    List<string> compNames = hosts.HostName.Split('.').ToList();
                    hostName = compNames.First();
                }
            }
            catch
            {
                hostName = Environment.MachineName;
            }
            return hostName ?? "";
        }

        /// <summary>
        /// 获取客户端操作系统的名字
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        protected string GetClientOSPlatform(IHttpContextAccessor accessor)
        {
            var osVersion = "";
            var userAgent = accessor.HttpContext.Request.Headers["User-Agent"].FirstOrDefault();

            #region Get OS Version

            if (userAgent.Contains("NT 10.0"))
            {
                osVersion = "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8.1";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                osVersion = "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = userAgent.Contains("64") ? "Windows XP" : "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            else
            {
                osVersion = "";
            }
            #endregion

            return osVersion;
        }

        /// <summary>
        /// 获取客户端User-Agent
        /// </summary>
        /// <returns></returns>
        protected string GetClientBrowser(IHttpContextAccessor accessor)
        {
            var browser = accessor.HttpContext.Request.Headers["User-Agent"].FirstOrDefault();

            return string.IsNullOrWhiteSpace(browser) ? "" : browser;
        }

    }
}