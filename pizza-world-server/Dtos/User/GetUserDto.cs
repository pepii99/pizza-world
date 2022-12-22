using pizza_hub.Dtos.Pizza;

namespace pizza_hub.Dtos.User;

public class GetUserDto
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public ICollection<GetPizzaDto> PIzzas { get; set; }
}
