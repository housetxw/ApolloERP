using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class OrderCommentResponse
    {
        /// <summary>
        /// 服务车型
        /// </summary>
        public string CarName { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 技师明细表单
        /// </summary>
        public List<OrderCommentDetailTechFormVO> Techs { get; set; }
        /// <summary>
        /// 门店明细表单
        /// </summary>
        public OrderCommentDetailShopFormVO Shop { get; set; }
        /// <summary>
        /// 商品明细表单集合
        /// </summary>
        public List<OrderCommentDetailProductFormVO> Products { get; set; }
        /// <summary>
        /// 奖励规则名称
        /// </summary>
        public string RewardRuleName { get; set; }
    }
}
