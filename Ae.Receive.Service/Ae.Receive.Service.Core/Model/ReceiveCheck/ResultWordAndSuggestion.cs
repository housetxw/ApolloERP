using Ae.Receive.Service.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultWordAndSuggestion
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CheckResultWord> ResultWords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string  Suggestion { get; set; }
    }
}
