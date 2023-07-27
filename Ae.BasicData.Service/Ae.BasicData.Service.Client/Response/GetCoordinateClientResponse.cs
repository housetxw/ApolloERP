using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Client.Response
{
    public class GetCoordinateClientResponse
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
        /// 地址解析结果
        /// </summary>
        public CoordinateResult Result { get; set; }
    }

    public class CoordinateResult
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public CoordinateLocation Location { get; set; }
        /// <summary>
        /// AdCode
        /// </summary>
        public AdInfo ad_info { get; set; }
    }

    public class CoordinateLocation
    {
        /// <summary>
        /// 经度
        /// </summary>
        public string Lng { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Lat { get; set; }
    }
}
