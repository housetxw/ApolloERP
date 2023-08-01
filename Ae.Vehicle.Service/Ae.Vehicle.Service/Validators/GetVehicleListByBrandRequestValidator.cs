using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ae.Vehicle.Service.Core.Request;

namespace Ae.Vehicle.Service.Validators
{
    /// <summary>
    /// 参数校验
    /// </summary>
    public class GetVehicleListByBrandRequestValidator : AbstractValidator<GetVehicleListByBrandRequest>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetVehicleListByBrandRequestValidator()
        {
            RuleFor(n => n.Brand).NotEmpty().WithMessage("品牌不能为空");
        }
    }
}
