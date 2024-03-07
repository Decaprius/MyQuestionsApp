using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionsApp.Web.DB;

namespace QuestionsApp.Web.Handlers.Queries;

public class GetQuestionsQuery : IRequestHandler<GetQuestionsRequest, List<GetQuestionsResponse>>
{
    private readonly QuestionsContext _context;

    public GetQuestionsQuery(QuestionsContext context)
    {
        _context = context;
    }

    public async Task<List<GetQuestionsResponse>> Handle(GetQuestionsRequest request,
        CancellationToken cancellationToken)
    {
        return await (from q in _context.Questions
                select new GetQuestionsResponse { Id = q.Id, Content = q.Content, Votes = q.Votes.Count() })
            .ToListAsync(cancellationToken);
    }
}