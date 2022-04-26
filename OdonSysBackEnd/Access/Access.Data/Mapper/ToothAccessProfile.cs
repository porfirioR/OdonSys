using Access.Contract;
using AutoMapper;
using Sql.Entities;

namespace Access.Data.Mapper
{
    public class ToothAccessProfile : Profile
    {
        public ToothAccessProfile()
        {
            CreateMap<Tooth, ToothAccessResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
