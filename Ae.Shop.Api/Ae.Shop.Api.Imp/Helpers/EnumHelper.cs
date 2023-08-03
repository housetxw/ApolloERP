using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Helpers
{
   public static class EnumHelper
    {
        public static async Task<List<BasicInfoDTO>> GetBasicInfoDTOs<T>()
        {
            var list = new List<BasicInfoDTO>();

            await Task.Run(() =>
            {
                foreach (var e in Enum.GetValues(typeof(T)))
                {
                    var m = new BasicInfoDTO();
                    object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                    m.Key = e.ToString();
                    m.Value = e.ToString();
                    list.Add(m);
                }
            });
            return list;
        }

    }
}
