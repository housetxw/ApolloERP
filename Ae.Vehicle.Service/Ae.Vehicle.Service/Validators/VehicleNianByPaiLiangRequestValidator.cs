using FluentValidation;
using Ae.Vehicle.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class VehicleNianByPaiLiangRequestValidator: AbstractValidator<GetVehicleNianByPaiLiangRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public VehicleNianByPaiLiangRequestValidator()
        {
            RuleFor(n => n.VehicleId).NotEmpty().WithMessage("车系不能为空");
            RuleFor(n => n.PaiLiang).NotEmpty().WithMessage("排量不能为空");
        }
    }
}
