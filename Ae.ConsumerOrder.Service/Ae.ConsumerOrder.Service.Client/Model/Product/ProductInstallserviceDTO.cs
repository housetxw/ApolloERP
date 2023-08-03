namespace Ae.ConsumerOrder.Service.Client.Model
{
    public class ProductInstallserviceDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 服务id
        /// </summary>
        public string ServiceId { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务价格
        /// </summary>
        public decimal ServicePrice { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;
    }
}
