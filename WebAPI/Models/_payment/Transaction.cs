using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._payment;

[Table("Transactions")]
public class Transaction : IEntityWithTime<int>
{
    public int Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string PaymentIntentId { get; set; } = string.Empty;

    public int CustomerId { get; set; }
    public User Customer { get; set; } = default!;

    public int ProductId { get; set; }
    public required string ProductType { get; set; }

    /// <summary>
    /// One of the following values: <see cref="PaymentIntentStatus"/>
    /// </summary>
    public required string Status { get; set; }
}
