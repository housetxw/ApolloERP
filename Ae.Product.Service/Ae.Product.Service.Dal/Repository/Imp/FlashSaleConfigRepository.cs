using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Ae.Product.Service.Dal.Repository
{

    public class FlashSaleConfigRepository : AbstractRepository<FlashSaleConfigDO>, IFlashSaleConfigRepository
    {
        public FlashSaleConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<bool> AutoOffFlashSaleConfig()
        {
            await OpenConnectionAsync(async conn => await conn.ExecuteAsync(@"UPDATE flash_sale_config 
                                                                            SET `status` = '已结束',
                                                                            update_by = 'System',
                                                                            update_time = SYSDATE( ) 
                                                                            WHERE
	                                                                            active_end_time < SYSDATE( ) 
	                                                                            AND is_deleted = 0 
	                                                                            AND `status` = '已生效'"));
            return true;
        }
    }
}
