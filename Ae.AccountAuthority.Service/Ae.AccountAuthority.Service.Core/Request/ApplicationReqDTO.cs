using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Base.Request;

namespace Ae.AccountAuthority.Service.Core.Request
{
    public class ApplicationReqDTO { }

    public class AppListReqDTO : BasePageRequest
    {
        public long Id { get; set; }

        //public string Name { get; set; }

        public string Initialism { get; set; }

        public int IsDeleted { get; set; }
    }

    public class AppReqDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Initialism { get; set; }

        public string Route { get; set; }
    }

    public class ApplicationDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Initialism { get; set; }

        public string Route { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }


}
