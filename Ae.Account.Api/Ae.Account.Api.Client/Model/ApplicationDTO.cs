
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Client.Model
{
    public class ApplicationDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Initialism { get; set; }

        public string Route { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }
    }

}
