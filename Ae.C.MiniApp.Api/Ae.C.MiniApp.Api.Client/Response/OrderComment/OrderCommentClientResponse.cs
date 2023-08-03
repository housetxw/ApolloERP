using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class OrderCommentClientResponse
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
        public List<OrderCommentDetailTechFormDTO> Techs { get; set; }
        /// <summary>
        /// 门店明细表单
        /// </summary>
        public OrderCommentDetailShopFormDTO Shop { get; set; }
        /// <summary>
        /// 商品明细表单集合
        /// </summary>
        public List<OrderCommentDetailProductFormDTO> Products { get; set; }
        /// <summary>
        /// 奖励规则名称
        /// </summary>
        public string RewardRuleName { get; set; }
    }
}
