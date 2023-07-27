using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Core.CommonModel;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Response;
using RoleType = Ae.Account.Api.Client.Response.RoleType;

namespace Ae.Account.Api.Imp.Services
{
    public class CompanyService : ICompanyService
    {

        #region 变量和构造器

        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly ICompanyClient companyClient;

        private readonly string shopCollName = "虚拟门店系统(仅限于当前系统)";

        private readonly string companyCollName = "虚拟公司系统(仅限于当前系统)";

        public CompanyService(IMapper mapper, IConfiguration configuration, ICompanyClient companyClient)
        {
            this.mapper = mapper;
            this.configuration = configuration;

            this.companyClient = companyClient;

            var tmpShopName = this.configuration["BizConfig:ShopCollectionAlias"];
            if (tmpShopName.IsNotNullOrWhiteSpace()) shopCollName = tmpShopName;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<List<OrganizationSel>> GetAllOrganizationSelectListAsync()
        {
            var resDto = await companyClient.GetAllOrganizationListAsync();
            var res = mapper.Map<List<OrganizationSel>>(resDto);
            res.ForEach(f =>
            {
                f.Id = string.Join(CommonConstant.SeparatorVertical, f.Id, f.Type);
            });
            return res;
        }

        public async Task<List<OrganizationSel>> GetAllOrganizationExceptShopSelectListAsync()
        {
            var resDto = await companyClient.GetAllOrganizationListAsync();
            var excShopDto = resDto.FindAll(r => r.Type != CommonConstant.One);

            //虚拟门店系统(仅限于当前系统)
            excShopDto.Insert(CommonConstant.Zero, new UnitDTO
            {
                Id = CommonConstant.Zero,
                Name = shopCollName,
                Type = (int)RoleType.Shop,
                Status = CommonConstant.Zero
            });


            var res = mapper.Map<List<OrganizationSel>>(excShopDto);
            res.ForEach(f =>
            {
                f.Id = string.Join(CommonConstant.SeparatorVertical, f.Id, f.Type);
            });
            return res;
        }


        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法
    }
}
