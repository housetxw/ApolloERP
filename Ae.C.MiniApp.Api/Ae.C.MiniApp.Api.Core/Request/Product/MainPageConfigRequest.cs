using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Web.WebApi;

namespace Ae.C.MiniApp.Api.Core.Request.Product
{
    /// <summary>
    /// MainPageConfigRequest
    /// </summary>
    public class MainPageConfigRequest : ApiRequest
    {
        /// <summary>
        /// Api版本号 
        /// </summary>
        public int Version { get; set; }
    }
}
