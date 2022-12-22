namespace pizza_hub.Data.Models.ServiceFactory;

public static class ServiceResponseFactory<T>
{
    public static ServiceResponse<T> CreateServiceResponse()
    {
        return new ServiceResponse<T>();
    }
}
