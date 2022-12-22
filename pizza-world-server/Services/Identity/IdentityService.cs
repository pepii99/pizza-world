using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pizza_hub.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using pizza_hub.Data;
using pizza_hub.Dtos.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pizza_hub.Data.Models.Identity;
using pizza_hub.Data.Models.ServiceFactory;

namespace pizza_hub.Services.Identity;

public class IdentityService : IIdentityService
{

    private readonly AppSettings _appSettings;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IOptions<AppSettings> appSettings, ApplicationDbContext context,
        UserManager<ApplicationUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
            _appSettings = appSettings.Value;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
    {
        var pizzas = await _context.Pizzas.ToListAsync();
        var users = await _context.Users.ToListAsync();
        var usersDto = users.Select(p => _mapper.Map<GetUserDto>(p)).ToList();
        return new ServiceResponse<List<GetUserDto>> { Data = usersDto };
    }
    
    public async Task<ApplicationUser> GetById(string id)
    {
        //var response = ServiceResponseFactory<GetUserDto>.CreateServiceResponse();
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user;

        //if (user == null)
        //{
        //    response.Message = "User doesn't exist";
        //}

        //response.Data = _mapper.Map<GetUserDto>(user);

    }

    public string GetUserId()
    {
        var user = (ApplicationUser)_httpContextAccessor.HttpContext.Items["User"];
        return user.Id;
    }


    public async Task<ServiceResponse<GetUserDto>> Register(RegisterRequestModel model)
    {
        var response = ServiceResponseFactory<GetUserDto>.CreateServiceResponse();

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Password = model.Password,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            response.Data = _mapper.Map<GetUserDto>(user);
            return response;
        }

        return response;

    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim("id", user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
