using Ae.BaoYang.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class TransFluidModel
    {
        public TransFluidModel(BaoYangAccessoryModel accessory)
        {
            if (accessory != null)
            {
                decimal volume = 0;
                if (decimal.TryParse(accessory.Volume, out volume))
                {
                    Volume = volume;
                }
            }
        }

        public decimal Volume { get; set; }

        public bool IsValid()
        {
            bool result = this.Volume > 0;

            return result;
        }
    }
}
