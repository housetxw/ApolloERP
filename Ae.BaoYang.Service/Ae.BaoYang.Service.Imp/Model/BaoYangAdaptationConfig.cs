using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class BaoYangAdaptationConfig
    {
        public List<ProductAdaptation> ProductAdaptations { get; set; }

        public List<SpecialProductAdaptation> SpecialProductAdaptations { get; set; }

        public ProductAdaptation this[string displayName]
        {
            get
            {
                ProductAdaptation value = null;
                foreach (var adaptation in ProductAdaptations)
                {
                    if (adaptation.DisplayName.Equals(displayName))
                    {
                        value = adaptation;
                    }
                }

                return value;
            }
        }

        public string GetPartNames(string displayName)
        {
            string partNames = string.Empty;
            foreach (var adaptation in ProductAdaptations)
            {
                if (adaptation.DisplayName.Equals(displayName))
                {
                    partNames = adaptation.PartNames;
                    break;
                }
            }

            return partNames;
        }

        public string GetSpecialPartNames(string displayName)
        {
            string partNames = string.Empty;

            foreach (var adaptation in SpecialProductAdaptations)
            {
                if (adaptation.DisplayName.Equals(displayName))
                {
                    partNames = adaptation.PartNames;
                    break;
                }
            }

            return partNames;
        }
    }

    public class ProductAdaptation
    {
        public string PartNames { get; set; }

        public string DisplayName { get; set; }

        public string Brands { get; set; }

        public string CategoryName { get; set; }

        public string Type { get; set; }
    }

    public class SpecialProductAdaptation
    {
        public string PartNames { get; set; }

        public string DisplayName { get; set; }

        public string Brands { get; set; }

        public string CategoryName { get; set; }
    }
}
