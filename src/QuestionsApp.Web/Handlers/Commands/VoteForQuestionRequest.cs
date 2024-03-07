using MediatR;

namespace QuestionsApp.Web.Handlers.Commands;

public class VoteForQuestionRequest : IRequest<IResult>
{
    public int QuestionId { get; set; }
}