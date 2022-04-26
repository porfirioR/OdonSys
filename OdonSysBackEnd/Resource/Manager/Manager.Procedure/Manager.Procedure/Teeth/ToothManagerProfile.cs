using Access.Contract;
using AutoMapper;
using Contract.Procedure.Teeth;

namespace Manager.Procedure.Teeth
{
    public class ToothManagerProfile : Profile
    {
        public ToothManagerProfile()
        {
            CreateMap<ToothAccessResponse, ToothModel>();
        }
    }
}
