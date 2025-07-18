namespace WebAPI.Models.Contract;

public interface IPurchasable<TKey>
{
    TKey Id { get; set; }
    decimal Price { get; set; }
}
