using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangAntifreezeSettingRepository
    {
        Task<IEnumerable<BaoYangAntifreezeSettingDO>> GetBaoYangAntifreezeSettingAsync();
    }
}
