using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementDAL.Entities;
using GymManagementSystemBLL.View_Models.SessionVm;

namespace GymManagementBLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.CategoryName)
                )
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
        }
    }
}
