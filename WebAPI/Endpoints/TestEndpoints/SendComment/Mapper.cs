using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.SendComment;

[Mapper]
public static partial class Mapper
{
    [MapProperty(nameof(SendCommentRequest.Text), nameof(TestComment.Text))]
    [MapValue(nameof(TestComment.CreationDate), Use = nameof(Now))]
    public static partial TestComment ToComment(this SendCommentRequest request, string authorId);

    [MapProperty(nameof(TestComment.Text), nameof(SendCommentRequest.Text))]
    public static partial SendCommentResponse ToResponse(this TestComment comment);

    private static DateTimeOffset Now() => DateTimeOffset.UtcNow;
}
