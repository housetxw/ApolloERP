using Ae.BaoYang.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class BrakeFluidModel
    {
        public decimal Volume { get; set; }

        public int Level { get; set; }

        public bool IsValid()
        {
            bool result = this.Volume > 0 && this.Level > 0;

            return result;
        }

        public BrakeFluidModel(BaoYangAccessoryModel accessory)
        {
            if (accessory != null)
            {
                decimal volume = 0;
                if (decimal.TryParse(accessory.Volume, out volume))
                {
                    Volume = volume;
                }

                if (!string.IsNullOrEmpty(accessory.Level))
                {
                    accessory.Level = accessory.Level.Replace("DOT-", string.Empty);
                    int level = 0;

                    if (Int32.TryParse(accessory.Level, out level))
                    {
                        Level = level;
                    }
                }
            }
        }
    }
}
