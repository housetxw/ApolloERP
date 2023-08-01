using AutoMapper;
using Ae.BaoYang.Service.Core.Model.Config;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.BaoYang.Service.Client.Response;
using Ae.BaoYang.Service.Core.Response;

namespace Ae.BaoYang.Service.Imp.Mappers
{
    public class BaoYangMapperProfile : Profile
    {
        public BaoYangMapperProfile()
        {
            CreateMap<BaoYangPackageConfigDO, BaoYangPackageConfigVo>();
            CreateMap<BaoYangTypeConfigDO, BaoYangTypeConfig>();

            CreateMap<InstallServiceDetailDto, InstallServiceDetailVo>();
            CreateMap<ProductRelateServiceDto, ProductRelateServiceVo>();
        }
    }
}
