using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model.ShopCustomer;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopCustomer;
using Ae.Shop.Service.Core.Response;

namespace Ae.Shop.Service.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShopCustomerService
    {
        /// <summary>
        /// 会员标签查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<MemberTagDto> GetMemberTag(MemberTagRequest request);

        /// <summary>
        /// 获取所有会员标签
        /// </summary>
        /// <returns></returns>
        Task<List<MemberTagDto>> GetMemberTagEnum();

        /// <summary>
        /// 用户关联门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddShopUserRelation(AddShopUserRelationRequest request);

        /// <summary>
        /// 编辑会员标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditMemberTag(EditMemberTagRequest request);

        /// <summary>
        /// 更新用户最近一次到店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateUserLastReceiveTime(UpdateUserLastReceiveTimeRequest request);

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopCustomerVo>> GetCustomerList(CustomerListRequest request);

        /// <summary>
        /// 门店客户列表（Shop站点）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopCustomerListVo>> GetShopCustomerList(ShopCustomerListRequest request);

        /// <summary>
        /// 门店客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopCustomerDetailVo> GetShopCustomerDetail(ShopCustomerDetailRequest request);

        Task<ShopReferrerCustomerResDTO> GetCurMonthShopNewCustomerStatisticsInfo(ShopReferrerCustomerReqDTO req);

        Task<ApiPagedResultData<DrainageStatisticsResDTO>> GetDrainageStatisticsPage(DrainageStatisticsPageReqDTO req);


        /// <summary>
        /// 平台汽配下单调用
        /// 绑定客户与店的关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddShopUserRelationForQp(AddShopUserRelationForQpRequest request);

        /// <summary>
        /// 平台汽配 - 我的顾客
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopCustomerQpVo>> GetCustomerListForQp(CustomerListForQpRequest request);
    }
}
