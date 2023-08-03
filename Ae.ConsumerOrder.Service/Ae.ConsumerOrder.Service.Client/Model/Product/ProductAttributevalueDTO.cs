namespace Ae.ConsumerOrder.Service.Client.Model
{
    /// <summary>
    /// 商品属性
    /// </summary>
    public class ProductAttributevalueDTO
    {
        /// <summary>
        /// 从表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public string ProductId { get; set; }

        /// 属性名id
        /// </summary>
        public int AttributeNameId { get; set; }
        /// 属性名
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 商品属性值
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;
    }
}
