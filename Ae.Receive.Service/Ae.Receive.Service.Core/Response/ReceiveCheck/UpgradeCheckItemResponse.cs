using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class UpgradeCheckItemResponse
    {
        /// <summary>
        /// checkId
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 项Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 检查项目
        /// </summary>
        public List<UpgradeCheckproject> Children { get; set; } = new List<UpgradeCheckproject>();
    }

    /// <summary>
    /// 检查项目
    /// </summary>
    public class UpgradeCheckproject
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 显示描述
        /// </summary>
        public string DisplayDes { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 异常图片
        /// </summary>
        public List<string> ErrorImages { get; set; }

        /// <summary>
        /// 是否是检查项
        /// </summary>
        public bool CheckItemMain { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public List<CheckSubItem> CheckSubItems { get; set; }

        /// <summary>
        /// 子项
        /// </summary>
        public List<UpgradeCheckproject> Children { get; set; }
    }

    /// <summary>
    /// 结果选项
    /// </summary>
    public class CheckSubItem
    {
        /// <summary>
        /// input检查项数量
        /// </summary>
        public int CheckCount { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckItemMainId { get; set; }

        /// <summary>
        /// 检查子项Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 0单选 1多选
        /// </summary>
        public int CheckType { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Chosen { get; set; }

        /// <summary>
        /// input输入框校验提示语
        /// </summary>
        public string LimitMessage { get; set; }

        /// <summary>
        /// 是否需要计算 （true 需要调接口计算）
        /// </summary>
        public bool NeedCompute { get; set; }

        /// <summary>
        /// 是否需要图片
        /// </summary>
        public int NeedPhoto { get; set; }

        /// <summary>
        /// 选项类型
        /// 操作类型 
        /// input-num,
        /// input-txt,
        /// checkbox,
        /// checkbox-input-num,
        /// checkbox-input-txt,
        /// checkbox-scancode-battery,
        /// image,
        /// radio
        /// </summary>
        public string OptType { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// 异常对应得维修建议
        /// </summary>
        public string Suggestion { get; set; }

        /// <summary>
        /// 结果值
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// 结果词
        /// </summary>
        public List<CheckResultWord> ResultWords { get; set; }
    }

    /// <summary>
    /// 检查结果词
    /// </summary>
    public class CheckResultWord
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
    }
}
