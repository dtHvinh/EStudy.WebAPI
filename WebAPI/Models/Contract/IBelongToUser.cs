namespace WebAPI.Models.Contract;

public interface IBelongToUser<T>
{
    T AuthorId { get; set; }
    User Author { get; set; }

    bool IsBelongTo(string userId)
    {
        return Author != null && Author.Id.ToString() == userId;
    }
}
