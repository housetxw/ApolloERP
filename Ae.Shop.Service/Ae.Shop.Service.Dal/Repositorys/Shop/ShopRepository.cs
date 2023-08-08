using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;
using ApolloErp.Data.DapperExtensions.Data;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Core.Model;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model.OpeningGuide;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopRepository : AbstractRepository<ShopDO>, IShopRepository
    {
        public ShopRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 新增门店信息
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public async Task<long> AddShopAsync(ShopDO shop)
        {
            int id = await InsertAsync(shop);
            return id;
        }

        /// <summary>
        /// 查询门店信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ShopDO> GetShopAsync(long id)
        {
            var shop = await GetAsync<ShopDO>(id);
            return shop;
        }
        /// <summary>
        /// 根据门店名称查询门店信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ShopDO> GetShopByNameAsync(string name, long id)
        {
            var para = new DynamicParameters();
            para.Add("@name", name);
            para.Add("@id", id);
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE is_deleted = 0");
            condition.Append(" AND simple_name = @name");
            if (id > 0)
            {
                condition.Append(" AND id != @id");
            }

            var shop = (await GetListAsync<ShopDO>(condition.ToString(), para)).FirstOrDefault();
            return shop;
        }

        /// <summary>
        /// 获取所有门店信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopDO>> GetAllShopAsync()
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE is_deleted = 0");
            var result = await GetListAsync<ShopDO>(condition.ToString());
            return result.ToList();
        }

        /// <summary>
        /// BOSS端查询门店列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopSimpleInfoModel>> GetShopListForBOSSAsync(GetShopListModel model)
        {
            string sqlCount = @"SELECT
COUNT(*) as cs
FROM
    shop s
    LEFT JOIN shop_config sc ON s.id = sc.shop_id";
            string sql = @"SELECT

    s.id,
	s.simple_name SimpleName,
	s.full_name FullName,
	s.shop_company_name ShopCompanyName,
	CONCAT(s.province,s.city,s.district,s.address) as Address,
	s.post,s.status Status,s.check_status CheckStatus,s.failed_examined_reason FailedExaminedReason,s.online Online,
	s.brand,
	s.contact,
	s.telephone,
	s.mobile,
    s.head Head,
    s.head_phone HeadPhone,
	s.type,
	s.examiner,
	s.submitor,
    s.owner_name OwnerName,
    s.owner_phone OwnerPhone,
	sc.lunbao_responsible_person as LunbaoResponsiblePerson,
	DATE_FORMAT(sc.suspend_end_date_time,'%Y-%m-%d') as SuspendEndDateTime,
	DATE_FORMAT(sc.suspend_start_date_time,'%Y-%m-%d') as SuspendStartDateTime,s.create_time as CreateTime,s.update_time as UpdateTime,s.system_type as SystemType
FROM
    shop s
    LEFT JOIN shop_config sc ON s.id = sc.shop_id";


            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE s.is_deleted = 0");
            if (!string.IsNullOrEmpty(model.SimpleName))
            {
                condition.Append(" AND s.simple_name LIKE CONCAT('%',@SimpleName,'%')");
                para.Add("@SimpleName", model.SimpleName);
            }
            if (!string.IsNullOrEmpty(model.FullName))
            {
                condition.Append(" AND s.full_name LIKE CONCAT('%',@FullName,'%')");
                para.Add("@FullName", model.FullName);
            }
            if (model.ParentId > 0)
            {
                condition.Append(" AND s.company_id = @ParentId");
                para.Add("@ParentId", model.ParentId);
            }
            if (!string.IsNullOrEmpty(model.OwnerPhone))
            {
                condition.Append(" AND (s.telephone = @OwnerPhone OR s.mobile =  @OwnerPhone OR s.owner_phone = @OwnerPhone OR s.head_phone = @OwnerPhone)");
                para.Add("@OwnerPhone", model.OwnerPhone);
            }
            if (!string.IsNullOrEmpty(model.ExternalPhone))
            {
                condition.Append(" AND (s.telephone = @ExternalPhone OR s.mobile = @ExternalPhone)");
                para.Add("@ExternalPhone", model.ExternalPhone);
            }

            #region  新加检索条件
            if (!string.IsNullOrEmpty(model.ProvinceId))
            {
                condition.Append(" AND s.province_id =@ProvinceId ");
                para.Add("@ProvinceId", model.ProvinceId);
            }

            if (!string.IsNullOrEmpty(model.CityId))
            {
                condition.Append(" AND s.city_id =@CityId ");
                para.Add("@CityId", model.CityId);
            }

            if (!string.IsNullOrEmpty(model.DistrictId))
            {
                condition.Append(" AND s.district_id =@DistrictId ");
                para.Add("@DistrictId", model.DistrictId);
            }
            if (!string.IsNullOrEmpty(model.Head))
            {
                condition.Append(" AND s.head = @Head");
                para.Add("@Head", model.Head);
            }

            if (model.OnLineStatus != 2)
            {
                condition.Append(" AND s.online =@OnLineStatus ");
                para.Add("@OnLineStatus", model.OnLineStatus);
            }

            if (model.CheckStatus != 4)
            {
                condition.Append(" AND s.check_status =@CheckStatus ");
                para.Add("@CheckStatus", model.CheckStatus);
            }

            if (model.ShopType > 0)
            {
                condition.Append(" AND s.type =@Type ");
                para.Add("@Type", model.ShopType);
            }

            if (model.SystemType >= 0)
            {
                condition.Append(" AND s.system_type =@SystemType ");
                para.Add("@SystemType", model.SystemType);
            }
            #endregion
            PagedEntity<ShopSimpleInfoModel> pagedEntity = new PagedEntity<ShopSimpleInfoModel>();
            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });

            if (count > 0)
            {
                condition.Append(" order by s.id DESC");
                var Offset = (model.PageIndex - 1) * model.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", model.PageSize);

                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();

                IEnumerable<ShopSimpleInfoModel> ShopList = new List<ShopSimpleInfoModel>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<ShopSimpleInfoModel>(sql, para);
                });
                pagedEntity.Items = ShopList.ToList();
            }
            pagedEntity.TotalItems = count;
            return pagedEntity;
        }


        /// <summary>
        /// 根据服务PID查询门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopSimpleInfoModel>> GetShopListForServiceAsync(GetShopListForServiceRequest request)
        {
            string sqlCount = @"SELECT COUNT(*) as cs ";
            string sql = @"SELECT	
  id,
	simple_name SimpleName,
	full_name FullName,
	province,
	city,
	district,
	address,
	contact,
	telephone,
	mobile,
  head Head,
  head_phone HeadPhone,
  owner_name OwnerName,
  owner_phone OwnerPhone ";

            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();

            //经纬度计算距离
            if (!string.IsNullOrWhiteSpace(request.Longitude) && !string.IsNullOrWhiteSpace(request.Latitude))
            {
                para.Add("@DistrictId", request.DistrictId);
                para.Add("@Longitude", request.Longitude);
                para.Add("@Latitude", request.Latitude);
                sql += @" ,CAST(SQRT(
    POW(111.2 * (latitude - @Latitude), 2) +
    POW(111.2 * (@Longitude - longitude) * COS(latitude / 57.3), 2)) AS DECIMAL(20,2)) AS distance ";
                condition.Append(@" FROM shop where is_deleted = 0 and district_id = @DistrictId ");
            }
            else
            {
                condition.Append(@" FROM shop where is_deleted = 0 ");
            }

            #region  新加检索条件
            if (request.Status > -1)
            {
                condition.Append(" AND status =@Status ");
                para.Add("@Status", request.Status);
            }

            if (request.ServicePId != null && request.ServicePId.Any())
            {
                condition.Append(@" AND id in (SELECT shop_id FROM(
SELECT  shop_id, count(*) as shopnum from shop_service where product_id in @ServicePId AND status = 1  and is_deleted=0
group by shop_id  HAVING shopnum = @ServiceCount) s)
AND check_status = 2 AND online = 1 ");
                para.Add("@ServicePId", request.ServicePId);
                para.Add("@ServiceCount", request.ServicePId.Count());
            }

            if (request.ShopType > 0)
            {
                condition.Append(" AND type =@Type ");
                para.Add("@Type", request.ShopType);
            }
            #endregion
            PagedEntity<ShopSimpleInfoModel> pagedEntity = new PagedEntity<ShopSimpleInfoModel>();
            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });

            if (count > 0)
            {

                //经纬度计算距离
                if (!string.IsNullOrWhiteSpace(request.Longitude) && !string.IsNullOrWhiteSpace(request.Latitude))
                {
                    condition.Append(@" ORDER BY distance asc");
                }
                else
                {
                    condition.Append(" order by id ASC");
                }
                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);

                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();

                IEnumerable<ShopSimpleInfoModel> ShopList = new List<ShopSimpleInfoModel>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<ShopSimpleInfoModel>(sql, para);
                });
                pagedEntity.Items = ShopList.ToList();
            }
            pagedEntity.TotalItems = count;
            return pagedEntity;
        }

        /// <summary>
        /// 微信小程序查询附近门店列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GetNearShopListDO> GetNearShopListAsync(GetNearShopListRequest request)
        {
            //查询数量
            string sqlCount = @"SELECT COUNT(*) ";
            //查询列表
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.status,
	CONCAT(s.province,s.city,s.district,s.address) as Address,
    s.Longitude,
    s.Latitude,
	s.telephone,
    img.img_url as Img,
    s.score Score,
    s.total_order as TotalOrder,
	sc.is_default_shop as IsDefaultShop,
    sc.start_work_time as StartWorkTime,
    sc.end_work_time as EndWorkTime,
    s.tag_name TagName,
    s.type,
    (case s.type when 2 THEN 100 when 8 THEN 90 when 1 THEN 80 ELSE 70 END) orderByItselftType";


            StringBuilder condition = new StringBuilder();
            //经纬度计算距离
            if (request.Longitude > 0 && request.Latitude > 0)
            {
                sql = sql + @" ,CAST(SQRT(
    POW(111.2 * (s.latitude - @Latitude), 2) +
    POW(111.2 * (@Longitude - s.longitude) * COS(s.latitude / 57.3), 2)) AS DECIMAL(20,2)) AS distance ";
            }
            //20210127 过滤掉平台先生
            condition.Append(@" FROM  ( SELECT * FROM shop WHERE is_deleted = 0 AND status != 1 AND check_status = 2 AND online = 1 and nature<>1 and type<>32 ");
            if (request.Type == 4)
            {
                condition.Append(" AND type = 4");
            }
            else
            {
                condition.Append(" AND type IN (1,2,8,16)");
            }
            if (request.CityId > 0)
            {
                condition.Append(" AND city_id = @CityId");
            }
            //if (request.DistrictId > 0)
            //{
            //    condition.Append(" AND district_id = @DistrictId");
            //}
            if (request.ShopIds != null && request.ShopIds.Any())
            {
                condition.Append(" AND id in @ShopIds");
            }
            condition.Append(@" ) s  LEFT JOIN shop_config sc ON s.id = sc.shop_id 
                      LEFT JOIN shop_img img ON s.id = img.shop_id AND img.type = 1 and img.is_deleted = 0");

            var para = new DynamicParameters();

            para.Add("@Longitude", request.Longitude);
            para.Add("@Latitude", request.Latitude);
            para.Add("@ProvinceId", request.ProvinceId);
            para.Add("@CityId", request.CityId);
            para.Add("@DistrictId", request.DistrictId);
            para.Add("@ShopIds", request.ShopIds);

            //总数量
            var count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });
            GetNearShopListDO result = new GetNearShopListDO();
            IEnumerable<NearShopInfoDO> ShopList = new List<NearShopInfoDO>();

            if (count > 0)
            {
                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                // condition.Append(" ORDER BY IsDefaultShop desc");
                condition.Append(" ORDER BY orderByItselftType desc");
                //按距离排序
                if (request.Longitude > 0 && request.Latitude > 0)
                {
                    condition.Append(" ,distance asc");
                }
                condition.Append(" ,status asc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<NearShopInfoDO>(sql, para);
                });
            }
            result.List = ShopList?.ToList() ?? new List<NearShopInfoDO>();
            result.TotalItems = count;
            return result;
        }


        /// <summary>
        /// 获取门店详情--MiniApp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<NearShopInfoDO> GetNearShoDetailAsync(GetShopDetailRequest request)
        {
            //查询详情信息
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.status,
    s.province,
    s.city,
    s.district,
	s.address,
    s.Longitude,
    s.Latitude,
	s.telephone,
sc.start_work_time as StartWorkTime,
sc.end_work_time as EndWorkTime,
s.score Score,
s.total_order as TotalOrder,
s.tag_name TagName,
s.description Description,s.type Type
	";

            StringBuilder condition = new StringBuilder();
            //经纬度计算距离
            if (request.Longitude > 0 && request.Latitude > 0)
            {
                condition.Append(@" ,CAST(SQRT(
    POW(111.2 * (s.latitude - @Latitude), 2) +
    POW(111.2 * (@Longitude - s.longitude) * COS(s.latitude / 57.3), 2)) AS DECIMAL(20,2)) AS distance ");
            }
            condition.Append(" FROM shop s LEFT JOIN shop_config sc ON s.id = sc.shop_id ");
            condition.Append(" WHERE s.is_deleted = 0 AND s.status != 1 AND s.online = 1 AND s.id = @ShopId ");
            if (request.CheckStatus > 0)
            {
                condition.Append("AND s.check_status = @CheckStatus ");
            }

            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);
            para.Add("@Longitude", request.Longitude);
            para.Add("@Latitude", request.Latitude);
            para.Add("@CheckStatus", request.CheckStatus);

            IEnumerable<NearShopInfoDO> ShopList = new List<NearShopInfoDO>();
            try
            {
                sql = sql + condition.ToString();
                await OpenSlaveConnectionAsync(async conn =>
                {

                    ShopList = await conn.QueryAsync<NearShopInfoDO>(sql, para);
                });
            }
            catch (Exception ex)
            {

                throw;
            }

            return ShopList?.FirstOrDefault();
        }

        /// <summary>
        /// 获取最近的门店ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopDO> GetOptimalShopId(GetOptimalShopRequest request)
        {
            var para = new DynamicParameters();
            para.Add("@Longitude", request.Longitude);
            para.Add("@Latitude", request.Latitude);
            para.Add("@CityId", request.CityId);

            string sql = @"SELECT s.id,s.simple_name as SimpleName ";
            StringBuilder condition = new StringBuilder();
            //经纬度计算距离
            if (request.Longitude > 0 && request.Latitude > 0)
            {
                condition.Append(@" ,CAST(SQRT(
    POW(111.2 * (s.latitude - @Latitude), 2) +
    POW(111.2 * (@Longitude - s.longitude) * COS(s.latitude / 57.3), 2)) AS DECIMAL(20,2)) AS distance ");
                condition.Append(@" FROM shop s WHERE s.is_deleted = 0 AND s.status != 1 AND s.check_status = 2 AND s.online = 1 ");
                if (!string.IsNullOrEmpty(request.CityId))
                {
                    condition.Append(@" and s.city_id = @CityId ");
                }
                condition.Append(@" ORDER BY distance asc LIMIT 1");
            }
            else
            {
                if (!string.IsNullOrEmpty(request.CityId))
                {
                    condition.Append(@" FROM shop s WHERE s.is_deleted = 0 AND s.status != 1 AND s.check_status = 2 AND s.online = 1 and s.city_id = @CityId limit 1");
                }
                else
                {
                    condition.Append(@" FROM shop s WHERE s.is_deleted = 0 AND s.status != 1 AND s.check_status = 2 AND s.online = 1 limit 1");
                }
            }
            IEnumerable<ShopDO> ShopList = new List<ShopDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<ShopDO>(sql + condition.ToString(), para);
            });
            return ShopList?.FirstOrDefault();
        }



        /// <summary>
        /// 查询门店简单信息列表---分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopSimpleInfoDO>> GetShopListAsync(GetShopListRequest request)
        {
            string sqlCount = @"select Count(*) from shop s ";
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.province,
    s.city,
    s.district,
    s.province_id ProvinceId,
    s.city_id CityId,
    s.district_id DistrictId,
    s.company_id as CompanyId,
    s.address,
    s.type,
	s.contact,
	s.telephone,
	s.mobile
FROM
    shop s ";
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE s.is_deleted = 0 AND s.type > 0 ");
            var para = new DynamicParameters();

            if (!string.IsNullOrEmpty(request.SimpleName))
            {
                para.Add("@SimpleName", request.SimpleName);
                condition.Append(" AND s.simple_name like CONCAT('%',@SimpleName,'%') ");
            }
            if (!string.IsNullOrEmpty(request.ProvinceId))
            {
                para.Add("@ProvinceId", request.ProvinceId);
                condition.Append(" AND s.province_id = @ProvinceId ");
            }
            if (!string.IsNullOrEmpty(request.CityId))
            {
                para.Add("@CityId", request.CityId);
                condition.Append(" AND s.city_id = @CityId ");
            }
            if (!string.IsNullOrEmpty(request.DistrictId))
            {
                para.Add("@DistrictId", request.DistrictId);
                condition.Append(" AND s.district_id = @DistrictId ");
            }

            if (request.ShopIds != null && request.ShopIds.Any())
            {
                para.Add("@ShopIds", request.ShopIds);
                condition.Append(" AND s.id in @ShopIds ");
            }

            if (request.ShopTypes != null && request.ShopTypes.Any())
            {
                para.Add("@ShopTypes", request.ShopTypes);
                condition.Append(" AND s.type in @ShopTypes ");
            }
            if (request.CompanyId > 0)
            {
                para.Add("@CompanyId", request.CompanyId);
                condition.Append(" AND s.company_id = @CompanyId ");
            }

            ApiPagedResultData<ShopSimpleInfoDO> result = new ApiPagedResultData<ShopSimpleInfoDO>();
            int count = 0;
            sqlCount = sqlCount + condition.ToString();
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            });
            IEnumerable<ShopSimpleInfoDO> ShopList = new List<ShopSimpleInfoDO>();
            if (count > 0)
            {
                var Offset = (request.PageIndex - 1) * request.PageSize;
                para.Add("@Offset", Offset);
                para.Add("@PageSize", request.PageSize);
                condition.Append(" ORDER BY s.id desc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    ShopList = await conn.QueryAsync<ShopSimpleInfoDO>(sql, para);
                });

            }
            result.Items = ShopList.ToList();
            result.TotalItems = count;
            return result;
        }

        public async Task<List<ShopSimpleInfoDO>> GetShopWareHouseListAsync(GetShopListRequest request)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.province,
    s.city,
    s.district,
    s.province_id ProvinceId,
    s.city_id CityId,
    s.district_id DistrictId,
    s.company_id as CompanyId,
    s.address,
    s.type,
	s.contact,
	s.telephone,
	s.mobile
