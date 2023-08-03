using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    public class ArrivalMaintenanceProjectVo
    {
        public string ArrivalDate { get; set; }

        public string ServiceTypeText { get; set; }

        public string ShopName { get; set; }

        public double Kilometre { get; set; }

        public  int Id { get; set; }

        public List<ArrivalMaintenanceProjectItem> Items { get; set; }


    }


    public class ArrivalMaintenanceProjectItem
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public  string ProductName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; }

        /// <summary>
        ///价格
        /// </summary>
        public decimal Price { get; set; }
    }
}
