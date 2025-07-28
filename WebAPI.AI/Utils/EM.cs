namespace WebAPI.AI.Utils;

/// <summary>
/// Contains error messages.
/// </summary>
internal class EM
{
    public static string ChatClientNotType(Type type) =>
        $"The chat client is not of type {type.Name}. Please ensure you are using the correct client type.";
}