FROM
    shop s ";
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE s.is_deleted = 0 AND s.type < 0 ");
            var para = new DynamicParameters();

            if (!string.IsNullOrEmpty(request.SimpleName))
            {
                para.Add("@SimpleName", request.SimpleName);
                condition.Append(" AND s.simple_name like CONCAT('%',@SimpleName,'%') ");
            }
            if (!string.IsNullOrEmpty(request.ProvinceId))
            {
                para.Add("@ProvinceId", request.ProvinceId);
                condition.Append(" AND s.province_id = @ProvinceId ");
            }
            if (!string.IsNullOrEmpty(request.CityId))
            {
                para.Add("@CityId", request.CityId);
                condition.Append(" AND s.city_id = @CityId ");
            }
            if (!string.IsNullOrEmpty(request.DistrictId))
            {
                para.Add("@DistrictId", request.DistrictId);
                condition.Append(" AND s.district_id = @DistrictId ");
            }

            if (request.ShopIds != null && request.ShopIds.Any())
            {
                para.Add("@ShopIds", request.ShopIds);
                condition.Append(" AND s.id in @ShopIds ");
            }

            if (request.ShopTypes != null && request.ShopTypes.Any())
            {
                para.Add("@ShopTypes", request.ShopTypes);
                condition.Append(" AND s.type in @ShopTypes ");
            }
            if (request.CompanyId > 0)
            {
                para.Add("@CompanyId", request.CompanyId);
                condition.Append(" AND s.company_id = @CompanyId ");
            }

            //condition.Append(" ORDER BY s.id desc");
            sql = sql + condition.ToString();
              
            IEnumerable<ShopSimpleInfoDO> ShopList = new List<ShopSimpleInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<ShopSimpleInfoDO>(sql, para);
            });
           
            return ShopList.ToList();
        }


        /// <summary>
        /// 查询门店简单信息列表---不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopSimpleInfoDO>> GetShopListByIdsAsync(List<long> ShopIds)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.province,
    s.city,
    s.district,
    s.province_id ProvinceId,
    s.city_id CityId,
    s.district_id DistrictId,
    s.address,
    s.type,
	s.contact,
	s.telephone,
	s.mobile
