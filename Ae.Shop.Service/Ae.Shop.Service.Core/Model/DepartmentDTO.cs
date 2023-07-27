using System;
using System.Collections.Generic;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Model
{
    public class DepartmentDTO
    {
        public long Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public int OrganizationId { get; set; }

        public OrganizationType Type { get; set; }

        public int IsDeleted { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }

    public class DepartmentSelDTO
    {
        public long Value { get; set; }
        public string Label { get; set; }
        public int Type { get; set; }
    }

    public class DepartmentTreeDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ParentId { get; set; }

        public List<DepartmentTreeDTO> Children { get; set; } = new List<DepartmentTreeDTO>();
    }

    public class ElementDepartmentTree
    {
        public long Id { get; set; }

        public string Label { get; set; }

        public List<ElementDepartmentTree> Children { get; set; } = new List<ElementDepartmentTree>();
    }

}
