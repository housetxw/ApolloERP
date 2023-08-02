using FluentValidation;
using Ae.Product.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Product.Service.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class AddBrandRequestValidator: AbstractValidator<AddBrandRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public AddBrandRequestValidator()
        {
            RuleFor(n => n.BrandName).NotEmpty().WithMessage("品牌名不能为空");
        }
    }
}
