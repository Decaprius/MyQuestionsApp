namespace QuestionsApp.Web.Handlers.Queries;

public class GetQuestionsResponse
{
    public int Id { get; set; }
    public string Content { get; set; } = "";
    public int Votes { get; set; }
}