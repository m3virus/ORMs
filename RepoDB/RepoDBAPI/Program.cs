using RepoDb;
using RepoDBAPI;
using Scalar.AspNetCore;

public partial class Program
{
    [Obsolete]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        SqliteBootstrap.Initialize();
        IConfiguration config = builder.Configuration;
        var context = new DbContext(config);
        using var dataBase = context.AppDbContext;
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("My API");
                options.WithOpenApiRoutePattern("/openapi/v1.json");
            });
        }
        app.MapPost("/Insert", async (User x) => {
            var a = await dataBase.InsertAsync(x);
            return a.ToString();
        });

        app.MapGet("/Get/{id}", async (int id) => {
            var a = (await dataBase.QueryAsync<User>(id)).FirstOrDefault();
            return a;
        });

        app.MapDelete("/Delete/{id}", async (int id) =>
        {
            var a = await dataBase.DeleteAsync<User>(id);
            return a.ToString();
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}