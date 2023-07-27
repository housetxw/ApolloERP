using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Login.Api.Common.Constant;

namespace Ae.B.Login.Api.Common.Extension
{
    public static class IntExtension
    {

        public static int RandomIntRange(int minVal = 1, int maxVal = 10)
        {
            var val = new Random().Next(minVal, maxVal);
            return val < CommonConstant.One ? CommonConstant.Three : val;
        }


    }
}
