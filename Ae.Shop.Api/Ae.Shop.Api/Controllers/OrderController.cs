using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Response.Order;

namespace Ae.Shop.Api.Controllers
{

    [Route("[controller]/[action]")]
    // [Filter(nameof(OrderController))]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="orderService"></param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop([FromQuery] GetOrderInfoListForShopRequest request)
        {
            return await _orderService.GetOrderInfoListForShop(request);
        }

        /// <summary>
        /// 得到订单基本信息集合
        /// </summary>
        [HttpPost]
        public async Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo([FromBody] ApiRequest<GetOrderBaseInfoRequest> request)
        {
            return await _orderService.GetOrderBaseInfo(request.Data);
        }

        /// <summary>
        /// 得到订单产品信息集合
        /// </summary>
        [HttpPost]
        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct([FromBody] ApiRequest<GetOrderProductRequest> request)
        {
            return await _orderService.GetOrderProduct(request.Data);
        }

        /// <summary>
        /// 得到订单的预约信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopReserveDTO>> GetReserverInfo([FromQuery] GetReserverInfoRequest request)
        {
            return await _orderService.GetReserverInfo(request);
        }


        /// <summary>
        /// 得到订单的保险车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany([FromQuery] GetOrderInsuranceCompanyRequest request)
        {
            return await _orderService.GetOrderInsuranceCompany(request);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<OrderDispatchDTO>> GetDispatchInfo([FromQuery] GetReserverInfoRequest request)
        {
            return await _orderService.GetDispatchInfo(request);
        }

        [HttpPost]
        public async Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch([FromBody] ApiRequest<GetOrderDispatchRequest> request)
        {
            return await _orderService.GetOrderDispatch(request.Data);
        }

        [HttpPost]
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo([FromBody] ApiRequest<GetOrderCarsRequest> request)
        {
            return await _orderService.GetOrderCarsInfo(request.Data);
        }

        [HttpGet]
        public async Task<ApiResult<List<GetOrderStaticReportResponse>>> GetOrderStaticReportForShop()
        {
            return await _orderService.GetOrderStaticReportForShop();

        }

        [HttpGet]
        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReportForShop(
           [FromQuery] GetOrderDetailStaticReportApiRequest request)
        {
            return await _orderService.GetOrderDetailStaticReportForShop(request);

        }


        [HttpPost]
        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReportForShop(
            [FromBody] ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            return await _orderService.GetOrderOutProductsProfitReportForShop(request);

        }

        [HttpPost]
        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReportForShop([FromBody] ApiRequest<GetOrderProductsRequest> request)
        {
            return await _orderService.GetOrderProductsReportForShop(request);


        }

        /// <summary>
        /// 下载订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult DownloadOrders([FromQuery] GetOrderOutProductsProfitRequestForExcel request)
        {
            //var currentUseId = User.FindFirst("sub").Value;
            var currentUseId = "test";

            var rootPath = @"C:\OrderProduct\";
            if (System.IO.Directory.Exists(rootPath) == false)
                System.IO.Directory.CreateDirectory(rootPath);

            var newFile = rootPath + currentUseId + ".xls";
            if (System.IO.File.Exists(newFile))
            {
                System.IO.File.Delete(newFile);
            }

            var newRequest = new ApiRequest<GetOrderOutProductsProfitRequest>
            {
                Data = new GetOrderOutProductsProfitRequest
                {
                    PageIndex = 1,
                    PageSize = 1000,
                    EndCreateTime = request.EndCreateTime,
                    StartCreateTime = request.StartCreateTime,
                    OrderNo = request.OrderNo
                }
            };

            var task1 = _orderService.GetOrderOutProductsProfitReportForShop(newRequest);
            Task[] tasks = new Task[1] { task1 };

            Task.WaitAll(tasks);

            var outProductRes = task1.Result;
            if (outProductRes.Code == ResultCode.Success
                && outProductRes?.Data?.Items != null &&
                outProductRes?.Data?.Items.Count > 0)
            {

                using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();

                    var sheet = workbook.CreateSheet("orders");

                    var header = sheet.CreateRow(0);
                    header.CreateCell(0).SetCellValue("销售单号");
                    header.CreateCell(1).SetCellValue("产品编号");
                    header.CreateCell(2).SetCellValue("产品名称");

                    header.CreateCell(3).SetCellValue("原厂编号");
                    header.CreateCell(4).SetCellValue("产品类目");
                    header.CreateCell(5).SetCellValue("供应商简称");
                    header.CreateCell(6).SetCellValue("采购单号");
                    header.CreateCell(7).SetCellValue("车牌号");

                    header.CreateCell(8).SetCellValue("车型信息");
                    header.CreateCell(9).SetCellValue("采购单价");

                    header.CreateCell(10).SetCellValue("销售定价");
                    header.CreateCell(11).SetCellValue("产品单价");

                    header.CreateCell(12).SetCellValue("销售数量");
                    header.CreateCell(13).SetCellValue("采购金额");

                    header.CreateCell(14).SetCellValue("实收金额");
                    header.CreateCell(15).SetCellValue("创建时间");

                    var i = 1;
                    foreach (var item in outProductRes?.Data?.Items)
                    {
                        var datarow = sheet.CreateRow(i);
                        datarow.CreateCell(0).SetCellValue(item.SaleOrder);
                        datarow.CreateCell(1).SetCellValue(item.ProductId);
                        datarow.CreateCell(2).SetCellValue(item.ProductName);
                        datarow.CreateCell(3).SetCellValue(item.OeNumber);
                        datarow.CreateCell(4).SetCellValue(item.CategoryCode);
                        datarow.CreateCell(5).SetCellValue(item.VenderShortName);
                        datarow.CreateCell(6).SetCellValue(item.PurchaseOrder);
                        datarow.CreateCell(7).SetCellValue(item.CarNumber);
                        datarow.CreateCell(8).SetCellValue(item.CarInfo);
                        datarow.CreateCell(9).SetCellValue(item.PurchasePrice.ToString());
                        datarow.CreateCell(10).SetCellValue(item.SalePrice.ToString());
                        datarow.CreateCell(11).SetCellValue(item.SaleOrderPrice.ToString());
                        

                        datarow.CreateCell(12).SetCellValue(item.TotalNumber);
                        datarow.CreateCell(13).SetCellValue(item.PurchaseTotalPrice.ToString());
                        datarow.CreateCell(14).SetCellValue(item.ActualAmount.ToString());
                        datarow.CreateCell(15).SetCellValue(item.CreateTime);
                        
                        i++;
                    }
                    workbook.Write(fs);
                }
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(newFile, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            // return File(memory, "application/vnd.ms-excel", "order.xlsx");
            return File(memory, "application/vnd.android.package-archive", "OrderProduct.xlsx");

        }
    }
}