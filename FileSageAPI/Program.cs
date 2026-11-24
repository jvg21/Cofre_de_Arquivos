using FileSageAPI.Database.AppDbContext;
using FileSageAPI.Database.Models;
using FileSageAPI.Entities.Archive.Interfaces;
using FileSageAPI.Entities.Archive.Layers;
using FileSageAPI.Entities.Author.DTOs;
using FileSageAPI.Entities.Author.Interfaces;
using FileSageAPI.Entities.Author.Layers;
using FileSageAPI.Entities.Book.DTOs;
using FileSageAPI.Entities.Book.Interfaces;
using FileSageAPI.Entities.Book.Layers;
using FileSageAPI.Entities.Tag.DTOs;
using FileSageAPI.Entities.Tag.Interfaces;
using FileSageAPI.Entities.Tag.Layers;
using FileSageAPI.Entities.Url.DTOs;
using FileSageAPI.Entities.Url.Interfaces;
using FileSageAPI.Entities.Url.Layers;
using FileSageAPI.Middleware;
using Mapster;
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


builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IBookService,BookService>();

//builder.Services.AddScoped<IArchiveTagRepository, ArchiveTagRepository>();
//builder.Services.AddScoped<IArchiveTagService, ArchiveTagService>();


//builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
//builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();



#endregion


# region MapsterConfig
//TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
//TypeAdapterConfig<UrlUpdateDTO, UrlModel>.NewConfig().IgnoreNullValues(true).Ignore(dest => dest.Id);
//TypeAdapterConfig<TagUpdateDTO, TagModel>.NewConfig().IgnoreNullValues(true).Ignore(dest => dest.Id);
//TypeAdapterConfig<BookInsertDTO, Book>.NewConfig().Ignore(dest => dest.Authors);
//TypeAdapterConfig<BookUpdateDTO, BookModel>.NewConfig().IgnoreNullValues(true).Ignore(dest => dest.ArchiveId).Ignore(dest => dest.Authors);
//TypeAdapterConfig<AuthorUpdateDTO, AuthorModel>.NewConfig().IgnoreNullValues(true).Ignore(dest => dest.ArchiveId).Ignore(dest => dest.Books);
//TypeAdapterConfig<CatalogItemUpdateDTO, CatalogItem>.NewConfig().IgnoreNullValues(true).Ignore(dest => dest.Id);

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
