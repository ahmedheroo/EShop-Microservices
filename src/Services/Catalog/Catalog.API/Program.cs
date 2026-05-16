using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(assembly);
    opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();