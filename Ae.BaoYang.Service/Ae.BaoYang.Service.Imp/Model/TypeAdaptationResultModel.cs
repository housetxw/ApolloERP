using Ae.BaoYang.Service.Imp.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class TypeAdaptationResultModel<T>
    {
        public T Data { get; set; }

        public InAdaptableType Reason { get; set; } = InAdaptableType.None;

        public string Tips { get; set; }
    }
}
