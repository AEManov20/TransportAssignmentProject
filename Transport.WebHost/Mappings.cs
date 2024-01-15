﻿using AutoMapper;
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

        CreateMap<User, UserViewModel>();
        CreateMap<Vehicle, VehicleViewModel>();
        CreateMap<Driver, DriverViewModel>();
        CreateMap<UserReview, UserReviewViewModel>();
        CreateMap<RideStop, RideStopViewModel>();
        CreateMap<Ride, RideViewModel>()
            .ForMember(
                x => x.RideStops, 
                opt => opt.MapFrom(y => y.RideStops.Select(x => new RideStopViewModel()
                {
                    AddressText = x.AddressText,
                    OrderingNumber = x.OrderingNumber
                })));
    }
}