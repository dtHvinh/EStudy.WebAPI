using Riok.Mapperly.Abstractions;
using WebAPI.AI.JsonSchema;

namespace WebAPI.Endpoints.AIEndpoints.GetSentenceFeedback;

[Mapper]
public static partial class Mapper
{
    public static partial GetSentenceFeedbackResponse ToResponse(this SentenceFeedbackResponse response);
}
