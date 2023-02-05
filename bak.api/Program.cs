using AutoMapper;
using bak.api.Configurations;
using bak.api.Context;
using bak.api.Extensions;
using bak.api.Interface;
using bak.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(); //.AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("MailSettings"));

var mapConfig = new MapperConfiguration(opt => { opt.AddProfile(new MappingConfiguration()); });

builder.Services.AddSingleton(mapConfig.CreateMapper());

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDatabaseContext(builder.Configuration);

builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.AddJwtAuth();

builder.Services.AddSwaggerBearerAuth();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();

    context.Database.EnsureCreated();
}

app.UseEnsureAndSeedDb();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();