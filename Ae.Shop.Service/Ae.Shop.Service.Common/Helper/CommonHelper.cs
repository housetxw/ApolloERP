using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Common.Helper
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

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescriptionByEnum(Enum enumValue)
        {
            string value = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(value);
            if (field != null)
            {
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs.Length == 0)    //当描述属性没有时，直接返回名称
                    return value;
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                return descriptionAttribute.Description;
            }
            return string.Empty;

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
        public static string GetCarVehicle(string vehicle, string paiLiang, string nian, string salesName)
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
                carVehicle = carVehicle.Substring(0, carVehicle.Length - 1);
            }
            return carVehicle;
        }
    }
}
