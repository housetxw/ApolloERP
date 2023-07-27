using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopBankCardRepository : AbstractRepository<ShopBankCardDO>, IShopBankCardRepository
    {
        public ShopBankCardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }


        /// <summary>
        /// 根据门店ID查询门店银行信息
        /// </summary>
        /// <param name="shopId">门店id</param>
        /// <returns></returns>
        public async Task<ShopBankCardInfoDO> GetShopBankCardByShopIdAsync(long shopId)
        {
            string sql = @"SELECT
    cd.Id Id,
	cd.shop_id ShopId,
    cd.type,
    cd.bank_id BankId,
	b.bank_name BankName,
	b.icon_url IconUrl,
	cd.opening_bank OpeningBank,
    cd.opening_branch OpeningBranch,
	cd.bank_no BankNo,
	cd.opening_user_name OpeningUserName,
	cd.bank_card_no BankCardNo,
    cd.company_name CompanyName,
    cd.opening_licence OpeningLicence
FROM
	shop_bank_card cd
	LEFT JOIN bank_information b ON cd.bank_id = b.id";
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE cd.is_deleted = 0 AND cd.shop_id = @shopId");
            para.Add("@shopId", shopId);
            sql = sql + condition.ToString();

            IEnumerable<ShopBankCardInfoDO> list = new List<ShopBankCardInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<ShopBankCardInfoDO>(sql, para);
            });
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// 修改门店银行账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyShopBankAccountAsync(ModifyShopBankAccountRequest request)
        {
            string sql = @"update shop_bank_card set ";
            if (request.Source == 2) 
            {
                sql += "type=@Type,bank_id =@BankId,company_name = @CompanyName,opening_licence = @OpeningLicence,";
            }
            sql += @"opening_bank = @OpeningBank,opening_branch = @OpeningBranch,bank_no = @BankNo,opening_user_name = @OpeningUserName,bank_card_no = @BankCardNo,
                    update_by = @UpdateBy,update_time = @UpdateTime where shop_id = @ShopId";

            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);
            para.Add("@Type", request.Type);
            para.Add("@BankId", request.BankId);
            para.Add("@OpeningBank", request.OpeningBank);
            para.Add("@OpeningBranch", request.OpeningBranch);
            para.Add("@BankNo", request.BankNo);
            para.Add("@OpeningUserName", request.OpeningUserName);
            para.Add("@BankCardNo", request.BankCardNo);
            para.Add("@CompanyName", request.CompanyName);
            para.Add("@OpeningLicence", request.OpeningLicence);
            para.Add("@UpdateBy", request.UpdateBy);
            para.Add("@UpdateTime", DateTime.Now);

            var num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num > 0;
        }
    }
}
