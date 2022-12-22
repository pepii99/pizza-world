namespace pizza_hub.Dtos.Pizza;

public class GetPizzaDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsGlutenFree { get; set; }
}
