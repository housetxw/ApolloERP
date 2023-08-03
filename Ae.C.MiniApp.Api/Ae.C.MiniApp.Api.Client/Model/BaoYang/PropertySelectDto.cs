using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.BaoYang
{
    public class PropertySelectDto
    {
        /// <summary>
        /// 属性类型(TID或者Property)
        /// 如果为TID,查询赋值时直接使用Values数组中的Key
        /// 如果为Property,则修改车型Properties数组中数据
        /// [{"PropertyKey":"发动机","PropertyValue":"CDD（2009/05/01之后）"}]
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 筛选Title(EG:请选择年款)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 筛选说明(EG:您所选的车型存在多个年款，不同年款的车型，配件存在差异)
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 如果Type为Property时,对应车型Properties中PropertyKey值
        /// EG:年款
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 如果Type为Property时,Values中的KEY 对应车型Properties中PropertyValue值
        /// 如果Type为Tid是,Values中的KEY 对应车型Properties中Tid值
        /// </summary>
        public List<PropertyResultDto> Values { get; set; }
    }

    public class PropertyResultDto
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 值(EG:2015)
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 显示(EG:2015)
        /// </summary>
        public string DisplayValue { get; set; }
    }
}
