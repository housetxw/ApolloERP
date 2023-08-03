using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NLog.Time;

namespace Ae.FMS.Service.Imp.Helpers
{
    public static class PreCalculationReconciliationFilterHelper
    {
        /// <summary>
        /// 对账得前置校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T PreFilter<T>(this T source, Func<T, T> func)
        {
            return func(source);
        }

        /// <summary>
        /// 门店对账规则校验
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TSource ShopReconciliationFilter<TSource>(this TSource source, Func<TSource, TSource> func)
        {
            return func(source);
        }

        /// <summary>
        /// 公司对账规则校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T CompanyReconciliationFilter<T>(this T source, Func<T, T> func)
        {
            return func(source);
        }

        /// <summary>
        /// 自动完成对账Filter
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TSource AutoReconciliationFilter<TSource>(this TSource source, Func<TSource, TSource> func)
        {
            return func(source);
        }

        public static ReconciliationRuleEnum GetReconciliationRule<TSource>(this TSource source,Func<TSource,ReconciliationRuleEnum> func)
        {
            return func(source);
        }
    
    }

    public enum ReconciliationRuleEnum
    {
        [Description("普通")]
        OrdinaryOrder=1,
        [Description("客户向小仓下单")]
        CustomerToSmallWarehouseOrder=2,
        [Description("门店向小仓下单")]
        ShopToSmallWarehouseOrder =3
    }
}
