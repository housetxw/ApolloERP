using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace Ae.C.Login.Api.Extension
{
    public class DefaultExceptionHandlerOptions : ExceptionHandlerOptions
    {
        public DefaultExceptionHandlerOptions(IHostingEnvironment env)
        {
            ExceptionHandler = (context) =>
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>();

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json;charset=utf-8";
                return context.Response.WriteAsync($"{{\"ErrCode\": 0,\"ErrMsg\": null,\"SubErrCode\": {context.Response.StatusCode},\"SubErrMsg\": \"{FormattingException(ex.Error)}\"}}");
            };
        }

        private static string FormattingException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ReplaceSpecialCharacter(string.Format("异常信息 {0}{1} 内部异常 {2}{3} 异常来源 {4}{5} 异常堆栈 {6}",
                        ex.Message,
                        Environment.CommandLine,
                        FormattingException(ex.InnerException),
                        Environment.CommandLine,
                        ex.Source,
                        Environment.CommandLine,
                        ex.StackTrace));
            }
            else
            {
                return ReplaceSpecialCharacter(string.Format("异常信息 {0}{1} 内部异常 {2}{3} 异常来源 {4}{5} 异常堆栈 {6}",
                        ex.Message,
                        Environment.CommandLine,
                        ex.InnerException,
                        Environment.CommandLine,
                        ex.Source,
                        Environment.CommandLine,
                        ex.StackTrace));
            }
        }

        /// <summary>  
        /// 过滤特殊字符  
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        private static string ReplaceSpecialCharacter(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        //在ASCⅡ码中，第0～31号及第127号(共33个)是控制字符或通讯专用字符
                        if ((c >= 0 && c <= 31) || c == 127)
                        {
                            //TODO
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }
    }
}

