using WebAPI.Models._others;

namespace WebAPI.Models.Contract;

public interface IBelongToUser<T>
{
    T AuthorId { get; set; }
    User Author { get; set; }
}
