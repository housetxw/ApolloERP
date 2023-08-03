using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response
{
    public class ShopAssetTypeOptionsResponse : ShopAssetTypeVO
    {
        public List<ShopAssetTypeVO> children { get; set; }
    }
}
