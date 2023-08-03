using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Product
{
     public class ProductAllInfoDto: ProductBaseInfoDto
    {
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
        /// 关联类目名称
        /// </summary>
        public string SecondCategoryName { get; set; }


        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int ChildCategoryId { get; set; }

        /// <summary>
        /// 关联类目Id
        /// </summary>
        public string ChildCategoryCode { get; set; }


        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string ChildCategoryName { get; set; }

        #endregion
    }
}
