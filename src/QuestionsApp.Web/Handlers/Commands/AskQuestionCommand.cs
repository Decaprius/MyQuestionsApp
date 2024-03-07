using MediatR;

namespace QuestionsApp.Web.Handlers.Commands;

public class AskQuestionCommand : IRequestHandler<AskQuestionRequest, IResult>
{
    public Task<IResult> Handle(AskQuestionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}