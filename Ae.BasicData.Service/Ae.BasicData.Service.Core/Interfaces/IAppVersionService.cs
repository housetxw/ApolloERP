using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;

namespace Ae.BasicData.Service.Core.Interfaces
{
    public interface IAppVersionService
    {
        Task<AppVersionEntityResDTO> GetAppVersionInfo(AppVersionEntityReqDTO req);

        Task<List<AppVersionListResDTO>> GetAppVersionHistory();

    }
}
