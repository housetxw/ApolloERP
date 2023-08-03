using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Fms;

namespace Ae.ShopOrder.Service.Client.Clients.FMS
{
    public interface IFmsClient
    {

        Task<ApiResult> CalculationReconciliationFee(
            CalculationReconciliationFeeRequest request);
        Task<ApiResult<string>> DeleteAccountCheck(
            CalculationReconciliationFeeRequest request);
    }
}
