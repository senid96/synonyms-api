using Synonyms_API.Services;

var allowedOrigins = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);
// Register services
builder.Services.AddSingleton<ISynonym, SynonymService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                             "http://www.todo-prod-one.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod(); ;
                      });
});

var app = builder.Build();

app.UseCors(allowedOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
