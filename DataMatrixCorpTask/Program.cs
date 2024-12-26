using DataMatrixCorpTask.DB;
using DataMatrixCorpTask.Services.Contacts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataMatrixDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataMatrixDefault"));
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactsService, ContactsService>();

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


// Applying migrations to database before starting application
// initially i tried to install dotnet ef tools inside container in Dockerfile
// so i found this solutions to apply migrations programatically 
await Task.Run(() =>
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DataMatrixDbContext>();
        context.Database.Migrate();
    }
});

app.Run();