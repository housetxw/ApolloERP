using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;
using Dapper;
using Ae.Shop.Service.Core.Response;

namespace Ae.Shop.Service.Dal.Repositorys.Ad
{
    public class NoticeRepository : AbstractRepository<NoticeDO>, INoticeRepository
    {
        public NoticeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 获取最新公告
        /// </summary>
        /// <returns></returns>
        public async Task<NoticeDO> GetTopNewNoticeAsync()
        {
            //查询最新公告
            string sql  = @"select id,title from notice 
            where is_deleted = 0 AND is_display = 1 ORDER BY id desc LIMIT 1";

            NoticeDO noticeInfoDO = new NoticeDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                noticeInfoDO = await conn.QueryFirstAsync<NoticeDO>(sql);
            });
            return noticeInfoDO;
        }


        /// <summary>
        /// 获取已读公告
        /// </summary>
        /// <returns></returns>
        public async Task<List<NoticeInfoDO>> GetNoticeListByShopAsync(GetNoticeListRequest request)
        {
            string sql = @"SELECT
	n.id,
	n.title,
	r.id ShopId 
FROM
	notice n
	LEFT JOIN notice_reader r ON n.id = r.notice_id 
WHERE
	is_deleted = 0 
	AND is_display = 1
    AND r.shop_id = @ShopId
	AND r.user_id = @UserId";
            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);
            para.Add("@UserId", request.UserId);
            IEnumerable<NoticeInfoDO> noticeList = new List<NoticeInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                noticeList = await conn.QueryAsync<NoticeInfoDO>(sql, para);
            });
            return noticeList.ToList();
        }
        /// <summary>
        /// 根据门店ID，UserId添加公告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddUserNoticeAsync(GetNoticeInfoRequest request)
        {
            //查询最新公告
            string sql = @"SELECT
	n.id,
	n.title,
	r.shop_id ShopId 
FROM
	notice n
	LEFT JOIN notice_reader r ON n.id = r.notice_id 
WHERE
	is_deleted = 0 
	AND is_display = 1
    AND r.shop_id = @ShopId
	AND r.user_id = @UserId";
            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);
            para.Add("@UserId", request.UserId);
            para.Add("@CreateBy", request.CreateBy);
            NoticeDO noticeInfoDO = new NoticeDO();
            await OpenConnectionAsync(async conn =>
            {
                noticeInfoDO = await conn.QueryFirstAsync<NoticeDO>(sql, para);
            });
            var row = 0;
            if (noticeInfoDO == null)
            {
                string insertSql = @"INSERT notice_reader ( notice_id, shop_id, user_id, create_by, create_time )
                SELECT
                n.id,
                @ShopId,
                @UserId, 
                @CreateBy,
                now()
                FROM
	                notice";
                await OpenConnectionAsync(async conn =>
                {
                    row = await conn.ExecuteAsync(insertSql);
                });
            }
            else 
            {
                row = 1;
            }
            return row > 0;
        }

    }
}
