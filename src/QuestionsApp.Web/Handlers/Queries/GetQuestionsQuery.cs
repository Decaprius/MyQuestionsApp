namespace QuestionsApp.Web.Handlers.Queries;

using MediatR;

public class GetQuestionsQuery : IRequestHandler<GetQuestionsRequest, List<GetQuestionsResponse>>
{
    public Task<List<GetQuestionsResponse>> Handle(GetQuestionsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
