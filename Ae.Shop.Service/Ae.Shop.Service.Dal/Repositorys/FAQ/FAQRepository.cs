using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public class FAQRepository : AbstractRepository<FaqDO>, IFAQRepository
    {
        public FAQRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 问答列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<FaqInfoDO>> GetFAQListAsync(GetFAQListRequest request)
        {
            //查询数量
            string sqlCount = @"SELECT COUNT(f.id) from faq f
		where f.is_deleted=0 ";
            //查询列表
            string sql = @"	SELECT f.id,f.channel_id ChannelId,cn.name ChannelName,one.name CategoryOneName,two.name CategoryTwoName,three.name CategoryThreeName,f.`question`, f.`answer`,f.create_time CreateTime,f.create_by CreateBy,f.update_by UpdateBy,f.update_time UpdateTime from faq f 
		left join faq_channel cn on f.channel_id = cn.id
        left join faq_category one on one.id = f.category_one_id
        left join faq_category two on two.id = f.category_two_id
		left join faq_category three on f.category_three_id = three.id
		where f.is_deleted=0 ";

            var para = new DynamicParameters();
            string join = string.Empty;
            StringBuilder condition = new StringBuilder();
            if (request.ChannelId > 0) 
            {
                para.Add("@ChannelId", request.ChannelId);
                condition.Append(" AND f.channel_id = @ChannelId");
            }
            if (request.OneCategoryId > 0)
            {
                para.Add("@OneCategoryId", request.OneCategoryId);
                condition.Append(" AND f.category_one_id = @OneCategoryId");
            }
            if (request.TwoCategoryId > 0)
            {
                para.Add("@TwoCategoryId", request.TwoCategoryId);
                condition.Append(" AND f.category_two_id =  @TwoCategoryId");
            }
            if (request.ThreeCategoryId > 0)
            {
                para.Add("@ThreeCategoryId", request.ThreeCategoryId);
                condition.Append(" AND f.category_three_id = @ThreeCategoryId");
            }
            if (!string.IsNullOrEmpty(request.Question)) 
            {
                para.Add("@Question", request.Question);
                condition.Append(" AND f.question LIKE CONCAT('%',@Question,'%')");
            }
            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });
            IEnumerable<FaqInfoDO> ShopList = new List<FaqInfoDO>();

            if (count > 0)
            {
                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                condition.Append(" ORDER BY  f.id desc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<FaqInfoDO>(sql, para);
                });
            }
            ApiPagedResultData<FaqInfoDO> result = new ApiPagedResultData<FaqInfoDO>()
            {
                TotalItems = count,
                Items = ShopList.ToList()
            };

            return result;
        }

        /// <summary>
        /// 查询FAQ渠道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<FaqChannelDO>> GetFaqChannelListAsync() 
        {
            string sql = @"SELECT id,name from faq_channel where is_deleted=0 ";
            IEnumerable<FaqChannelDO> faqChannelList = new List<FaqChannelDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                faqChannelList = await conn.QueryAsync<FaqChannelDO>(sql);
            });
            return faqChannelList.ToList();
        }

        /// <summary>
        /// 查询FAQ分类列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<FaqCategoryDO>> GetFaqCategoryListAsync(long categoryId)
        {
            string sql = @"SELECT id,name,level from faq_category where is_deleted=0 ";

            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            if (categoryId > 0)
            {
                para.Add("@CategoryId", categoryId);
                condition.Append(" AND parent_id = @CategoryId");
            }
            else 
            {
                condition.Append(" AND level = 1");
            }
            sql = sql + condition.ToString();
            IEnumerable<FaqCategoryDO> faqCategoryList = new List<FaqCategoryDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                faqCategoryList = await conn.QueryAsync<FaqCategoryDO>(sql, para);
            });
            return faqCategoryList.ToList();
        }

    }
}
