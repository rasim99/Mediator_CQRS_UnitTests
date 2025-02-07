namespace Business.Services.Producer
{
    public interface IProducerService
    {
        Task ProduceAsync(string action,object data);
    }
}
