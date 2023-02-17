using bak.api.Context;
using bak.api.Extensions;
using bak.api.Interface;
using bak.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerBearerAuth();


builder.Services.AddMapping(builder.Configuration);

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddDatabaseContext(builder.Configuration);

builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.AddJwtAuth();

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

app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();