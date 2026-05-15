var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly((typeof(Program).Assembly));
builder.Services.AddCarter();
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();