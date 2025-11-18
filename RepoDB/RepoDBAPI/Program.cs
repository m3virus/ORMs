using RepoDb;
using RepoDBAPI;
using Microsoft.Data.Sqlite;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        SQLiteBootstrap.Initialize();
        IConfiguration config = builder.Configuration;
        var context = new DbContext(config);
        using var dataBase = context.AppDbContext;
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.MapPost("/Insert", async (string x) => {
            var a = await dataBase.InsertAsync(x);
            return a.ToString();
        });

        app.MapGet("/Get", async (int id) => {
            var a = (await dataBase.QueryAsync<User>(id));
            return a.ToString();
        });

        app.MapDelete("/Delete", async (int id) =>
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