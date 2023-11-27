using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("MongoDBConnection");

var mongoClient = new MongoClient(connectionString);

using (var cursor = await mongoClient.ListDatabasesAsync())
{
    var databases = cursor.ToList();
    foreach (var database in databases)
    {
        Console.WriteLine(database);
    }
}

builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddSingleton<IMongoDatabase>(provider =>
{
    var client = provider.GetRequiredService<IMongoClient>();
    return client.GetDatabase("Sellers");
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
