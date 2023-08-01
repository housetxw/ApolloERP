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
    public class PaiLiangByVehicleIdRequestValidator: AbstractValidator<GetPaiLiangByVehicleIdRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public PaiLiangByVehicleIdRequestValidator()
        {
            RuleFor(n => n.VehicleId).NotEmpty().WithMessage("车系不能为空");
        }
    }
}
