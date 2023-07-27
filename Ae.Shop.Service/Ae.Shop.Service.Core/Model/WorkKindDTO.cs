using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class WorkKindDTO { }

    public class WorkKindEntityDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class WorkKindListDTO : WorkKindEntityDTO
    {

    }

}
