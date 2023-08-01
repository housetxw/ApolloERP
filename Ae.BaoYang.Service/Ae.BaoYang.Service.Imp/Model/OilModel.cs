using Ae.BaoYang.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class OilModel
    {
        /// <summary>
        /// 机油升数
        /// </summary>
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 机油粘度
        /// </summary>
        public string Viscosity { get; set; }

        /// <summary>
        /// 机油等级
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 规格
        /// </summary>

        public string Description { get; set; }

        public OilModel(BaoYangAccessoryModel accessory)
        {
            if (accessory != null)
            {
                decimal volume = 0;
                if (decimal.TryParse(accessory.Volume, out volume))
                {
                    Volume = volume;
                }

                Viscosity = accessory.Viscosity;
                Description = accessory.Description;
                if (!string.IsNullOrEmpty(accessory.Level))
                {
                    Level = accessory.Level;
                }
                else
                {
                    Level = "HX5";
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool result = this.Volume > 0 && !string.IsNullOrEmpty(Viscosity) && !string.IsNullOrEmpty(Level) &&
                          !string.IsNullOrEmpty(Description);

            return result;
        }
    }
}
