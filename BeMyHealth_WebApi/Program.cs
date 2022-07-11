using AutoMapper;
using BeMyHealth_WebApi.ContextData;
using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Models;
using BeMyHealth_WebApi.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var config = new MapperConfiguration(cfg =>
 {
     cfg.CreateMap<User, UserDto>()
         .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
         .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
         .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.DOB))
         .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
         .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
         .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
         .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.ConfirmPassword))
        .ReverseMap();
 }
);
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
builder.Services.AddDbContext<BeMyHealthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:con"]);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearerAuth"
                                }
                            },
                            new string[] {}
                    }
                });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
