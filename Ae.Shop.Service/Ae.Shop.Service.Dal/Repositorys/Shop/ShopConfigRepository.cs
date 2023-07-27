using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;
using Ae.Shop.Service.Core.Request;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopConfigRepository : AbstractRepository<ShopConfigDO>, IShopConfigRepository
    {
        public ShopConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 新增门店配置信息
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public async Task<long> AddAsync(ShopConfigDO shopConfig)
        {
            int id = await InsertAsync(shopConfig);
            return id;
        }

        /// <summary>
        /// 根据门店ID查询门店配置信息
        /// </summary>
        /// <param name="shopId">门店id</param>
        /// <returns></returns>
        public async Task<ShopConfigDO> GetShopConfigByShopIdAsync(long shopId)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE is_deleted = 0");
            condition.Append(" AND shop_id = @shopId");
            para.Add("@shopId", shopId);

            var shopConfig = (await GetListAsync<ShopConfigDO>(condition.ToString(), para)).FirstOrDefault();
            return shopConfig;
        }
        /// <summary>
        /// 修改门店配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopConfigInfoAsync(ModifyShopConfigInfoRequest request)
        {
            string sql = @"update shop_config set start_work_time = @StartWorkTime,end_work_time = @EndWorkTime,";
                if(request.Type != 1)
                {
                    sql += @"online_time = @OnlineTime,
                    lease_start_date = @LeaseStartDate,
                    lease_end_date = @LeaseEndDate,";
                }
                    
            sql += @" car_fault_diagnostic_tool = @CarFaultDiagnosticTool,is_lounge_room = @IsLoungeRoom,is_rest_room = @IsRestRoom,is_free_wifi = @IsFreeWifi,
is_post_lift = @IsPostLift,suspend_start_date_time = @SuspendStartDateTime,suspend_end_date_time = @SuspendEndDateTime, update_by = @UpdateBy,update_time = @UpdateTime where shop_id = @ShopId";

            var para = new DynamicParameters();
            para.Add("@StartWorkTime", request.StartWorkTime);
            para.Add("@EndWorkTime", request.EndWorkTime);
            para.Add("@OnlineTime", request.OnlineTime);
            para.Add("@LeaseStartDate", request.LeaseStartDate);
            para.Add("@LeaseEndDate", request.LeaseEndDate);
            para.Add("@CarFaultDiagnosticTool", request.CarFaultDiagnosticTool);
            para.Add("@IsLoungeRoom", request.IsLoungeRoom);
            para.Add("@IsRestRoom", request.IsRestRoom);
            para.Add("@IsFreeWifi", request.IsFreeWifi);
            para.Add("@IsPostLift", request.IsPostLift);
            para.Add("@SuspendStartDateTime", request.SuspendStartDateTime);
            para.Add("@SuspendEndDateTime", request.SuspendEndDateTime);
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
        /// 修改门店配置服务费用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopConfigExpenseAsync(ModifyShopConfigExpenseRequest request)
        {
            string sql = @"update shop_config set fee_per_tire = @FeePerTire,fee_per_front_brake = @FeePerFrontBrake,fee_per_rear_brake = @FeePerRearBrake,
fee_per_front_disc = @FeePerFrontDisc,fee_per_rear_disc = @FeePerRearDisc,fee_per_maintain = @FeePerMaintain,fee_per_4_wheel = @FeePer4Wheel,
fee_pm_installation = @FeePmInstallation,daily_order_quantity = @DailyOrderQuantity,daily_order_upper_limit = @DailyOrderUpperLimit,
option_order_count = @OptionOrderCount,daily_tire_order_quantity = @DailyTireOrderQuantity,daily_tire_order_upper_limit = @DailyTireOrderUpperLimit,
option_tire_order_count = @OptionTireOrderCount,update_by = @UpdateBy,update_time = @UpdateTime where shop_id = @ShopId";

            var para = new DynamicParameters();
            para.Add("@FeePerTire", request.FeePerTire);
            para.Add("@FeePerFrontBrake", request.FeePerFrontBrake);
            para.Add("@FeePerRearBrake", request.FeePerRearBrake);
            para.Add("@FeePerFrontDisc", request.FeePerFrontDisc);
            para.Add("@FeePerRearDisc", request.FeePerRearDisc);
            para.Add("@FeePerMaintain", request.FeePerMaintain);
            para.Add("@FeePer4Wheel", request.FeePer4Wheel);
            para.Add("@FeePmInstallation", request.FeePmInstallation);
            para.Add("@DailyOrderQuantity", request.DailyOrderQuantity);
            para.Add("@DailyOrderUpperLimit", request.DailyOrderUpperLimit);
            para.Add("@OptionOrderCount", request.OptionOrderCount);
            para.Add("@DailyTireOrderQuantity", request.DailyTireOrderQuantity);
            para.Add("@DailyTireOrderUpperLimit", request.DailyTireOrderUpperLimit);
            para.Add("@OptionTireOrderCount", request.OptionTireOrderCount);
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
        /// 修改暂停营业时间 更新营业状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> ModifyShopSuspendTime(ModifyShopSuspendTimeRequest request)
        {
            var startTime = DateTime.Parse(request.Times[0]);
            var endTime = DateTime.Parse(request.Times[1]);
            var sql = @"UPDATE shop_config 
                        SET suspend_start_date_time = @suspend_start_date_time,
                        suspend_end_date_time = @suspend_end_date_time,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        shop_id = @shop_id;

                        UPDATE shop 
                        SET status = 2,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        id = @shop_id;";
            var param = new DynamicParameters();
            param.Add("@suspend_start_date_time", startTime);
            param.Add("@suspend_end_date_time", endTime);
            param.Add("@update_by", request.UdpateBy);
            param.Add("@shop_id", request.ShopId);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
