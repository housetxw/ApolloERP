using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.ShopProduct
{
    public class ShopProductVo:BaseShopProductVo
    {
        /// <summary>
        /// 图片信息
        /// </summary>
        public List<string> Images { get; set; }

        #region 类目信息

        /// <summary>
        /// 一级类目Id
        /// </summary>
        public int MainCategoryId { get; set; }

        /// <summary>
        /// 顶级类目Code
        /// </summary>
        public string MainCategoryCode { get; set; }
        /// <summary>
        /// 顶级类目ShortCode
        /// </summary>
        public string MainCategoryShortCode { get; set; }
        /// <summary>
        /// 顶级类目名称
        /// </summary>
        public string MainCategoryName { get; set; }


        /// <summary>
        /// 二级类目Id
        /// </summary>
        public int SecondCategoryId { get; set; }
        /// <summary>
        /// 关联类目Code
        /// </summary>
        public string SecondCategoryCode { get; set; }

        /// <summary>
        /// 关联类目ShortCode
        /// </summary>
        public string SecondCategoryShortCode { get; set; }
        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string SecondCategoryName { get; set; }


        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int ChildCategoryId { get; set; }

        /// <summary>
        /// 关联类目Code
        /// </summary>
        public string ChildCategoryCode { get; set; }

        /// <summary>
        /// 关联类目ShortCode
        /// </summary>
        public string ChildCategoryShortCode { get; set; }

        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string ChildCategoryName { get; set; }

        #endregion
    }
}
