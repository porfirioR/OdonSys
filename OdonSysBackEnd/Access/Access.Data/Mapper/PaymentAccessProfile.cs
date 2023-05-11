using Access.Contract.Payments;
using Access.Sql.Entities;
using AutoMapper;

namespace Access.Data.Mapper
{
    public class PaymentAccessProfile : Profile
    {
        public PaymentAccessProfile()
        {
            CreateMap<PaymentAccessRequest, Payment>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new Guid(src.UserId)))
                .ForMember(dest => dest.HeaderBillId, opt => opt.MapFrom(src => new Guid(src.HeaderBillId)));
        }
    }
}
