using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Agro.Model.Data;
using System.Text;
using WebApi.JwtFeatures;
using WebApi.Services;
using AutoMapper;
using Agro.Model.Interfaces;
using Agro.Model.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Agro.Model.Entities;
using Logic.Services;
using Microsoft.OpenApi.Models;
using Logic.Interfaces;
using Logic.MapperConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Agro.Model.DataSeed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
    //opt =>
    //{
    //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    //}

    )
    .AddJwtBearer(
    authenticationScheme: JwtBearerDefaults.AuthenticationScheme,
    configureOptions: options =>
    {

        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.IncludeErrorDetails = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7272",
            ValidAudience = "https://localhost:7272",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    })      
    ;
builder.Services.AddAuthorization(option =>
{
    option.DefaultPolicy = 
        new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder(
            JwtBearerDefaults.AuthenticationScheme
        ).RequireAuthenticatedUser().
        Build();
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddControllers();
//.AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null) ;

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<JwtHandler>();

builder.Services.AddAutoMapper(typeof(Logic.MapperConfiguration.CityProfile));
//builder.Services.AddSingleton( new MapperSetup().GetMapperConfiguration().CreateMapper() );

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IIntentionService, IntentionService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(
    (options) =>
    {
        options.Password.RequiredLength = 0;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireNonAlphanumeric= false;

    }

) ;

//builder.Services.AddScoped<SignInManager<ApplicationUser>>();    

// services
builder.Services.AddScoped<ICityService, CityService>();
//builder.Services.AddSingleton(
//    new Logic.MapperConfiguration.MapperSetup().
//    GetMapperConfiguration().
//    CreateMapper());

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Api V1");
    });
    app.UseExceptionHandler("/Error");
}
app.UseCors("EnableCORS");
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//SampleData.Initialize(app.Services);

app.Run();
