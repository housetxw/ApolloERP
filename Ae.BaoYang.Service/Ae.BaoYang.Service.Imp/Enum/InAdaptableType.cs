using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Enum
{
    /// <summary>
    /// 不适配的原因分类
    /// </summary>
    public enum InAdaptableType
    {
        // 没有原因
        None = 0,
        NoData = 1,
        NoProduct = 2,
        RegionRestrict = 3,
        VehicleRestrict = 4,
        NeedMoreVehicleInfo = 5,
        InValidData = 6,
        NoActivityProduct = 7,
        ProjectRely = 8,
        NoActivityProductButHasProduct = 9,
        NoYearCardProduct = 10
    }
}
