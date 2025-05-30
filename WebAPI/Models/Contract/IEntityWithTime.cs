namespace WebAPI.Models.Contract;

public interface IEntityWithTime<T>
{
    T Id { get; set; }
    DateTimeOffset CreationDate { get; set; }
}
