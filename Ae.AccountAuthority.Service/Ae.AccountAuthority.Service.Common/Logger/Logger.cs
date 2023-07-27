using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
//using log4net;
//using log4net.Config;
//using log4net.Core;

namespace Ae.AccountAuthority.Service.Common.Logger
{
    public class Logger
    {
        //private static readonly ILog logger;

        //static Logger()
        //{
        //    if (null == logger)
        //    {
        //        var repository = LoggerManager.CreateRepository("Ae.AccountAuthority.Service");
        //        XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

        //        //name: LoggerName, Optional
        //        var loggerInstance = LogManager.GetLogger(repository.Name, "");
        //        Interlocked.CompareExchange(ref logger, loggerInstance, null);
        //    }
        //}

        ///// <summary>
        ///// Debug日志
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="exception"></param>
        //public static void Debug(string message, Exception exception = null)
        //{
        //    if (null == exception)
        //    {
        //        logger.Debug(message);
        //    }
        //    else
        //    {
        //        logger.Debug(message, exception);
        //    }
        //}

        ///// <summary>
        ///// 普通日志
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="exception"></param>
        //public static void Info(string message, Exception exception = null)
        //{
        //    if (null == exception)
        //    {
        //        logger.Info(message);
        //    }
        //    else
        //    {
        //        logger.Info(message, exception);
        //    }
        //}

        ///// <summary>
        ///// 告警日志
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="exception"></param>
        //public static void Warn(string message, Exception exception = null)
        //{
        //    if (null == exception)
        //    {
        //        logger.Warn(message);
        //    }
        //    else
        //    {
        //        logger.Warn(message, exception);
        //    }
        //}

        ///// <summary>
        ///// 错误日志
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="exception"></param>
        //public static void Error(string message = "发生异常：", Exception exception = null)
        //{
        //    if (null == exception)
        //    {
        //        logger.Error(message);
        //    }
        //    else
        //    {
        //        logger.Error(message, exception);
        //    }
        //}

    }
}
