using ApolloErp.Common.BrandLogoHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;

namespace Ae.Receive.Service.Common.Helper
{
    public static class CommonHelper
    {
        public static string GetWeekDay(string name) 
        {
            switch (name)
            {
                case "Monday":
                    return "周一";
                case "Tuesday":
                    return "周二";
                case "Wednesday":
                    return "周三";
                case "Thursday":
                    return "周四";
                case "Friday":
                    return "周五";
                case "Saturday":
                    return "周六";
                case "Sunday":
                    return "周日";
                default:
                    return "";
            }
        }

        public static int GetWeekDayNum(string name)
        {
            switch (name)
            {
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                case "Sunday":
                    return 7;
                default:
                    return 1;
            }
        }

        public static sbyte GetOrderType(string serviceCode)
        {
            switch (serviceCode)
            {
                case "Tire":
                    return 1;
                case "BaoYang":
                    return 2;
                case "Wash":
                    return 3;
                case "SheetMetal":
                    return 4;
                case "CarModification":
                    return 5;
                case "Other":
                    return 6;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescriptionByEnum(Enum enumValue)
        {
            string value = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }

        /// <summary>
        /// 用“|” 拼接车型信息
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="vehicle"></param>
        /// <param name="nian"></param>
        /// <param name="paiLiang"></param>
        /// <param name="salesName"></param>
        /// <returns></returns>
        public static string GetCarVehicle(string vehicle, string paiLiang, string nian,  string salesName) 
        {
            string carVehicle = string.Empty;
            if (!string.IsNullOrEmpty(vehicle))
            {
                carVehicle += vehicle + "|";
            }
            
            if (!string.IsNullOrEmpty(paiLiang))
            {
                carVehicle += paiLiang + "|";
            }
            if (!string.IsNullOrEmpty(nian))
            {
                carVehicle += nian + "|";
            }
            if (!string.IsNullOrEmpty(salesName))
            {
                carVehicle += salesName;
            }
            if (carVehicle.EndsWith("|")) 
            {
                carVehicle = carVehicle.Substring(0,carVehicle.Length-1);
            }
            return carVehicle;
        }

        /// <summary>
        /// 获取Logo图片地址(汉字转化为拼音)
        /// </summary>
        /// <param name="brand">品牌名称</param>
        /// <returns>Url</returns>
        public static string GetLogoUrlByName(string brand)
        {
            if (brand == null || brand.Length < 4)
                return null;

            return "/Images/Logo/" + WebUtility.UrlEncode(brand.Substring(4).Trim().Pinyin().ToLower()) + ".png";
        }

        public static string FormatTel(string tel)
        {
            if (string.IsNullOrEmpty(tel))
            {
                return string.Empty;
            }

            int length = tel.Length;
            if (length < 4)
            {
                return tel.PadRight(11, '*');
            }
            else
            {
                return tel.Substring(0, 3) + "****" + tel.Substring(length - 4, 4);
            }
        }
    }
}
