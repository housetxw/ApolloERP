using FluentValidation;
using Ae.BaoYang.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Validators
{
    /// <summary>
    /// 参数校验
    /// </summary>
    public class BaoYangPartAdaptationsRequestValidator : AbstractValidator<GetBaoYangPartAdaptationsRequest>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaoYangPartAdaptationsRequestValidator()
        {
            RuleFor(n => n.TidList).NotEmpty().WithMessage("Tid不能为空");
        }
    }
}
