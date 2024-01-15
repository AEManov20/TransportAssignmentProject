using AutoMapper;
using Transport.BusinessLogic.Models;
using Transport.Data.Models;

namespace Transport.WebHost;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<UserInputModel, User>();
        CreateMap<UserReviewInputModel, UserReview>();
        CreateMap<VehicleInputModel, Vehicle>();
        //CreateMap<RideStopInputModel, RideStop>();
        //CreateMap<RideInputModel, Ride>();

        CreateMap<RideViewModel, RideInputModel>();
        CreateMap<RideStopViewModel, RideStopInputModel>();
        CreateMap<UserViewModel, UserInputModel>();
        CreateMap<VehicleViewModel, VehicleInputModel>();
        CreateMap<UserReviewViewModel, UserReviewInputModel>();

        CreateMap<User, UserViewModel>();
        CreateMap<Vehicle, VehicleViewModel>();
        CreateMap<Driver, DriverViewModel>();
        CreateMap<UserReview, UserReviewViewModel>();
        CreateMap<RideStop, RideStopViewModel>();
        CreateMap<Ride, RideViewModel>()
            .ForMember(
                x => x.RideStops, 
                opt => opt.MapFrom(y => y.RideStops));
    }
}