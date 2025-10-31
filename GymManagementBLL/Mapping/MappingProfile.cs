using AutoMapper;
using GymManagementBLL.View_Models.MemberVM;
using GymManagementBLL.View_Models.PlanVM;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementBLL.View_Models.TrainerVM;
using GymManagementDAL.Entities;
using GymManagementSystemBLL.View_Models.SessionVm;

namespace GymManagementBLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapSession();
            MapMember();
            MapPlan();
            MapTrainer();
        }

        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(
                    dest => dest.TrainerName,
                    options => options.MapFrom(src => src.Trainer.Name)
                )
                .ForMember(
                    dest => dest.CategoryName,
                    options => options.MapFrom(src => src.Category.CategoryName)
                )
                .ForMember(dest => dest.AvailableSlots, options => options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();

            CreateMap<Trainer, TrainerSelectViewModel>();

            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.CategoryName));
        }

        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(
                    dest => dest.Adress,
                    opt =>
                        opt.MapFrom(src => new Adress
                        {
                            BuildingNumber = src.BuildingNumber,
                            Street = src.Street,
                            City = src.City,
                        })
                );

            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<Member, MemberViewModel>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString())
                )
                .ForMember(
                    dest => dest.Address,
                    opt =>
                        opt.MapFrom(src =>
                            $"{src.Adress.BuildingNumber}-{src.Adress.Street}-{src.Adress.City}"
                        )
                );

            #region Second Way
            //CreateMap<CreateMemberViewModel, Member>()
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));

            //CreateMap<CreateMemberViewModel, Address>()
            //    .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            //    .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));
            #endregion

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(
                    dest => dest.BuildingNumber,
                    opt => opt.MapFrom(src => src.Adress.BuildingNumber)
                )
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Adress.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Adress.City));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap(
                    (src, dest) =>
                    {
                        dest.Adress.BuildingNumber = src.BuildingNumber;
                        dest.Adress.Street = src.Street;
                        dest.Adress.City = src.City;
                        dest.UpdatedAt = DateTime.Now;
                    }
                );
        }

        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<PlanToUpdateViewModel, Plan>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }

        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(
                    dest => dest.Adress,
                    opt =>
                        opt.MapFrom(src => new Adress
                        {
                            BuildingNumber = src.BuildingNumber,
                            Street = src.Street,
                            City = src.City,
                        })
                );

            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString())
                )
                .ForMember(
                    dest => dest.Address,
                    opt =>
                        opt.MapFrom(src =>
                            $"{src.Adress.BuildingNumber}-{src.Adress.Street}-{src.Adress.City}"
                        )
                );
            ;

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Adress.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Adress.City))
                .ForMember(
                    dest => dest.BuildingNumber,
                    opt => opt.MapFrom(src => src.Adress.BuildingNumber)
                );

            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap(
                    (src, dest) =>
                    {
                        dest.Adress.BuildingNumber = src.BuildingNumber;
                        dest.Adress.City = src.City;
                        dest.Adress.Street = src.Street;
                        dest.UpdatedAt = DateTime.Now;
                    }
                );
        }
    }
}
