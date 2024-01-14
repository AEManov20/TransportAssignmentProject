using Microsoft.EntityFrameworkCore;
using Transport.Data;
using Microsoft.AspNetCore.Identity;
using Transport.BusinessLogic.Services;
using Transport.Data.Models;
using Transport.WebHost;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, o =>
    {
        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        o.MigrationsAssembly(typeof(Program).Assembly.FullName);
    });

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }

    options.EnableThreadSafetyChecks();
    options.EnableDetailedErrors();
});

builder.Services
    .AddIdentity<User, IdentityRole<Guid>>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
        opt.SignIn.RequireConfirmedAccount = false;
        opt.SignIn.RequireConfirmedEmail = false;
        opt.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        // ReSharper disable once VariableHidesOuterVariable
        options.AddDefaultPolicy(builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });
}

builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAccessTokenSecret"]!)),
        };
    });

builder.Services.AddControllers(options =>
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Mappings));
builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();
app.UseCors();
app.Run();