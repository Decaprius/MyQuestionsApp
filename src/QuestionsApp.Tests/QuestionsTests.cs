using Microsoft.EntityFrameworkCore;
using QuestionsApp.Web.DB;

namespace QuestionsApp.Tests;

using FluentAssertions;
using QuestionsApp.Web.Handlers.Commands;
using QuestionsApp.Web.Handlers.Queries;

public class QuestionsTests
{
    
    private readonly QuestionsContext _context;

    public QuestionsTests()
    {
        var options = new DbContextOptionsBuilder<QuestionsContext>().
            UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = new QuestionsContext(options);
    }
    
    private GetQuestionsQuery GetQuestionsQueryHandler => new(_context);
    private AskQuestionCommand AskQuestionCommandHandler => new(_context, null);
    private VoteForQuestionCommand VoteForQuestionCommandHandler => new(_context, null);
    
    
    
    [Fact]
    public async void Empty()
    {
        var response = await GetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().BeEmpty();
    }
    
    [Fact]
    public async void OneQuestion()
    {
        var askResponse = await AskQuestionCommandHandler.Handle(new AskQuestionRequest { Content = "Dummy Question" }, default);
        askResponse.Should().NotBeNull();

        var response = await GetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().HaveCount(1);
    }
    
    [Fact]
    public async void OneQuestionAndVote()
    {
        var askResponse = await AskQuestionCommandHandler.Handle(new AskQuestionRequest { Content = "Dummy Question" }, default);
        askResponse.Should().NotBeNull();

        var response = await GetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().HaveCount(1);
        response[0].Votes.Should().Be(0);

        var voteResponse = await VoteForQuestionCommandHandler.Handle(new VoteForQuestionRequest { QuestionId = response[0].Id }, default);
        voteResponse.Should().NotBeNull();

        response = await GetQuestionsQueryHandler.Handle(new GetQuestionsRequest(), default);
        response.Should().HaveCount(1);
        response[0].Votes.Should().Be(1);
    }
}