FROM
    shop s WHERE s.is_deleted = 0 AND s.id in @ShopIds ";

            var para = new DynamicParameters();
            para.Add("@ShopIds", ShopIds);

            IEnumerable<ShopSimpleInfoDO> ShopList = new List<ShopSimpleInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<ShopSimpleInfoDO>(sql, para);
            });
            return ShopList.ToList();
        }


        /// <summary>
        /// 查询门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopSimpleInfoDO> GetShopSimpleInfoAsync(long shopId)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.type,
    s.system_type SystemType,
    s.check_status CheckStatus,s.company_id CompanyId, 
	CONCAT(s.province,s.city,s.district,s.address) as Address,
	s.contact,
	s.telephone,
	s.mobile,
    s.head Head,
    s.head_phone HeadPhone,
    s.head_email HeadEmail,
    s.owner_name OwnerName,
    s.owner_phone OwnerPhone,
    sc.start_work_time as StartWorkTime,
    sc.end_work_time as EndWorkTime,
    s.score,
    s.nature,
    s.total_order as TotalOrder,
    img.img_url as ShopImageUrl,s.city_id as CityId,s.district_id as DistrictId
FROM
    shop s LEFT JOIN shop_config sc ON s.id = sc.shop_id
LEFT JOIN shop_img img ON s.id = img.shop_id AND img.type = 1 and img.is_deleted = 0
WHERE s.is_deleted = 0 AND s.id = @ShopId;";
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);

            IEnumerable<ShopSimpleInfoDO> shopSimples = new List<ShopSimpleInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                shopSimples = (await conn.QueryAsync<ShopSimpleInfoDO>(sql, para));
            });
            return shopSimples?.FirstOrDefault();
        }

        /// <summary>
        /// 查询门店简单信息--小程序，app
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopSimpleInfoDO> GetShopSimpleInfoForApp(long shopId)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
	CONCAT(s.province,s.city,s.district,s.address) as Address,
	s.contact,
	s.telephone,
	s.mobile,
    s.head Head,
    s.head_phone HeadPhone,
    s.head_email HeadEmail,
    sc.start_work_time as StartWorkTime,
    sc.end_work_time as EndWorkTime,
    s.score,
    s.total_order as TotalOrder
