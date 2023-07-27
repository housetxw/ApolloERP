using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;

namespace Ae.BasicData.Service.Dal.Repositorys.IDAL
{
    public interface IAppVersionRepository
    {
        Task<List<AppVersionDO>> GetAppVersionHistory();

        Task<List<AppVersionDO>> GetAppVersionListByCode(AppVersionEntityReqDTO req);

        Task<AppVersionGrayDO> GetAppVersionGrayInfoByCodeAndId(AppVersionEntityReqDTO req);


    }
}
