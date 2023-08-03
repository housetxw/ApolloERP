using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    public enum StockOutTypeEnum
    {
        采购退货 = 1,
        订单安装,
        货损出库,
        其他出库,
        销售出库, //内销，外销
        盘亏出库,
        丢失出库,
        耗材领用,
        调拨出库 //-->门店->仓库的出库任务
    }

    public enum StockInTypeEnum
    {
        采购入库 = 1,
        其他入库,
        盘盈入库,
        出库作废, //->
        订单取消,
        铺货入库,
        调拨入库,
        领料退货,
        内销入库
    }

    #region  出入库枚举类型
    //public enum StockOutTypeSel
    //{
    //    采购退货 = 1,
    //    订单安装,
    //    货损,
    //    其他,
    //    盘亏出库,
    //    丢失,
    //    辅料消耗,
    //    调拨出库 //-->门店->仓库的出库任务
    //}

    //public enum StockInTypeSel
    //{
    //    采购入库 = 1,
    //    其他入库,
    //    盘盈入库,
    //    出库作废, //->订单取消后，库存重新回到门店中
    //    铺货入库,
    //    调拨入库
    //}
    #endregion



    public enum StockOperateTypeEnum
    {
        入库 = 1,
        出库
    }

    public enum StockInOutStatusEnum
    {
        新建 = 1,
        部分收货,
        已收货,
        已出库
    }

    public enum StorageStatusEnum
    {
        指定产品 = 1,
        全盘 = 2
    }

    public enum ProductTypeEnum
    {
        总部产品 = 1,
        其他产品
    }

    public enum StorageTypeEnum
    {
        新建 = 1,
        盘点中,
        差异处理中, //待确认
        盘点完成, //已确认
        无效  //->重盘
    }

    public enum DealTypeEnum
    {
        要求重盘此产品 = 1,
        差异确认通过 = 2
    }

    /// <summary>
    /// 货主类型
    /// </summary>
    public enum OwnerType
    {
        Shop = 1,
        Company,
        Warehouse,
        Supplier
    }

    /// <summary>
    /// 良品类型
    /// </summary>
    public enum ProductAttrTypeEnum
    {
        良品 = 1,
        不良品
    }

    public enum ExceptionFileTypeEnum
    {
        货损 = 1,
        多发,
        漏发
    }

    /// <summary>
    /// 货损产品状态枚举
    /// </summary>
    public enum ExceptionRecordStatusEnum
    {
        新建 = 1,
        已审核,
        已完成
    }

    public enum PackageStatusEnum
    {
        新建 = 1,
        已发出,
        已签收,
        已清点,
        未清点 //这个状态只是转换使用!
    }

    public enum SignType
    {
        订单 = 1,
        铺货
    }

    public enum ShopWmsLogEnum
    {
        /// <summary>
        /// 盘库
        /// </summary>
        Storage,

        /// <summary>
        /// 出入库
        /// </summary>
        StockInOut,

        /// <summary>
        /// 盘库差异
        /// </summary>
        StockDiff
    }
}
