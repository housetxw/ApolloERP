using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.BaoYang
{
    /// <summary>
    /// 保养适配分类大类
    /// </summary>
    public class BaoYangCategoryDto
    {
        /// <summary>
        /// 一级分类CODE(EG:Normal)
        /// </summary>
        public string CategoryType { get; set; }

        /// <summary>
        /// 一级类型名称(EG:常规保养项目)
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 一级类型简单名(EG:基础养护)
        /// </summary>
        public string SimpleCategoryName { get; set; }

        /// <summary>
        /// 保养项目
        /// </summary>
        public List<BaoYangPackageDto> PackageItems { get; set; }
    }
}
