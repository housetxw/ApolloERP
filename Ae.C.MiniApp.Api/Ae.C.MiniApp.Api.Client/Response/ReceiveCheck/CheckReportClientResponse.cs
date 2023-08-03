using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.ReceiveCheck
{
    public class CheckReportClientResponse
    {
        /// <summary>
        /// 主数据
        /// </summary>
        public CheckReportMainDataDto MainData { get; set; }

        /// <summary>
        /// 附加检查数据（备胎/随车工具/贵重物品）
        /// </summary>
        public List<OtherProjectResultDto> OtherProjectResult { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public List<CheckResultClassifyDto> Items { get; set; }
    }

    /// <summary>
    /// 附加信息
    /// </summary>
    public class OtherProjectResultDto
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 检查项名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目Code
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 结果值 0未检查 1无 2有
        /// </summary>
        public decimal ResultValue { get; set; }

        /// <summary>
        /// 检查结果图片图片
        /// </summary>
        public List<string> Image { get; set; }
    }

    /// <summary>
    /// 检查报告主数据
    /// </summary>
    public class CheckReportMainDataDto
    {
        /// <summary>
        /// 检查记录Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 检查状态 0待提交  1已提交
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 客户手机号界面显示  可能脱敏
        /// </summary>
        public string UserTelDisplay { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车品牌url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 到店时间
        /// </summary>
        public string InTime { get; set; }

        /// <summary>
        /// <summary>
        /// Vin码
        /// </summary>
        public string VinCode { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// 仪表盘
        /// </summary>
        public string DashboardImg { get; set; }

        /// <summary>
        /// 客户描述
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 技师签字
        /// </summary>
        public string TechnicianSignature { get; set; }

        /// <summary>
        /// 客户签字
        /// </summary>
        public string CustomerSignature { get; set; }
    }

    /// <summary>
    /// 检查结果分类
    /// </summary>
    public class CheckResultClassifyDto
    {
        /// <summary>
        /// 结果词Code
        /// </summary>
        public string ResultWordCode { get; set; }

        /// <summary>
        /// 结果词显示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 检查大类
        /// </summary>
        public List<CheckClassfyDto> CheckClassfy { get; set; }
    }

    /// <summary>
    /// 检查大类
    /// </summary>
    public class CheckClassfyDto
    {
        /// <summary>
        /// 分类名称：内饰 外观  发动机舱……
        /// </summary>
        public string ClassfyName { get; set; }

        /// <summary>
        /// 分类Code
        /// </summary>
        public string ClassfyCode { get; set; }

        /// <summary>
        /// 具体检查项
        /// </summary>
        public List<CheckResultItemDto> ResultItems { get; set; }
    }

    /// <summary>
    /// 具体检查项
    /// </summary>
    public class CheckResultItemDto
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckItemId { get; set; }

        /// <summary>
        /// 检查项显示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 检查项描述
        /// </summary>
        public string DisplayDes { get; set; }

        /// <summary>
        /// KeyName
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string FunctionDes { get; set; }

        /// <summary>
        /// 异常图片
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public List<ErrorDesListDto> ErrorDesList { get; set; }

        /// <summary>
        /// 正常描述
        /// </summary>
        public List<string> NormalDes { get; set; }

        /// <summary>
        /// 建议描述
        /// </summary>
        public List<string> Suggestions { get; set; }
    }

    /// <summary>
    /// 异常描述
    /// </summary>
    public class ErrorDesListDto
    {
        /// <summary>
        /// 显示名称  前部 /后部
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public List<string> ErrorDes { get; set; }
    }
}
