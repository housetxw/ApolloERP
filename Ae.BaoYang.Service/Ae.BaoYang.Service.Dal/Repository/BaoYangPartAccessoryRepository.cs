using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPartAccessoryRepository: AbstractRepository<BaoYangPartAccessoryDO>,
        IBaoYangPartAccessoryRepository
    {
        public BaoYangPartAccessoryRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据tid查询辅料配置
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartAccessoryDO>> GetPartAccessoryByTidAsync(string tid)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            return await GetListAsync<BaoYangPartAccessoryDO>("WHERE tid = @tid", para);
        }

        /// <summary>
        /// 根据TidList查询辅料配置
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPartAccessoryDO>> GetPartAccessoryByTidListAsync(List<string> tidList)
        {
            var para = new DynamicParameters();
            para.Add("@tidList", tidList);
            return await GetListAsync<BaoYangPartAccessoryDO>("WHERE tid IN @tidList", para);
        }

        /// <summary>
        /// 根据tid和类型查询数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="accessoryName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<BaoYangPartAccessoryDO> GetPartAccessoryByTidAndTypeAsync(string tid, string accessoryName,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@accessoryName", accessoryName);
            var result =
                await GetListAsync<BaoYangPartAccessoryDO>("WHERE tid = @tid AND accessory_name = @accessoryName",
                    para, !readOnly);
            return result.FirstOrDefault();
        }


        /// <summary>
        /// 属性适配车型
        /// </summary>
        /// <param name="accessoryName"></param>
        /// <param name="volume"></param>
        /// <param name="viscosity"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SimpleVehicleDo>> GetTidByVolume(string accessoryName, int volume,
            List<string> viscosity = null, List<string> level = null, List<string> description = null)
        {
            string sql = @"SELECT DISTINCT
	tid AS Tid 
FROM
	bao_yang_part_accessory 
WHERE
	accessory_name = @accessoryName 
	AND CEILING( volume ) >= @volume{0}
	AND is_deleted = 0";

            var para = new DynamicParameters();
            para.Add("@accessoryName", accessoryName);
            para.Add("@volume", volume);

            StringBuilder condition = new StringBuilder();
            if (viscosity != null && viscosity.Any())
            {
                condition.Append(" AND viscosity IN @viscosity");
                para.Add("@viscosity", viscosity);
            }

            if (level != null && level.Any())
            {
                condition.Append(" AND `level` IN @level");
                para.Add("@level", level);
            }

            if (description != null && description.Any())
            {
                condition.Append(" AND `description` IN @description");
                para.Add("@description", description);
            }

            sql = string.Format(sql, condition);

            IEnumerable<SimpleVehicleDo> result = new List<SimpleVehicleDo>();

            await OpenSlaveConnectionAsync(
                async conn => { result = await conn.QueryAsync<SimpleVehicleDo>(sql, para); });

            return result;
        }

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="accessoryName"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccessory(string tid, string accessoryName, string submitBy)
        {
            string sql = @"UPDATE bao_yang_part_accessory 
SET is_deleted = 1,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	tid = @tid 
	AND accessory_name = @accessory_name 
	AND is_deleted =0";

            var para = new DynamicParameters();
            para.Add("@update_by", submitBy);
            para.Add("@tid", tid);
            para.Add("@accessory_name", accessoryName);

            var result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });

            return result > 0;
        }
    }
}