FROM
    shop s LEFT JOIN shop_config sc ON s.id = sc.shop_id
WHERE s.is_deleted = 0 AND s.status != 1 AND s.check_status = 2 AND s.online = 1 AND s.id = @ShopId ;";
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);

            IEnumerable<ShopSimpleInfoDO> shopSimples = new List<ShopSimpleInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                shopSimples = await conn.QueryAsync<ShopSimpleInfoDO>(sql, para);
            });
            return shopSimples?.FirstOrDefault();
        }


        /// <summary>
        /// 查询一城市门店信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public async Task<List<ShopInfoDO>> GetCityShopListAsync(long cityId)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    CONCAT(s.province,s.city,s.district,s.address) as Address,
    s.city_id as CityId,
    s.district_id as DistrictId,
	s.contact,
	s.telephone,
	s.mobile
FROM
    shop s WHERE s.city_id = @cityId AND s.is_deleted = 0 AND s.status != 1 AND s.check_status = 2 AND s.online = 1;";
            var para = new DynamicParameters();
            para.Add("@cityId", cityId);

            IEnumerable<ShopInfoDO> shops = new List<ShopInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                shops = (await conn.QueryAsync<ShopInfoDO>(sql, para));
            });
            return shops.ToList();
        }



        /// <summary>
        /// 查询门店信息---APP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopDetailForAppDO> GetShopDetailForAppAsync(long shopId)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
	CONCAT(s.province,s.city,s.district,s.address) as Address,
	s.contact,
	s.telephone,
	s.mobile,
    s.head Head,
    s.head_phone HeadPhone,
    s.head_email HeadEmail,
    s.description Description,
    s.applet_code AppletCode,s.shop_applet_code ShopAppletCode,
    sc.start_work_time as StartWorkTime,
    sc.end_work_time as EndWorkTime,
    s.score,
    s.total_order as TotalOrder,
    s.owner_name OwnerName,
    s.owner_phone OwnerPhone,
    img.img_url as Img,
