using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Client.Response
{
    public class GetPositionClientResponse
    {
        /// <summary>
        /// 状态码
        /// 0为正常,310请求参数信息有误，311Key格式错误,306请求有护持信息请检查字符串,110请求来源未被授权
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 状态说明
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 本次请求的唯一标识
        /// </summary>
        public string Request_Id { get; set; }
        /// <summary>
        /// 逆地址解析结果
        /// </summary>
        public PositionResult Result { get; set; }
    }
    public class PositionResult
    {
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 地址组成
        /// </summary>
        public AddressComponent Address_Component { get; set; }


        public AdInfo ad_info { get; set; }
    }

    public class AddressComponent 
    {
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string Street_Number { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string AdCode { get; set; }
    }

    public class AdInfo 
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string AdCode { get; set; }
    }
}
