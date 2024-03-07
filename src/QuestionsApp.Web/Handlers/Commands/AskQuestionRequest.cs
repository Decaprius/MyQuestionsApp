using MediatR;

namespace QuestionsApp.Web.Handlers.Commands;

public class AskQuestionRequest : IRequest<IResult>
{
    public string Content { get; set; } = "";
}