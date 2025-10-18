using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<MySqlConnection>(_ =>
{
    var connStr = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                  $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                  $"Uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                  $"Pwd={Environment.GetEnvironmentVariable("DB_PASS")};";
    return new MySqlConnection(connStr);
});

var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.Run();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