sc.car_fault_diagnostic_tool as CarFaultDiagnosticTool,
sc.is_lounge_room as IsLoungeRoom,
sc.is_rest_room as IsRestRoom,
sc.is_free_wifi as IsFreeWifi,
sc.is_post_lift as IsPostLift,s.applet_code AppletCode,s.tag_name TagName
FROM
    shop s LEFT JOIN shop_config sc ON s.id = sc.shop_id
LEFT JOIN shop_img img ON s.id = img.shop_id AND img.type = 1 and img.is_deleted = 0
WHERE s.is_deleted = 0 AND s.id = @ShopId;";
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);

            IEnumerable<GetShopDetailForAppDO> shopSimples = new List<GetShopDetailForAppDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                shopSimples = (await conn.QueryAsync<GetShopDetailForAppDO>(sql, para));
            });
            return shopSimples?.FirstOrDefault();
        }
        /// <summary>
        /// 修改门店信息--SHOP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopInfoForShopAsync(ShopDO model)
        {
            var num = await UpdateAsync(model, new[] { "Contact", "Telephone", "Mobile", "OwnerName", "OwnerPhone", "Charge", "ChargePhone", "TagName","Head","HeadPhone",
                        "Type", "Status", "Online", "ProvinceId", "CityId", "DistrictId", "Province", "City", "District", "Address", "Longitude", "Latitude", "Post", "UpdateBy", "UpdateTime","SystemType"});

            return num;
        }

        public async Task<int> UpdateShopInfoAsync(ShopDO model)
        {
            var num = await UpdateAsync(model, new[] { "OwnerName", "OwnerPhone","SimpleName","FullName",
                        "ProvinceId", "CityId", "DistrictId", "Province", "City", "District", "Address",  "UpdateBy", "UpdateTime"});

            return num;
        }


        /// <summary>
        /// 修改门店基本信息--BOSS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopBaseInfoAsync(ModifyShopBaseInfoRequest request)
        {
            string sql = @"update shop set business_type = @BusinessType,company_id = @CompanyId,
contact = @Contact,full_name = @FullName,
mobile = @Mobile,owner_name = @OwnerName,owner_phone = @OwnerPhone,shop_company_name = @ShopCompanyName,
simple_name = @SimpleName,status = @Status,telephone = @Telephone,type = @Type,head = @Head,head_phone = @HeadPhone,head_email = @HeadEmail,update_by = @UpdateBy,update_time = @UpdateTime,tag_name=@tagName,system_type=@SystemType where id = @ShopId";

            var para = new DynamicParameters();
            para.Add("@BusinessType", request.BusinessType);
            para.Add("@CompanyId", request.CompanyId);
            para.Add("@Contact", request.Contact);
            para.Add("@FullName", request.FullName);
            para.Add("@Mobile", request.Mobile);
            para.Add("@OwnerName", request.OwnerName);
            para.Add("@OwnerPhone", request.OwnerPhone);
            para.Add("@ShopCompanyName", request.ShopCompanyName);
            para.Add("@SimpleName", request.SimpleName);
            para.Add("@Status", request.Status);
            para.Add("@Telephone", request.Telephone);
            para.Add("@Type", request.Type);
            para.Add("@ShopId", request.ShopId);
            para.Add("@Head", request.Head);
            para.Add("@HeadPhone", request.HeadPhone);
            para.Add("@HeadEmail", request.HeadEmail);
            para.Add("@UpdateBy", request.UpdateBy);
            para.Add("@UpdateTime", DateTime.Now);
            para.Add("@tagName", request.TagName);
            para.Add("@SystemType", request.SystemType);
            var num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num > 0;
        }

        /// <summary>
        /// 修改门店地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopAddressAsync(ModifyShopAddressRequest request)
        {
            string sql = @"update shop set province_id = @ProvinceId,city_id = @CityId,district_id = @DistrictId,
province = @Province,city = @City,district = @District,address = @Address,
longitude = @Longitude,latitude = @Latitude,post = @Post,update_by = @UpdateBy,update_time = @UpdateTime where id = @ShopId";

            var para = new DynamicParameters();
            para.Add("@ProvinceId", request.ProvinceId);
            para.Add("@CityId", request.CityId);
            para.Add("@DistrictId", request.DistrictId);
            para.Add("@Province", request.Province);
            para.Add("@City", request.City);
            para.Add("@District", request.District);
            para.Add("@Address", request.Address);
            para.Add("@Longitude", request.Longitude);
            para.Add("@Latitude", request.Latitude);
            para.Add("@Post", request.Post);
            para.Add("@ShopId", request.ShopId);
            para.Add("@UpdateBy", request.UpdateBy);
            para.Add("@UpdateTime", DateTime.Now);

            var num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num > 0;
        }

        /// <summary>
        /// 更新门店好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopScoreAsync(List<ShopScoreDO> ShopScoreList)
        {
            var sql = @"update shop set score = @Score,total_order = @TotalOrder,applause_rate = @ApplauseRate where id = @ShopId ;";
            var shopScores = ShopScoreList;
            int num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, shopScores);
            });

            return num;
        }

        public async Task<List<ShopTagDO>> GetShopTags(ShopTagDO request)
        {
            var param = new DynamicParameters();
            string sqlWhere = string.Empty;
            if (request.ShopId > 0)
            {
                param.Add("@shop_id", request.ShopId);
                sqlWhere += " and shop_id=@shopId";
            }

            var sql = $@"SELECT id,tag_name TagName,shop_id ShopId from shop_tag where is_deleted=0 and (shop_id=0{sqlWhere})";
            IEnumerable<ShopTagDO> shopTags = null;

            await OpenConnectionAsync(async conn => shopTags = await conn.QueryAsync<ShopTagDO>(sql, param));

            return shopTags?.ToList() ?? new List<ShopTagDO>();
        }

        /// <summary>
        /// 更新门店二维码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopAppletCode(ShopDO request)
        {
            var sql = @"UPDATE shop 
                        SET shop_applet_code = @applet_code,
                        update_by = @update_by,
                        update_time = SYSDATE()
                        WHERE
                            id = @id";

            var param = new DynamicParameters();
            param.Add("@applet_code", request.ShopAppletCode);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@id", request.Id);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更新快速排队二维码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateQuickQueueAppletCode(ShopDO request)
        {
            var sql = @"UPDATE shop 
                        SET applet_code = @applet_code,
                        update_by = @update_by,
                        update_time = SYSDATE()
                        WHERE
                            id = @id";

            var param = new DynamicParameters();
            param.Add("@applet_code", request.AppletCode);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@id", request.Id);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更新门店基本信息For开店指导
        /// </summary>
        /// <param name="shopBaseInfo"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopBaseInfoForOpeningGuide(ShopBaseInfoVO request, long shopId, int nature)
        {
            string sql = @"update shop set  full_name = @FullName,";
            if (nature == 1)
            {
                sql += @" simple_name = @FullName,description = @Description,";
            }
            sql += @"owner_name = @OwnerName,owner_phone = @OwnerPhone,shop_company_name = @ShopCompanyName,
tag_name = @TagName,update_by = @UpdateBy,update_time = @UpdateTime where id = @ShopId";
            var para = new DynamicParameters();

            para.Add("@FullName", request.SimpleName);
            para.Add("@Description", request.Description);
            para.Add("@OwnerName", request.OwnerName);
            para.Add("@OwnerPhone", request.OwnerPhone);
            para.Add("@ShopCompanyName", request.ShopCompanyName);
            para.Add("@ShopId", shopId);
            para.Add("@Head", request.Head);
            para.Add("@HeadPhone", request.HeadPhone);
            para.Add("@TagName", request.TagNameOne);
            para.Add("@UpdateBy", request.UpdateBy);
            para.Add("@UpdateTime", DateTime.Now);
            var num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num;
        }



        /// <summary>
        ///  更新门店地址信息For 开店指导
        /// </summary>
        /// <param name="shopAddressInfoVO"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopAddressInfoForOpeningGuide(ShopAddressInfoVO request, long shopId)
        {
            string sql = @"update shop set province_id = @ProvinceId,city_id = @CityId,district_id = @DistrictId,
province = @Province,city = @City,district = @District,address = @Address,
longitude = @Longitude,latitude = @Latitude,update_by = @UpdateBy,update_time = @UpdateTime where id = @ShopId";

            var para = new DynamicParameters();
            para.Add("@ProvinceId", request.ProvinceId);
            para.Add("@CityId", request.CityId);
            para.Add("@DistrictId", request.DistrictId);
            para.Add("@Province", request.Province);
            para.Add("@City", request.City);
            para.Add("@District", request.District);
            para.Add("@Address", request.Address);
            para.Add("@Longitude", request.Longitude);
            para.Add("@Latitude", request.Latitude);
            para.Add("@ShopId", shopId);
            para.Add("@UpdateBy", request.UpdateBy);
            para.Add("@UpdateTime", DateTime.Now);

            var num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num;
        }


        /// <summary>
        /// 更新审核状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateExaminedStatus(UpdateExaminedStatusRequest request)
        {
            var sql = "";
            var param = new DynamicParameters();
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@id", request.ShopId);
            param.Add("@examiner", request.UpdateBy);

            ///审核不通过  后期会完善其他信息
            if (request.CheckStatus == 2)
            {
                sql = @"UPDATE shop 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        check_status = 3,examiner =@examiner,
                        failed_examined_reason = @failed_examined_reason 
                        WHERE
	                        id = @id";
                param.Add("@failed_examined_reason", request.FailedExaminedReason);
            }
            else if (request.CheckStatus == 1)
            {
                //审核通过
                sql = @"UPDATE shop 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        check_status = 2 ,examiner =@examiner
                        WHERE
	                        id = @id";
            }
            else if (request.CheckStatus == 0)
            {
                //待审核
                sql = @"UPDATE shop 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        check_status = 1 ,examiner =@examiner
                        WHERE
	                        id = @id";
            }
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }


        /// <summary>
        /// 查询门店专职顾问信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopSimpleInfoDO>> GetShopHeaderListByAsync()
        {
            string sql = @"SELECT
    DISTINCT head
FROM
    shop WHERE is_deleted = 0 ";

            IEnumerable<ShopSimpleInfoDO> ShopList = new List<ShopSimpleInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<ShopSimpleInfoDO>(sql);
            });
            return ShopList.ToList();
        }

        /// <summary>
        /// 修改门店上下架状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopOnline(UpdateOnlineRequest request)
        {
            var sql = "";
            var param = new DynamicParameters();
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@id", request.ShopId);
            param.Add("@online", request.Online);

            sql = @"UPDATE shop 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        online = @online
                        WHERE
	                        id = @id";

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 查询需要统计员工绩效的门店
        /// 状态非终止 审核通过 已上架
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopInfoDO>> GetShopPerformances()
        {
            var sql = @"SELECT
	                    s.id,
	                    s.simple_name AS SimpleName 
                    FROM
	                    shop s
                    WHERE
	                    is_deleted = 0 
	                    AND STATUS != 1 
	                    AND check_status = 2 
	                    AND ONLINE = 1";

            IEnumerable<ShopInfoDO> shops = null;
            await OpenConnectionAsync(async conn => shops = await conn.QueryAsync<ShopInfoDO>(sql));
            return shops?.ToList() ?? new List<ShopInfoDO>(0);
        }
        /// <summary>
        /// 修改门店老板身份证图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopIDCard(ShopDO request)
        {
            var sql = @"UPDATE shop 
                        SET id_card_front = @IdCardFront,
                        id_card_back = @IdCardBack,
                        update_by = @UpdateBy,
                        update_time = SYSDATE()
                        WHERE
                            id = @id";

            var param = new DynamicParameters();
            param.Add("@IdCardFront", request.IdCardFront);
            param.Add("@IdCardBack", request.IdCardBack);
            param.Add("@UpdateBy", request.UpdateBy);
            param.Add("@id", request.Id);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
        /// <summary>
        /// 查询公司下属门店数量
        /// </summary>
        /// <param name="companyIds"></param>
        /// <returns></returns>
        public async Task<List<CompanyContainShopsDO>> GetShopCountUnderTheCompanyAsync(List<long> companyIds)
        {
            string sql = @"SELECT
    company_id CompanyId,COUNT(*) TotalCount
FROM
    shop WHERE company_id in @companyIds and is_deleted = 0 and online = 1 GROUP BY company_id";
            var param = new DynamicParameters();
            param.Add("@companyIds", companyIds);

            IEnumerable<CompanyContainShopsDO> ShopList = new List<CompanyContainShopsDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<CompanyContainShopsDO>(sql, param);
            });
            return ShopList.ToList();
        }

        public async Task<PagedEntity<ShopDO>> GetNearCityShopInfo(GetNearCityShopInfoRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where 1=1");
            if (string.IsNullOrWhiteSpace(request.CityId) && string.IsNullOrWhiteSpace(request.DistrictId))
            {
                return await Task.FromResult(new PagedEntity<ShopDO>(totalItems: 0, items: null));
            }
            if (!string.IsNullOrWhiteSpace(request.CityId))
            {
                builder.AppendLine(" and city_id = @CityId");
                parameters.Add("@CityId", request.CityId);
            }
            if (!string.IsNullOrWhiteSpace(request.DistrictId))
            {
                builder.AppendLine(" and district_id = @DistrictId");
                parameters.Add("@DistrictId", request.DistrictId);
            }
            if (request.ShopType != null && request.ShopType.Any())
            {
                builder.AppendLine(" and type in @Types");
                parameters.Add("@Types", request.ShopType);
            }

            return await GetListPagedAsync<ShopDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                       parameters, false);
        }


        public async Task<bool> UpdateShopDeposit(UpdateShopDepositRequest request)
        {
            var sql = "";
            var param = new DynamicParameters();
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@id", request.ShopId);
            param.Add("@DepositAmount", request.Amount);

            sql = @"UPDATE shop 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        deposit_amount =deposit_amount+ @DepositAmount
                        WHERE
	                        id = @id";

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result > 0;
        }

        /// <summary>
        /// 根据shopType查询所有门店
        /// </summary>
        /// <param name="shopType"></param>
        /// <returns></returns>
        public async Task<List<ShopDO>> GetShopsByType(int shopType)
        {
            var para = new DynamicParameters();
            para.Add("@type", shopType);
            var result =
                await GetListAsync<ShopDO>(
                    "WHERE `type` = @type AND `status` = 0 AND `check_status` = 2 AND `online` = 1", para);

            return result?.ToList() ?? new List<ShopDO>();
        }

      
    }
}
