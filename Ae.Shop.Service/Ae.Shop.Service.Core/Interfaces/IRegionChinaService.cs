using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IRegionChinaService
    {
        Task<List<RegionChinaVO>> GetRegionChinaListByRegionId(GetRegionChinaListByRegionIdRequest request);
    }
}
