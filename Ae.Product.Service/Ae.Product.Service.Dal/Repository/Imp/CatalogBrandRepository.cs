using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DbType = ApolloErp.Data.DapperExtensions.DbType;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Enums;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class CatalogBrandRepository : AbstractRepository<DimBrandDO>, ICatalogBrandRepository
    {
        public CatalogBrandRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DimBrandDO>> GetCatalogBrandAsync(string brandName = null,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE is_deleted = 0");
            if (!string.IsNullOrEmpty(brandName))
            {
                condition.Append(" AND brand_name = @brand_name");
                para.Add("@brand_name", brandName);
            }

            return await GetListAsync<DimBrandDO>(condition.ToString(), para, useMaster: !readOnly);
        }

        /// <summary>
        /// 新增品牌
        /// </summary>
        /// <param name="catalogBrandsDo"></param>
        /// <returns></returns>
        public async Task<int> AddCategoryBrandAsync(DimBrandDO catalogBrandsDo)
        {
            //            string sql = @"INSERT catalog_brands ( `brand_name`, `brand_img`, `create_by` )
            //VALUES
            //	( @brandName, @brandImg, @createBy );SELECT LAST_INSERT_ID() as id;";

            //            var para = new DynamicParameters();
            //            para.Add("@brandName", catalogBrandsDo.BrandName);
            //            para.Add("@brandImg", catalogBrandsDo.BrandImg);
            //            para.Add("@createBy", catalogBrandsDo.CreateBy);

            //            int id = 0;
            //            await OpenConnectionAsync(async conn =>
            //            {
            //                id = await conn.ExecuteScalarAsync<int>(sql, para);
            //            }
            //            );
            return await InsertAsync(catalogBrandsDo);

        }

        /// <summary>
        /// 分页请求商品的品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<DimBrandDO>> GetProductBrandList(GetProductBrandListRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" where 1=1");
            if (!string.IsNullOrWhiteSpace(request.BrandName))
            {
                if (request.BrandFullMatch == false)
                {
                    builder.AppendLine(" and brand_name like CONCAT('%',@BrandName,'%')");

                }
                else
                {
                    builder.AppendLine(" and brand_name = @BrandName");
                }

                parameters.Add("@BrandName", request.BrandName.Trim());
            }

            if (request.Id > 0)
            {
                builder.AppendLine(" and id =@Id");
                parameters.Add("@Id", request.Id);
            }
            switch (request.IsForbid)
            {
                case (int)IsForbidEnumType.No:
                    builder.AppendLine(" and is_deleted =@IsDeleted");
                    parameters.Add("@IsDeleted", (int)IsForbidEnumType.No);
                    break;
                case (int)IsForbidEnumType.Yes:
                    builder.AppendLine(" and is_deleted =@IsDeleted");
                    parameters.Add("@IsDeleted", (int)IsForbidEnumType.Yes);
                    break;
                default:
                    builder.AppendLine(" and is_deleted IN @IsDeleted");
                    parameters.Add("@IsDeleted", new int[] { 0, 1 });
                    break;
            }

            var sqlCount = @"select Count(1) FROM dim_brand " + builder.ToString();
            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });


            var sql = "Select id AS Id ,brand_name AS BrandName,brand_img AS BrandImg,is_deleted AS IsDeleted, create_by AS CreateBy,create_time AS CreateTime, update_by AS UpdateBy," +
                     " update_time AS UpdateTime from dim_brand" + builder.ToString() + " Order by update_time DESC limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";

            IEnumerable<DimBrandDO> dimBrands = null;
            await OpenConnectionAsync(async conn => dimBrands = await conn.QueryAsync<DimBrandDO>(sql, parameters));


            PagedEntity<DimBrandDO> response = new PagedEntity<DimBrandDO>();
            response.TotalItems = total;
            response.Items = dimBrands.ToList();

            return response;

        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="catalogBrandsDo"></param>
        /// <returns></returns>
        public async Task<int> EditCategoryBrandAsync(DimBrandDO catalogBrandsDo)
        {
            var sql =
                "update dim_brand set brand_name=@BrandName,brand_img=@BrandImg,is_deleted=@IsDeleted,update_by=@UpdateBy,update_time=@UpdateTime where id=@Id";
            int id = 0;
            await OpenConnectionAsync(async conn =>
            {
                id = await conn.ExecuteAsync(sql, new
                {
                    BrandName = catalogBrandsDo.BrandName,
                    BrandImg = catalogBrandsDo.BrandImg,
                    IsDeleted = catalogBrandsDo.IsDeleted,
                    UpdateBy = catalogBrandsDo.UpdateBy,
                    UpdateTime = catalogBrandsDo.UpdateTime,
                    Id = catalogBrandsDo.Id
                });
            }
            );
            return id;
        }
    }
}
