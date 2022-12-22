using AutoMapper;
using pizza_hub.Data.Models.Identity;
using pizza_hub.Data.Models.Pizza;
using pizza_hub.Dtos.Pizza;
using pizza_hub.Dtos.User;
using pizza_hub.Models.Pizzas;

namespace pizza_hub;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pizza, GetPizzaDto>();
        CreateMap<GetPizzaDto, Pizza>();

        CreateMap<Pizza, CreatePizzaRequestModel>();
        CreateMap<CreatePizzaRequestModel, Pizza>();

        CreateMap<ApplicationUser, GetUserDto>();
        CreateMap<GetUserDto, ApplicationUser>();
    }
}
