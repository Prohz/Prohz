using AutoMapper;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;

namespace KopkeHome_BusinessLayer
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}
