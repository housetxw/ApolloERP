using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.EmployeePerformance
{
    public class EmployeePerformanceReportRepository : AbstractRepository<EmployeePerformanceReportDO>,
   IEmployeePerformanceReportRepository
    {
        public EmployeePerformanceReportRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeePerformanceDto>> GetEmployeePerformanceList(EmployeePerformanceRequest request)
        {
            //var condtion = "";
            //var key = request.Key?.Trim();
            //if (!string.IsNullOrEmpty(key))
            //{
            //    condtion += " and ( em.name like @employeeName or em.mobile = @mobile) ";
            //}
            //condtion += "  and epr.create_time >=@startDate and epr.create_time <= @endDate";
            //var sql = $@"select  em.id EmployeeId,em.name EmployeeName,SUM(IFNULL(epr.install_point,0)) InstallPoint,
            //            SUM(IFNULL(epr.pull_new_point,0)) PullNewPoint,SUM(IFNULL(epr.cunsume_point,0)) CunsumePoint,
            //            SUM(IFNULL(epr.total_point,0)) TotalPoint,max(employee_phone) Mobile
            //            from employee em 
            //            join employee_performance_report epr on em.id=epr.employee_id  
            //            where em.is_deleted=0 and epr.is_deleted=0  
            //            and em.dimission_type=0 and em.type=1 and em.organization_id=@shop_id
            //            {condtion} group by em.id ";

            //var sortFiled = "";
            //switch (request.SortType)
            //{
            //    case PerformanceSortType.TotalPoint:
            //        sortFiled = " order by TotalPoint desc ";
            //        break;
            //    case PerformanceSortType.InstallPoint:
            //        sortFiled = " order by InstallPoint desc ";
            //        break;
            //    case PerformanceSortType.PullNewPoint:
            //        sortFiled = " order by PullNewPoint desc ";
            //        break;
            //    case PerformanceSortType.CunsumePoint:
            //        sortFiled = " order by CunsumePoint desc ";
            //        break;
            //    default:
            //        sortFiled = " order by TotalPoint desc ";
            //        break;
            //}

            var condtion = "";
            var dateField = " DATE_FORMAT(install_time,'%Y-%m-01') as InstallDate,";
            var key = request.Key?.Trim();
            if (!string.IsNullOrEmpty(key))
            {
                condtion += " and ( employee_name = @Key ) ";
            }

            if (request.StartDate < DateTime.Now.AddYears(-1))
            {
                request.StartDate = DateTime.Now.AddYears(-1);
            }
            if (request.EndDate < DateTime.Now.AddYears(-1))
            {
                request.EndDate = DateTime.Now.AddDays(1);
            }

            condtion += " and create_time >=@StartDate and create_time < @EndDate ";

            var groupFiled = " GROUP BY EmployeeName,InstallDate ";
            var sortFiled = " ORDER BY InstallDate desc, EmployeeName ";

            //order_no as OrderNo,install_time as InstallDate,
            var sql = $@"select EmployeeName,InstallDate,
                        SUM(CardItemNum) CardItemNum,
                        SUM(NumItemNum) NumItemNum,
						count(DISTINCT AmtItemNum) AmtItemNum,
                        SUM(CardItemAmt) CardItemAmt,
                        SUM(NumItemAmt) NumItemAmt,
                        SUM(AmtItemAmt) AmtItemAmt,
                        SUM(total_point) TotalPoint
                    from (
                        select employee_name as EmployeeName,order_no,{dateField} (total_number * percent) as num,
                        (total_number * percent * case tab_type when 1 then 1 else 0 end) CardItemNum,
                        (total_number * percent * case tab_type when 2 then 1 else 0 end) NumItemNum,
                        ( case tab_type when 3 then order_no else '' end) AmtItemNum,
                        (total_point * case tab_type when 1 then 1 else 0 end) CardItemAmt,
                        (total_point * case tab_type when 2 then 1 else 0 end) NumItemAmt,
                        (total_point * case tab_type when 3 then 1 else 0 end) AmtItemAmt,
                            total_point
                        from `shop_order`.`employee_performance_order` p
                            where  p.is_deleted = 0 and p.shop_id > 4460
                                    and p.create_time>'2023-11-01'
                        and `shop_id` =@shop_id
                        {condtion}
                            ) table1 {groupFiled} ";

            var paras = new
            {
                employeeName = '%' + key + '%',
                mobile= key,
                shop_id=request.ShopId,
                startDate=request.StartDate.ToString("yyyy-MM-dd"),
                endDate=request.EndDate.ToString("yyyy-MM-dd")
            };
            IEnumerable<EmployeePerformanceDto> result = new List<EmployeePerformanceDto>();
            OpenConnection(async conn =>
            {
                result = await conn.QueryAsync<EmployeePerformanceDto>(sql+sortFiled, paras);
            }
            );
            return result.ToList();
        }


        /// <summary>
        /// 获取员工绩效详情
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeePerformanceDetialDto> GetEmployeePerformanceDetial(EmployeePerformanceDetialRequest request)
        {
            var condtion = " and em.id=@employeeId and epr.create_time >=@startDate and epr.create_time <= @endDate";
            var sql = $@"select  em.id EmployeeId,SUM(IFNULL(epr.install_point,0)) InstallPoint,
                        SUM(IFNULL(epr.pull_new_point,0)) PullNewPoint,SUM(IFNULL(epr.cunsume_point,0)) CunsumePoint,
                        SUM(IFNULL(epr.total_point,0)) TotalPoint
                        from employee em 
                        join employee_performance_report epr on em.id=epr.employee_id  
                        where em.is_deleted=0 and epr.is_deleted=0  
                        and em.dimission_type=0 and em.type=1 
                        {condtion}  group by em.id ";
            var paras = new
            {
                employeeId=request.EmployeeId,
                startDate = request.StartDate.ToString("yyyy-MM-dd"),
                endDate = request.EndDate.ToString("yyyy-MM-dd")
            };
            IEnumerable<EmployeePerformanceDetialDto> result = new List<EmployeePerformanceDetialDto>();
            OpenConnection(async conn =>
            {
                result = await conn.QueryAsync<EmployeePerformanceDetialDto>(sql, paras);
            }
            );
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 获取技师绩效列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDto>> GetTechPerformanceList(TechPerformanceRequest request)
        {
            var dateField = "";
            if (request.GroupType == PerformanceGroupType.GroupByAll)
            {
                dateField = " '"+ request.StartDate.ToString("yyyy-MM-dd")+ "' as InstallDate, ";
            }
            else
            {
                dateField = " DATE_FORMAT(o.install_time,'%Y-%m-%d') as InstallDate, ";
            }

            var condtion = "";
            var key = request.EmployeeName?.Trim();
            if (!string.IsNullOrEmpty(key))
            {
                condtion += " and shop_order.order_dispatch.tech_name = @employeeName ";
            }
            condtion += "  and o.install_time >=@startDate and o.install_time < @endDate ";

            var groupFiled = " GROUP BY EmployeeName,InstallDate, ItemName ";
            var sortFiled = " ORDER BY EmployeeName,InstallDate, ItemName ";

            var sql_1 = $@"SELECT EmployeeName,InstallDate, ItemName, sum(num) as ItemNum, sum(amt) as ItemAmt
                        FROM (
                            select  shop_order.order_dispatch.tech_name as EmployeeName,
                            op.product_name as ItemName,{dateField}
                            (shop_order.order_dispatch.percent * op.total_number) as num,
                            (shop_order.order_dispatch.percent * op.total_amount) as amt
                            from `order`.`order` o
                            inner join  `order`.order_product op on o.id = op.order_id
                            inner join shop_manage.shop on shop_manage.shop.id = o.shop_id
                            inner join shop_order.order_dispatch on shop_order.order_dispatch.order_no = o.order_no  

                            where  o.produce_type = 14
                            and o.order_status =30
                            and op.is_deleted =0
                            and op.parent_order_package_pid=0
                            and shop_order.order_dispatch.is_deleted = 0
                            and shop_manage.shop.type in (2,4)
                            {condtion}
                            ) table1  {groupFiled} ";


            var sql_2 = $@"SELECT EmployeeName,InstallDate, ItemName, sum(num) as ItemNum, sum(amt) as ItemAmt
                    FROM (
                    select  case when (op.`product_id` like 'TR-%' or 
                    op.`product_id` like 'MD-BY-LT-%' ) then '轮胎'
                    when (op.`product_id` like 'MD-BY-BT-%' ) then '蓄电池'
                    when (op.`product_id` like 'TG-XC-P-%' or 
                    op.`product_id` like 'MF-XC-PX-%' ) then '普洗'
                    when (op.`product_id` like 'BYFW-SMFW-AQJC-%' or 
                    op.`product_id` like 'FW-BY-JC-%' ) then '检测'
                    when (op.`product_id` like 'BYFW-QTPJ-WP-%' ) then '通用雨刷'
                    when (op.`product_id` > 'BY-PJ-WP-1' and op.`product_id` < 'BY-PJ-WP-22') then '多功能雨刷'
                    when (op.`product_id` > 'BY-PJ-WP-22' and op.`product_id` <= 'BY-PJ-WP-44') then '镀膜雨刷'
                    else  '其他'   end as ItemName,{dateField}
                    op.actual_pay_amount * shop_order.order_dispatch.percent as amt,
                    op.total_number * shop_order.order_dispatch.percent as num,
                    shop_order.order_dispatch.tech_name as EmployeeName
                    from `order`.`order` o
                     inner join  `order`.order_product op on o.id = op.order_id
                    inner join shop_manage.shop on shop_manage.shop.id = o.shop_id
                    inner join shop_order.order_dispatch on shop_order.order_dispatch.order_no = o.order_no   

                    where  o.`order_status` =30
                    and o.produce_type not in (1,14)
                    and op.is_deleted =0
                    and (op.`product_id` like 'TR-%' or 
                    op.`product_id` like 'MD-BY-LT-%' or 
                    op.`product_id` like 'MD-BY-BT-%' or 
                    op.`product_id` like 'TG-XC-P-%' or 
                    op.`product_id` like 'MF-XC-PX-%' or 
                    op.`product_id` like 'BYFW-SMFW-AQJC-%' or 
                    op.`product_id` like 'FW-BY-JC-%' or
                    op.`product_id` like 'BY-PJ-WP-%' or
                    op.`product_id` like 'BYFW-QTPJ-WP-%' 
                    )
                    and shop_order.order_dispatch.is_deleted = 0
                    and shop_manage.shop.type in (2,4)
                    {condtion}
                    ) table1 {groupFiled} ";

            var sql_3 = $@"SELECT EmployeeName,InstallDate, ItemName, sum(num) as ItemNum, sum(amt) as ItemAmt
                    FROM (
                        select  case 
                        when (op.`product_id` like 'TG-%' or 
                        op.`product_id` like 'MF-%'  or  -- 美容服务
                        op.`product_id` like 'MR-%'  or  -- 美容产品
                        op.`product_id` like 'FW-CP-TM-%'  or   -- 贴膜安装
                        op.`product_id` like 'BYFW-SMFW-CLXD-%'  or 
                        op.`product_id` like 'FW-BY-XD-%' )   -- 车辆消毒
                        then '精洗美容'
                        else  '机修' end as ItemName,{dateField}
                        (case when o.produce_type = 15 then c.avg_price * c.num 
                            else ROUND(op.total_amount - (o.total_coupon_amount * op.total_number / o.total_product_num)) end) * shop_order.order_dispatch.percent as amt,
                        op.total_number * shop_order.order_dispatch.percent as num,
                        shop_order.order_dispatch.tech_name as EmployeeName
                        from `order`.`order` o
                         inner join  `order`.order_product op on o.id = op.order_id
                        inner join shop_manage.shop on shop_manage.shop.id = o.shop_id
                        inner join shop_order.order_dispatch on shop_order.order_dispatch.order_no = o.order_no 
                        left join `order`.order_package_card c on o.produce_type = 15 and o.order_no = c.verify_order_no

                        where  o.`order_status` =30
                        and o.produce_type not in (1,14)
                        and op.`is_deleted` =0
                        and op.parent_order_package_pid=0
                        and op.`product_id` not like 'TR-%' 
                        and op.`product_id` not like 'MD-BY-LT-%' -- 轮胎
                        and op.`product_id` not like 'MD-BY-BT-%' -- 蓄电池
                        and op.`product_id` not like 'TG-XC-P-%' 
                        and op.`product_id` not like 'MF-XC-PX-%' -- 普洗
                        and op.`product_id` not like 'BYFW-SMFW-AQJC-%' 
                        and op.`product_id` not like 'FW-BY-JC-%'  -- 检测
                        and op.`product_id` not like 'BY-PJ-WP-%' 
                        and op.`product_id` not like 'BYFW-QTPJ-WP-%'  -- 雨刷

                        and shop_order.order_dispatch.is_deleted = 0
                        and shop_manage.shop.type in (2,4)
                        {condtion}
                    ) table1 {groupFiled} ";

            var sql = "";
            switch (request.ItemType)
            {
                case PerformanceItemType.PackageCard:
                    sql = sql_1;
                    break;
                case PerformanceItemType.NumItem:
                    sql = sql_2;
                    break;
                default:
                    sql = sql_3;
                    break;
            }
            var paras = new
            {
                employeeName = key ,
                startDate = request.StartDate.ToString("yyyy-MM-dd"),
                endDate = request.EndDate.AddDays(1).ToString("yyyy-MM-dd")
            };
            IEnumerable<TechPerformanceDto> result = new List<TechPerformanceDto>();
            OpenConnection(async conn =>
            {
                result = await conn.QueryAsync<TechPerformanceDto>(sql + sortFiled , paras);
            }
            );
            return result.ToList();
        }

        /// <summary>
        /// 获取技师绩效明细V2
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetail(TechPerformanceDetailRequest request)
        {

            var condtion = "";
            var key = request.EmployeeName?.Trim();
            if (!string.IsNullOrEmpty(key))
            {
                condtion += " and shop_order.order_dispatch.tech_name = @employeeName ";
            }
            condtion += "  and o.install_time >=@startDate and o.install_time < @endDate ";

            var condtion2 = " where 1=1 ";
            var key2 = request.ItemName?.Trim();
            if (!string.IsNullOrEmpty(key2))
            {
                condtion2 += " and ptype = @itemName ";
            }

            var sortFiled = " ORDER BY OrderNo Desc ";

            var sql_1 = $@"SELECT EmployeeName,InstallDate,OrderNo, ItemName, (num) as ItemNum, (amt) as ItemAmt
                        FROM (
                            select  shop_order.order_dispatch.tech_name as EmployeeName,op.product_name as ptype,
                            op.product_name as ItemName,o.order_no as OrderNo,o.install_time as InstallDate,
                            (shop_order.order_dispatch.percent * op.total_number) as num,
                            (shop_order.order_dispatch.percent * op.total_amount) as amt
                            from `order`.`order` o
                            inner join  `order`.order_product op on o.id = op.order_id
                            inner join shop_manage.shop on shop_manage.shop.id = o.shop_id
                            inner join shop_order.order_dispatch on shop_order.order_dispatch.order_no = o.order_no  

                            where  o.produce_type = 14
                            and o.order_status =30
                            and op.is_deleted =0
                            and op.parent_order_package_pid=0
                            and shop_order.order_dispatch.is_deleted = 0
                            and shop_manage.shop.type in (2,4)
                            {condtion}
                            ) table1  {condtion2}  ";


            var sql_2 = $@"SELECT EmployeeName,InstallDate,OrderNo, ItemName, (num) as ItemNum, (amt) as ItemAmt
                    FROM (
                    select  case when (op.`product_id` like 'TR-%' or 
                    op.`product_id` like 'MD-BY-LT-%' ) then '轮胎'
                    when (op.`product_id` like 'MD-BY-BT-%' ) then '蓄电池'
                    when (op.`product_id` like 'TG-XC-P-%' or 
                    op.`product_id` like 'MF-XC-PX-%' ) then '普洗'
                    when (op.`product_id` like 'BYFW-SMFW-AQJC-%' or 
                    op.`product_id` like 'FW-BY-JC-%' ) then '检测'
                    when (op.`product_id` like 'BYFW-QTPJ-WP-%' ) then '通用雨刷'
                    when (op.`product_id` > 'BY-PJ-WP-1' and op.`product_id` < 'BY-PJ-WP-22') then '多功能雨刷'
                    when (op.`product_id` > 'BY-PJ-WP-22' and op.`product_id` <= 'BY-PJ-WP-44') then '镀膜雨刷'
                    else  '其他'  end as ptype,o.order_no as OrderNo,o.install_time as InstallDate,op.product_name as ItemName,
                    op.actual_pay_amount * shop_order.order_dispatch.percent as amt,
                    op.total_number * shop_order.order_dispatch.percent as num,
                    shop_order.order_dispatch.tech_name as EmployeeName
                    from `order`.`order` o
                     inner join  `order`.order_product op on o.id = op.order_id
                    inner join shop_manage.shop on shop_manage.shop.id = o.shop_id
                    inner join shop_order.order_dispatch on shop_order.order_dispatch.order_no = o.order_no   

                    where  o.`order_status` =30
                    and o.produce_type not in (1,14)
                    and op.is_deleted =0
                    and (op.`product_id` like 'TR-%' or 
                    op.`product_id` like 'MD-BY-LT-%' or 
                    op.`product_id` like 'MD-BY-BT-%' or 
                    op.`product_id` like 'TG-XC-P-%' or 
                    op.`product_id` like 'MF-XC-PX-%' or 
                    op.`product_id` like 'BYFW-SMFW-AQJC-%' or 
                    op.`product_id` like 'FW-BY-JC-%' or
                    op.`product_id` like 'BY-PJ-WP-%' or
                    op.`product_id` like 'BYFW-QTPJ-WP-%' 
                    )
                    and shop_order.order_dispatch.is_deleted = 0
                    and shop_manage.shop.type in (2,4)
                    {condtion}
                    ) table1  {condtion2}   ";

            var sql_3 = $@"SELECT EmployeeName,InstallDate,OrderNo, ItemName, (num) as ItemNum, (amt) as ItemAmt
                    FROM (
                        select  case 
                        when (op.`product_id` like 'TG-%' or 
                        op.`product_id` like 'MF-%'  or  -- 美容服务
                        op.`product_id` like 'MR-%'  or  -- 美容产品
                        op.`product_id` like 'FW-CP-TM-%'  or   -- 贴膜安装
                        op.`product_id` like 'BYFW-SMFW-CLXD-%'  or 
                        op.`product_id` like 'FW-BY-XD-%' )   -- 车辆消毒
                        then '精洗美容'
                        else  '机修' end as ptype,o.order_no as OrderNo,o.install_time as InstallDate,op.product_name as ItemName,
                        (case when o.produce_type = 15 then c.avg_price * c.num 
                            else ROUND(op.total_amount - (o.total_coupon_amount * op.total_number / o.total_product_num)) end) * shop_order.order_dispatch.percent as amt,
                        op.total_number * shop_order.order_dispatch.percent as num,
                        shop_order.order_dispatch.tech_name as EmployeeName
                        from `order`.`order` o
                         inner join  `order`.order_product op on o.id = op.order_id
                        inner join shop_manage.shop on shop_manage.shop.id = o.shop_id
                        inner join shop_order.order_dispatch on shop_order.order_dispatch.order_no = o.order_no 
                        left join `order`.order_package_card c on o.produce_type = 15 and o.order_no = c.verify_order_no

                        where  o.`order_status` =30
                        and o.produce_type not in (1,14)
                        and op.`is_deleted` =0
                        and op.parent_order_package_pid=0
                        and op.`product_id` not like 'TR-%' 
                        and op.`product_id` not like 'MD-BY-LT-%' -- 轮胎
                        and op.`product_id` not like 'MD-BY-BT-%' -- 蓄电池
                        and op.`product_id` not like 'TG-XC-P-%' 
                        and op.`product_id` not like 'MF-XC-PX-%' -- 普洗
                        and op.`product_id` not like 'BYFW-SMFW-AQJC-%' 
                        and op.`product_id` not like 'FW-BY-JC-%'  -- 检测
                        and op.`product_id` not like 'BY-PJ-WP-%' 
                        and op.`product_id` not like 'BYFW-QTPJ-WP-%'  -- 雨刷

                        and shop_order.order_dispatch.is_deleted = 0
                        and shop_manage.shop.type in (2,4)
                        {condtion}
                    ) table1  {condtion2}   ";

            var sql = "";
            switch (request.ItemType)
            {
                case PerformanceItemType.PackageCard:
                    sql = sql_1;
                    break;
                case PerformanceItemType.NumItem:
                    sql = sql_2;
                    break;
                default:
                    sql = sql_3;
                    break;
            }
            var paras = new
            {
                employeeName = key,
                itemName = key2,
                startDate = request.StartDate.ToString("yyyy-MM-dd"),
                endDate = request.EndDate.AddDays(1).ToString("yyyy-MM-dd")
            };
            IEnumerable<TechPerformanceDetailDto> result = new List<TechPerformanceDetailDto>();
            OpenConnection(async conn =>
            {
                result = await conn.QueryAsync<TechPerformanceDetailDto>(sql + sortFiled, paras);
            }
            );
            return result.ToList();
        }


        /// <summary>
        /// 获取技师绩效列表V3
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDto>> GetTechPerformanceListV3(TechPerformanceRequest request)
        {
            var dateField = "";
            if (request.GroupType == PerformanceGroupType.GroupByAll)
            {
                dateField = " DATE_FORMAT(o.create_time,'%Y-%m-01') as InstallDate, ";
            }
            else
            {
                dateField = " DATE_FORMAT(o.create_time,'%Y-%m-%d') as InstallDate, ";
            }

            var condtion = "";
            var key = request.Mobile?.Trim();  //用手机号查询多个门店的绩效。
          
            condtion += "  and e.mobile = @Key and o.create_time >=@StartDate and o.create_time < @EndDate ";

            var groupFiled = " GROUP BY EmployeeName,InstallDate, ItemName ";
            var sortFiled = " ORDER BY EmployeeName,InstallDate, ItemName ";

            var sql_1 = $@"SELECT EmployeeName,InstallDate, ItemName, sum(num) as ItemNum, sum(amt) as ItemAmt, 0 as ItemJiXiao
                        FROM (
                            select  e.mobile as EmployeeName,o.order_no,
                            op.product_name as ItemName,{dateField}
                            (d.percent * op.total_number) as num,
                            (d.percent * op.total_amount) as amt
                            from `order`.`order` o
                            inner join  `order`.order_product op on o.id = op.order_id and op.is_deleted =0
                            inner join shop_manage.shop s on s.id = o.shop_id
                            inner join shop_order.order_dispatch d on d.order_no = o.order_no  and d.is_deleted = 0
							INNER JOIN shop_manage.employee e on e.id = d.tech_id
                            where  o.produce_type = 14
                            and o.order_status =30                            
                            and op.parent_order_package_pid=0
                            and s.type in (2,4) 
							and o.shop_id > 100
                            {condtion}
                            ) table1  {groupFiled} ";


            var sql_2 = "";

            var sql_3 = $@"SELECT EmployeeName,InstallDate, ItemName, sum(num) as ItemNum, sum(amt) as ItemAmt,
sum(case when config_type is null then 0 when config_type=1 then amt * config_point /100 when config_type=2 then config_point else 0 end) as ItemJiXiao
                    FROM (
                        SELECT  e.mobile as EmployeeName, 
                        case  when (o.`order_type` =2 ) then '保养'
when (o.`order_type` =3 ) then '美容' else  '维修'  end as ItemName, {dateField}
                        (d.percent * 1) as num,
                        (d.percent * (case when o.`shop_settlement_amount` - o.`shop_item_cost`< o.shop_install_amount then o.shop_install_amount
else o.`shop_settlement_amount` - o.`shop_item_cost` end)) as amt,
cfg.config_type,cfg.config_point
                        FROM 
                        fms.reconciliation_fee o
                        inner join shop_manage.shop s on s.id = o.shop_id
                        inner join shop_order.order_dispatch d on d.order_no = o.order_no  and d.is_deleted = 0
                        INNER JOIN shop_manage.employee e on e.id = d.tech_id
						LEFT JOIN product.service_type_enum enm on enm.id=o.order_type and enm.is_deleted = 0
						LEFT JOIN shop_manage.basic_performance_config cfg on cfg.shop_id = o.shop_id and cfg.service_type = enm.service_type and cfg.config_flag=1 and cfg.is_deleted = 0
                        where
                        o.is_deleted =0  
                        and o.shop_id > 100
                        and s.type in (2,4)                      
                        {condtion}

                        UNION ALL

                        select  e.mobile as EmployeeName,
                        '耗材领用' as ItemName, {dateField}
                        dt.`num` * -1 as num,
                        (dt.`cost_price` * dt.`num`* -1) as amt,
                        1 as config_type, 20 as config_point
                        from shop_wms.product_claim_record o
                        INNER JOIN shop_wms.tech_claim_product dt on dt.record_id = o.id and dt.is_deleted = 0
                        inner join shop_manage.shop s on s.id = o.shop_id
                        INNER JOIN shop_manage.employee e on e.id = o.tech_id
                        where o.is_deleted = 0 and o.shop_id > 100
                        and s.type in (2,4)                       
                        {condtion}
                    ) table1 {groupFiled} ";


            IEnumerable<TechPerformanceDto> result = new List<TechPerformanceDto>();

            var sql = "";
            switch (request.ItemType)
            {
                case PerformanceItemType.PackageCard:
                    sql = sql_1;
                    break;
                case PerformanceItemType.NumItem:
                    sql = sql_2;
                    return result.ToList();               
                default:
                    sql = sql_3;
                    break;
            }
            var paras = new
            {
                Key = key,
                StartDate = request.StartDate.ToString("yyyy-MM-dd"),
                EndDate = request.EndDate.AddDays(1).ToString("yyyy-MM-dd")
            };
            OpenConnection(async conn =>
            {
                result = await conn.QueryAsync<TechPerformanceDto>(sql + sortFiled, paras);
                foreach (var item in result)
                {
                    item.EmployeeId = request.EmployeeId;
                    item.EmployeeName = request.EmployeeName;
                    item.Mobile = request.Mobile;
                }
            }
            );
            return result.ToList();
        }


        /// <summary>
        /// 获取技师绩效明细V3
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetailV3(TechPerformanceDetailRequest request)
        {
            var condtion = "";
            var key = request.Mobile?.Trim();  //用手机号查询多个门店的绩效。

            condtion += "  and e.mobile = @Key and o.create_time >=@StartDate and o.create_time < @EndDate ";

            var condtion2 = " where 1=1 ";
            var key2 = request.ItemName?.Trim();
            if (!string.IsNullOrEmpty(key2))
            {
                condtion2 += " and ptype = @ItemName ";
            }

            var sortFiled = " ORDER BY OrderNo Desc ";

            var sql_1 = $@"SELECT EmployeeName,InstallDate,OrderNo, ItemName, (num) as ItemNum, (amt) as ItemAmt, 0 as ItemJiXiao
                        FROM (
                            select  e.name as EmployeeName,op.product_name as ptype,
                            op.product_name as ItemName,o.order_no as OrderNo,o.install_time as InstallDate,
                            (d.percent * op.total_number) as num,
                            (d.percent * op.total_amount) as amt
                            from `order`.`order` o
                            inner join  `order`.order_product op on o.id = op.order_id and op.is_deleted =0
                            inner join shop_manage.shop s on s.id = o.shop_id
                            inner join shop_order.order_dispatch d on d.order_no = o.order_no   and d.is_deleted = 0
							INNER JOIN shop_manage.employee e on e.id = d.tech_id
                            where  o.produce_type = 14
                            and o.order_status =30                   
                            and op.parent_order_package_pid=0
                            and s.type in (2,4) 
							and o.shop_id > 100
                            {condtion}
                            ) table1  {condtion2}  ";


            var sql_2 = " ";

            var sql_3 = $@"SELECT EmployeeName,InstallDate,OrderNo, ItemName, (num) as ItemNum, (amt) as ItemAmt,
                    (case when config_type is null then 0 when config_type=1 then amt * config_point /100 when config_type=2 then config_point else 0 end) as ItemJiXiao
                    FROM (
                        SELECT  e.mobile as EmployeeName, case  when (o.`order_type` =2 ) then '保养'
when (o.`order_type` =3 ) then '美容' else  '维修'  end as ptype,
                        o.product_detail as ItemName,o.order_no as OrderNo, o.create_time as InstallDate,
                        (d.percent * 1) as num,
                        (d.percent * (case when o.`shop_settlement_amount` - o.`shop_item_cost`< o.shop_install_amount then o.shop_install_amount
else o.`shop_settlement_amount` - o.`shop_item_cost` end)) as amt,
cfg.config_type,cfg.config_point
                        FROM 
                        fms.reconciliation_fee o
                        inner join shop_manage.shop s on s.id = o.shop_id
                        inner join shop_order.order_dispatch d on d.order_no = o.order_no  and d.is_deleted = 0
                        INNER JOIN shop_manage.employee e on e.id = d.tech_id
						LEFT JOIN product.service_type_enum enm on enm.id=o.order_type and enm.is_deleted = 0
						LEFT JOIN shop_manage.basic_performance_config cfg on cfg.shop_id = o.shop_id and cfg.service_type = enm.service_type and cfg.config_flag=1 and cfg.is_deleted = 0
                        
                        where
                        o.is_deleted =0  
                        and o.shop_id > 100
                        and s.type in (2,4)                      
                        {condtion}

                        UNION ALL

                        select  e.mobile as EmployeeName,'耗材领用' as ptype,
                        dt.product_name as ItemName,o.id as OrderNo, o.create_time as InstallDate,
                        dt.`num` * -1 as num,
                        (dt.`cost_price` * dt.`num`* -1) as amt,
                        1 as config_type, 20 as config_point
                        from shop_wms.product_claim_record o
                        INNER JOIN shop_wms.tech_claim_product dt on dt.record_id = o.id and dt.is_deleted = 0
                        inner join shop_manage.shop s on s.id = o.shop_id
                        INNER JOIN shop_manage.employee e on e.id = o.tech_id
                        where o.is_deleted = 0 and o.shop_id > 100
                        and s.type in (2,4)    
                        {condtion}
                    ) table1  {condtion2}   ";

            IEnumerable<TechPerformanceDetailDto> result = new List<TechPerformanceDetailDto>();
            var sql = "";
            switch (request.ItemType)
            {
                case PerformanceItemType.PackageCard:
                    sql = sql_1;
                    break;
                case PerformanceItemType.NumItem:
                    sql = sql_2;
                    return result.ToList();
                default:
                    sql = sql_3;
                    break;
            }
            var paras = new
            {
                Key = key,
                ItemName = key2,
                StartDate = request.StartDate.ToString("yyyy-MM-dd"),
                EndDate = request.EndDate.AddDays(1).ToString("yyyy-MM-dd")
            };
            OpenConnection(async conn =>
            {
                result = await conn.QueryAsync<TechPerformanceDetailDto>(sql + sortFiled, paras);
                foreach (var item in result)
                {
                    item.EmployeeId = request.EmployeeId;
                    item.EmployeeName = request.EmployeeName;
                    item.Mobile = request.Mobile;
                }
            }
            );
            return result.ToList();

        }


        /// <summary>
        /// 获取技师绩效列表V4
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDto>> GetTechPerformanceListV4(TechPerformanceRequest request)
        {
            var dateField = "";
            if (request.GroupType == PerformanceGroupType.GroupByAll)
            {
                dateField = " DATE_FORMAT(install_time,'%Y-%m-01') as InstallDate, ";
            }
            else
            {
                dateField = " DATE_FORMAT(install_time,'%Y-%m-%d') as InstallDate, ";
            }

            var condtion = "";
            var key = request.EmployeeName?.Trim();  //用查询多个门店的绩效。

            if (!string.IsNullOrEmpty(key))
            {
                condtion += " and ( employee_name = @Key ) ";
            }
            condtion += " and create_time >=@StartDate and create_time < @EndDate ";

            if (request.ItemType >= 0)
            {
                condtion += " and ( `tab_type` = @ItemType ) ";
            }

            var groupFiled = " GROUP BY EmployeeName,InstallDate, ItemName ";
            var sortFiled = " ORDER BY EmployeeName,InstallDate, ItemName ";

            string fieldnum = request.ItemType != PerformanceItemType.AmtItem ? "sum(num) as ItemNum," : "count(DISTINCT order_no)  as ItemNum,";
            var sql = $@"SELECT EmployeeName,InstallDate, ItemName, {fieldnum} sum(amt) as ItemAmt, sum( total_point ) as ItemJiXiao
                        FROM (
                            select  employee_name as EmployeeName,order_no,
                            case order_type when 0 then '未设置' when 1 then '轮胎' when 2 then '保养' when 3 then '美容' when 4 then '钣金喷漆' when 5 then '维修改装' else '其他' end 
                                as ItemName,{dateField}
                            percent, (total_number * percent) as num,
                            (case when maoli > settlement_amount then maoli else settlement_amount end * percent) as amt,total_point
                            from `shop_order`.`employee_performance_order` p
                            where  p.is_deleted = 0 and p.shop_id > 4460
                                    and p.create_time>'2025-01-01'
                            {condtion}
                            ) table1  {groupFiled} ";


            IEnumerable<TechPerformanceDto> result = new List<TechPerformanceDto>();

            var paras = new
            {
                Key = key,
                ItemType = request.ItemType == PerformanceItemType.AmtItem ? 3 : (int)request.ItemType,
                StartDate = request.StartDate.ToString("yyyy-MM-dd"),
                EndDate = request.EndDate.AddDays(1).ToString("yyyy-MM-dd")
            };
            await OpenConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<TechPerformanceDto>(sql + sortFiled, paras);
                if (result !=  null && result.Any()) {
                    foreach (var item in result)
                    {
                        item.EmployeeId = request.EmployeeId;
                        item.EmployeeName = request.EmployeeName;
                        item.Mobile = request.Mobile;
                    }
                }
                
            });
            return result.ToList();
        }


        /// <summary>
        /// 获取技师绩效明细V4
        /// </summary>
        /// <returns></returns>
        public async Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetailV4(TechPerformanceDetailRequest request)
        {
            var condtion = "";
            var key = request.EmployeeName?.Trim();  //用手机号查询多个门店的绩效。

            if (!string.IsNullOrEmpty(key))
            {
                condtion += " and ( employee_name = @Key ) ";
            }
            condtion += " and create_time >=@StartDate and create_time < @EndDate ";

            if (request.ItemType >= 0)
            {
                condtion += " and ( `tab_type` = @ItemType ) ";
            }

            var condtion2 = " where 1=1 ";
            var key2 = request.ItemName?.Trim();
            if (!string.IsNullOrEmpty(key2))
            {
                condtion2 += " and ptype = @ItemName ";
            }

            var groupFiled = " GROUP BY EmployeeName,OrderNo ";
            var sortFiled = " ORDER BY OrderNo Desc ";

            var sql = $@"SELECT EmployeeName,max(InstallDate) as InstallDate,OrderNo, (ItemName), sum(num) as ItemNum, sum(amt) as ItemAmt, sum(total_point) as ItemJiXiao
                        FROM (
                            select  employee_name as EmployeeName,
                            case order_type when 0 then '未设置' when 1 then '轮胎' when 2 then '保养' when 3 then '美容' when 4 then '钣金喷漆' when 5 then '维修改装' else '其他' end 
                                as ptype,
                            product_name as ItemName,order_no as OrderNo,install_time as InstallDate,
                            percent, (total_number * percent) as num,
                            (case when maoli > settlement_amount then maoli else settlement_amount end * percent) as amt,total_point
                            from `shop_order`.`employee_performance_order` p
                            where  p.is_deleted = 0 and p.shop_id > 4460
                                    and p.create_time>'2025-01-01'
                            {condtion}
                            ) table1  {condtion2}  ";


            IEnumerable<TechPerformanceDetailDto> result = new List<TechPerformanceDetailDto>();

            //先返回空，查询会超时。
            //return result.ToList();

            var paras = new
            {
                Key = key,
                ItemName = key2,
                ItemType = request.ItemType == PerformanceItemType.AmtItem ? 3 : (int)request.ItemType,
                StartDate = request.StartDate.ToString("yyyy-MM-dd"),
                EndDate = request.EndDate.AddDays(1).ToString("yyyy-MM-dd")
            };
            await OpenConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<TechPerformanceDetailDto>(sql + groupFiled + sortFiled, paras);
                if (result != null && result.Any())
                {
                    foreach (var item in result)
                    {
                        item.EmployeeId = request.EmployeeId;
                        item.EmployeeName = request.EmployeeName;
                        item.Mobile = request.Mobile;
                    }
                }
            });
            return result.ToList();

        }

    }
}

