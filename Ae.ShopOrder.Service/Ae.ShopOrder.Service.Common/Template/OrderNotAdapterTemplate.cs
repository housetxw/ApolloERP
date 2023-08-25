using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Common.Template
{
    public class OrderNotAdapterTemplate
    {
        public readonly static string InitTemplate = @"

  {
    ""tips"": ""建议戴上耳机联系客服,边拨打，边录入不适配信息，021-55558888"",
    ""orderNo"": """",
    ""isHistory"": false,
    ""carInfoList"": [
      {
        ""name"": ""车架号"",
        ""code"": ""CarFrameNo"",
        ""exampleImgUrl"": null,
        ""imgList"": [
          {
            ""url"": """"
          }
        ]
      },
      {
        ""name"": ""车身照片"",
        ""code"": ""CarBody"",
        ""exampleImgUrl"": null,
        ""imgList"": [
          {
            ""url"": """"
          }
        ]
      }
    ],
    ""productList"": [
      {
        ""pid"": ""PIDXXXXXXX"",
        ""name"": ""套餐机油0001"",
        ""productImgList"": [
          {
            ""name"": ""新产品照片"",
            ""code"": ""New"",
            ""exampleImgUrl"": """",
            ""imgList"": [
              {
                ""url"": """"
              }
            ]
          },
          {
            ""name"": ""旧产品照片"",
            ""code"": ""Old"",
            ""exampleImgUrl"": """",
            ""imgList"": [
              {
                ""url"": """"
              }
            ]
          },
          {
            ""name"": ""新旧对比照片"",
            ""code"": ""NewOldComparison"",
            ""exampleImgUrl"": """",
            ""imgList"": [
              {
                ""url"": """"
              }
            ]
          }
        ],
        ""remark"": """",
        ""handleModeList"": [
          {
            ""name"": ""订单已取消(或部分取消)"",
            ""code"": ""OrderCancel"",
            ""check"": false
          },
          {
            ""name"": ""门店商品替换"",
            ""code"": ""ShopProductReplace"",
            ""check"": false
          },
          {
            ""name"": ""其他"",
            ""code"": ""Other"",
            ""check"": false
          }
        ]
      }
    ]
  }";





    }
}
