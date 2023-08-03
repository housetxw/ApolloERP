using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 检查报告
    /// </summary>
    public class CheckMainDataResponse
    {
        /// <summary>
        /// 主数据
        /// </summary>
        public CheckMainDataVo MainData { get; set; }

        /// <summary>
        /// 检查项目
        /// </summary>
        public List<ProjectClassifyVo> ProjectClassify { get; set; }
    }

    /// <summary>
    /// 项目分类
    /// </summary>
    public class ProjectClassifyVo
    {
        /// <summary>
        /// 分类名字
        /// </summary>
        public string ClassifyName { get; set; }

        /// <summary>
        /// 分类Code
        /// </summary>
        public string Classify { get; set; }

        /// <summary>
        /// 检查项目
        /// </summary>
        public List<CheckProjectVo> CheckProjects { get; set; }
    }

    /// <summary>
    /// 检查报告主数据
    /// </summary>
    public class CheckMainDataVo
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
    }

    /// <summary>
    /// 检查项目
    /// </summary>
    public class CheckProjectVo
    {
        /// <summary>
        /// 检查项
        /// </summary>
        public string CheckModule { get; set; }

        /// <summary>
        /// 检查项Code
        /// </summary>
        public string CheckModuleCode { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 状态  0：未检查   1：已检查
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 异常项数
        /// </summary>
        public int AbnormalCount { get; set; }
    }
}
