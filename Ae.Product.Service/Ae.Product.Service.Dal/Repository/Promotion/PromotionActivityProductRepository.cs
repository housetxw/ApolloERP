using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public class PromotionActivityProductRepository : AbstractRepository<PromotionActivityProductDo>, IPromotionActivityProductRepository
    {
        public PromotionActivityProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 活动商品查询批量
        /// </summary>
        /// <param name="activityIds"></param>
        /// <returns></returns>
        public async Task<List<PromotionActivityProductDo>> GetPromotionProductByActivityIds(List<Guid> activityIds)
        {
            var para = new DynamicParameters();
            para.Add("@activityIds", activityIds);

            var result = await GetListAsync<PromotionActivityProductDo>("WHERE activity_id IN @activityIds", para);

            return result?.AsList() ?? new List<PromotionActivityProductDo>();
        }

        /// <summary>
        /// 活动商品明细
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public async Task<List<PromotionActivityProductDo>> GetPromotionProductByActivityId(string activityId)
        {
            var para = new DynamicParameters();
            para.Add("@activityId", activityId);

            var result = await GetListAsync<PromotionActivityProductDo>("WHERE activity_id = @activityId", para);

            return result?.AsList() ?? new List<PromotionActivityProductDo>();
        }

        /// <summary>
        /// 活动商品详情
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="pid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<PromotionActivityProductDo> GetPromotionProductDetail(string activityId, string pid, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@activityId", activityId);
            para.Add("@pid", pid);

            var result = await GetListAsync<PromotionActivityProductDo>("WHERE activity_id = @activityId AND product_code = @pid", para, !readOnly);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<List<PromotionActivityProductDo>> GetPromotionProductByPid(string pid)
        {
            var para = new DynamicParameters();
            para.Add("@pid", pid);

            var result = await GetListAsync<PromotionActivityProductDo>("WHERE product_code = @pid", para);

            return result?.ToList() ?? new List<PromotionActivityProductDo>();
        }
    }
}
