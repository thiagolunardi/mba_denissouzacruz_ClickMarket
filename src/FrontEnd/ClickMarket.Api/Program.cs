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

// Serve arquivos estaticos da própria aplicação (wwwroot padrão)
app.UseStaticFiles();

// Serve imagens enviadas (diretório configurado em appsettings)
var imagensPath = Path.Combine(builder.Configuration["Parametros:DiretorioBaseImagemProduto"], "wwwroot");
if (Directory.Exists(imagensPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(imagensPath),
        RequestPath = ""
    });
}

app.UseCors("Total");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.UseDbMigrationHelper();
app.Run();

