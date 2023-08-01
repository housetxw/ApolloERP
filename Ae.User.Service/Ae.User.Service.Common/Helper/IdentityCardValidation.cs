using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Common.Helper
{
    /**  
    * 身份证15位编码规则：dddddd yymmdd xx p
    * dddddd：地区码
    * yymmdd: 出生年月日
    * xx: 顺序类编码，无法确定
    * p: 性别，奇数为男，偶数为女
    * <p />
    * 身份证18位编码规则：dddddd yyyymmdd xxx y
    * dddddd：地区码
    * yyyymmdd: 出生年月日
    * xxx:顺序类编码，无法确定，奇数为男，偶数为女
    * y: 校验码，该位数值可通过前17位计算获得
    * <p />
    * 18位号码加权因子为(从右到左) Wi = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2,1 ]
    * 验证位 Y = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ]
    * 校验位计算公式：Y_P = mod( ∑(Ai×Wi),11 )
    * i为身份证号码从右往左数的 2...18 位; Y_P为脚丫校验码所在校验码数组位置
    **/
    public static class IdentityCardValidation
    {
        /// <summary> 
        /// 验证身份证合理性 
        /// </summary> 
        /// <param name="idNumber">身份证号</param> 
        /// <returns></returns> 
        public static bool CheckIdCard(string idNumber)
        {
            if (string.IsNullOrWhiteSpace(idNumber))
            {
                return false;
            }

            if (idNumber.Length == 18)
            {
                var check = CheckIdCard18(idNumber);
                return check;
            }

            if (idNumber.Length != 15) return false;
            {
                var check = CheckIdCard15(idNumber);
                return check;
            }
        }

        /// <summary> 
        /// 18位身份证号码验证 
        /// </summary> 
        private static bool CheckIdCard18(string idNumber)
        {
            if (long.TryParse(idNumber.Remove(17), out var n) == false
                || n < Math.Pow(10, 16)
                || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false; //数字验证 
            }

            //省份编号
            const string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false; //省份验证 
            }

            var birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out _) == false)
            {
                return false; //生日验证 
            }

            string[] arrArrifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                // 加权求和 
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }

            //得到验证码所在位置
            Math.DivRem(sum, 11, out var y);
            var x = idNumber.Substring(17, 1).ToLower();
            var yy = arrArrifyCode[y];
            if (yy != x)
            {
                return false; //校验码验证 
            }

            return true; //符合GB11643-1999标准 
        }

        /// <summary> 
        /// 15位身份证号码验证 
        /// </summary> 
        private static bool CheckIdCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false; //数字验证 
            }

            string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false; //省份验证 
            }

            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证 
            }

            return true;
        }

        /// <summary>
        /// 中文姓名校验
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool CheckIdentityName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(userName,
                @"^[\u4E00-\u9FA5·]{2,20}$"))
            {
                return false;
            }

            return true;
        }
    }
}
