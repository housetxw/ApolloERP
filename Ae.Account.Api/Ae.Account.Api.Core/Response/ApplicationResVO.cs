using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Core.Response
{
    public class ApplicationResVO { }

    public class AppListResVO
    {
        public List<AppResVO> AppList { get; set; }

        public int TotalItems { get; set; }
    }

    public class AppResVO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Initialism { get; set; }

        public string Route { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }
        public string CreateId { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }
        public string UpdateId { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public class AppSelectResVO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Initialism { get; set; }

        public bool IsDeleted { get; set; }
    }

}
