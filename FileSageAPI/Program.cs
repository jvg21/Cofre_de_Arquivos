using FileSageAPI.Database.AppDbContext;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Archive.Layers;
using FileSageAPI.Entities.Tag.Interfaces;
using FileSageAPI.Entities.Tag.Layers;
using FileSageAPI.Entities.Url;
using FileSageAPI.Entities.Url.Interfaces;
using FileSageAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FILE API",
        Version = "v1",
        Description = "API para gerenciamento de arquivos"
    });
});

#endregion

#region Database Connection

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(
//        Environment.GetEnvironmentVariable("DB_CONN")
//    )
//);
#endregion

#region Dependency Injection

builder.Services.AddScoped<ITagRepository, TagRepository>();
////builder.Services.AddScoped<ITagValidator, TagValidator>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<IArchiveRepository, ArchiveRepository>();
builder.Services.AddScoped<IArchiveService, ArchiveService>();

builder.Services.AddScoped<IUrlRepository, UrlRepository>();
//builder.Services.AddScoped<IUrlValidator, UrlValidator>();
builder.Services.AddScoped<IUrlService, UrlService>();

//builder.Services.AddScoped<IArchiveTagRepository, ArchiveTagRepository>();
//builder.Services.AddScoped<IArchiveTagService, ArchiveTagService>();


//builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
//builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();



#endregion


# region CORS
var corsPolicyName = "DefaultCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        //policy.WithOrigins("*")
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
        // .AllowCredentials(); // só se precisar enviar cookies/auth headers, e nesse caso não pode usar AllowAnyOrigin

    });
});

#endregion

//builder.Services.AddOpenApi();

var app = builder.Build();

#region Development

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FILE API v1");
    });
}

#endregion
app.UseMiddleware<RequestExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("DefaultCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
