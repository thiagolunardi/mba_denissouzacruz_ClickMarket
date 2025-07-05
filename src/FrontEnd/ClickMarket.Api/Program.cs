using ClickMarket.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .AddDatabaseSelector()
    .AddApiConfig()
    .RegisterServices()
    .AddIdentityConfig()
    .AddJwtConfig()
    .AddSwaggerConfig();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.UseDbMigrationHelper();
app.Run();

