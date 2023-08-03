using AutoMapper;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Imp.Mappers
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Purchase, PurchaseInfo>().ReverseMap();
        }
    }
}
