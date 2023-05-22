using Access.Contract.Invoices;
using Access.Sql.Entities;
using AutoMapper;

namespace Access.Data.Mapper
{
    public class InvoiceAccessProfile : Profile
    {
        public InvoiceAccessProfile()
        {
            CreateMap<InvoiceAccessRequest, Invoice>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<InvoiceDetailAccessRequest, InvoiceDetail>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

        }
    }
}
