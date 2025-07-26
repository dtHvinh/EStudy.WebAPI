namespace WebAPI.Constants;

public static class MessageTemplates
{
    public static string UserIsBanned(DateTimeOffset dueDate)
        => $"You are banned until {dueDate:yyyy-MM-dd HH:mm:ss}. Please contact support for more information.";
}
