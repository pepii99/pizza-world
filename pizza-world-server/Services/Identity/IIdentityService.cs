using pizza_hub.Dtos.User;
using pizza_hub.Data.Models.Identity;
using pizza_hub.Data.Models.ServiceFactory;

namespace pizza_hub.Services.Identity;

public interface IIdentityService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);

    Task<ServiceResponse<GetUserDto>> Register(RegisterRequestModel model);

    Task<ServiceResponse<List<GetUserDto>>> GetAll();

    Task<ApplicationUser> GetById(string id);

    string GetUserId();
}
