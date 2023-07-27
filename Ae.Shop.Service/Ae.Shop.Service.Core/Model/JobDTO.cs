using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Model
{
    public class JobEntityDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int OrganizationId { get; set; }

        public JobType Type { get; set; }

       
    }

    public class JobListDTO: JobEntityDTO
    {
        public bool HasChild { get; set; }
    }

}
