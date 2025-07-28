namespace WebAPI.AI.Extensions;

public static class ObjectExtensions
{
    public static TTarget OfType<TTarget>(this object obj)
    {
        return (TTarget)obj;
    }
}