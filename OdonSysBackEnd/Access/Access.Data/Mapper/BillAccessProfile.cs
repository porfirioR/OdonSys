using Access.Contract.Bills;
using Access.Sql.Entities;
using AutoMapper;

namespace Access.Data.Mapper
{
    public class BillAccessProfile : Profile
    {
        public BillAccessProfile()
        {
            CreateMap<HeaderBillAccessRequest, HeaderBill>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<BillDetailAccessRequest, BillDetail>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
