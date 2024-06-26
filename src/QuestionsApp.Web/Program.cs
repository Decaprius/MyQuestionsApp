using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuestionsApp.Web.DB;
using QuestionsApp.Web.Handlers.Commands;
using QuestionsApp.Web.Handlers.Queries;
using QuestionsApp.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
// Configuration for Entity Framework
var connectionString = new SqliteConnectionStringBuilder() { DataSource = "Production.db" }.ToString();
builder.Services.AddDbContext<QuestionsContext>(x => x.UseSqlite(connectionString));
// Configuration for SignalR
builder.Services.AddSignalR();


var app = builder.Build();

// Make sure, that the database exists
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<QuestionsContext>().Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// activate static files serving (before the Map...)
app.UseDefaultFiles();
app.UseStaticFiles();

// Queries
app.MapGet("api/queries/questions", async (IMediator mediator) 
    => await mediator.Send(new GetQuestionsRequest()));

// Commands
app.MapPost("api/commands/questions/", async (IMediator mediator, string content) 
    => await mediator.Send(new AskQuestionRequest { Content = content }));

app.MapPost("api/commands/questions/{id:int}/vote", async (IMediator mediator, int id) 
    => await mediator.Send(new VoteForQuestionRequest { QuestionId = id }));

// Activate SignalR Hub
app.MapHub<QuestionsHub>("/hub");

app.Run();