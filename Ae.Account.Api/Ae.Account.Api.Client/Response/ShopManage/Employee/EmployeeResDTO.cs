using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Account.Api.Client.Response
{
    public class EmployeeResDTO
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType Type { get; set; }

        public int OrganizationId { get; set; }

        public int DepartmentId { get; set; }

        public int JobId { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum EmployeeType
    {
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2,

        [Description("拓展")]
        Extend = 3,
        [Description("供应商")]
        Supplier = 4,


        [Description("所有类型")]
        All = 9999
    }

    public enum DimissionType
    {
        None = -1,

        [Description("自动离职")]
        VoluntaryTurnover = 0,

        [Description("辞退")]
        Dismiss = 1
    }

}
