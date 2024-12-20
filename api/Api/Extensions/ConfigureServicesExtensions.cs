using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PointOfSale.Api.BackgroundServices;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Services;
using PointOfSale.BusinessLogic.BusinessManagement.Interfaces;
using PointOfSale.BusinessLogic.BusinessManagement.Services;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;
using PointOfSale.BusinessLogic.OrderManagement.Services;
using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;
using PointOfSale.BusinessLogic.PaymentManagement.Services;
using PointOfSale.DataAccess;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Interfaces;
using PointOfSale.DataAccess.BusinessManagement.Repositories;
using PointOfSale.DataAccess.OrderManagement.Interfaces;
using PointOfSale.DataAccess.OrderManagement.Repositories;
using PointOfSale.DataAccess.PaymentManagement.Interfaces;
using PointOfSale.DataAccess.PaymentManagement.Repositories;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.DataAccess.Shared.Repositories;
using PointOfSale.Models.ApplicationUserManagement.Entities;
using Stripe;
using DiscountService = PointOfSale.BusinessLogic.OrderManagement.Services.DiscountService;
using ProductService = PointOfSale.BusinessLogic.OrderManagement.Services.ProductService;
using TaxService = PointOfSale.BusinessLogic.OrderManagement.Services.TaxService;

namespace PointOfSale.Api.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        if (environment.IsDevelopment())
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(policyBuilder =>
                    policyBuilder
                        .SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
        }

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddOrderManagement(this IServiceCollection services)
    {
        services.AddScoped<ITaxRepository, TaxRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IModifierRepository, ModifierRepository>();
        services.AddScoped<IServiceChargeRepository, ServiceChargeRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();

        services.AddScoped<ITaxMappingService, TaxMappingService>();
        services.AddScoped<IProductMappingService, ProductMappingService>();
        services.AddScoped<IOrderMappingService, OrderMappingService>();
        services.AddScoped<IModifierMappingService, ModifierMappingService>();
        services.AddScoped<IServiceChargeMappingService, ServiceChargeMappingService>();
        services.AddScoped<IDiscountMappingService, DiscountMappingService>();
        services.AddScoped<IServiceMappingService, ServiceMappingService>();
        services.AddScoped<IReservationMappingService, ReservationMappingService>();

        services.AddScoped<ITaxValidationService, TaxValidationService>();
        services.AddScoped<IProductValidationService, ProductValidationService>();
        services.AddScoped<IModifierValidationService, ModifierValidationService>();
        services.AddScoped<IServiceChargeValidationService, ServiceChargeValidationService>();
        services.AddScoped<IServiceValidationService, ServiceValidationService>();
        services.AddScoped<IReservationValidationService, ReservationValidationService>();
        services.AddScoped<IDiscountValidationService, DiscountValidationService>();

        services.AddScoped<ITaxService, TaxService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IModifierService, ModifierService>();
        services.AddScoped<IServiceChargeService, ServiceChargeService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IReservationService, ReservationService>();

        services.AddSingleton<ISmsMessageService, AwsSnsService>(serviceProvider =>
        {
            var awsCredentials = new BasicAWSCredentials(
                Environment.GetEnvironmentVariable("AWS_SNS_ACCESS_KEY"),
                Environment.GetEnvironmentVariable("AWS_SNS_SECRET_ACCESS_KEY")
            );
            var snsConfig = new AmazonSimpleNotificationServiceConfig { RegionEndpoint = RegionEndpoint.EUCentral1 };

            var snsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, snsConfig);
            var logger = serviceProvider.GetRequiredService<ILogger<AwsSnsService>>();

            return new AwsSnsService(snsClient, logger);
        });

        services.AddHostedService<ReservationNotificationsBackgroundService>();

        return services;
    }

    public static IServiceCollection AddPaymentManagement(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IGiftCardRepository, GiftCardRepository>();
        services.AddScoped<ITipRepository, TipRepository>();

        services.AddScoped<IGiftCardValidationService, GiftCardValidationService>();

        services.AddScoped<IPaymentMappingService, PaymentMappingService>();
        services.AddScoped<IGiftCardMappingService, GiftCardMappingService>();

        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IGiftCardService, GiftCardService>();

        StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
        services.AddScoped<IStripeService, StripeService>();
        services.AddHostedService<PaymentBackgroundService>();
        services.AddHostedService<RefundsBackgroundService>();

        return services;
    }

    public static IServiceCollection AddBusinessManagement(this IServiceCollection services)
    {
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IBusinessValidationService, BusinessValidationService>();
        services.AddScoped<IBusinessMappingService, BusinessMappingService>();
        services.AddScoped<IBusinessAuthorizationService, BusinessAuthorizationService>();
        services.AddScoped<IBusinessService, BusinessService>();

        return services;
    }

    public static IServiceCollection AddApplicationUserManagement(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IContactInfoValidationService, ContactInfoValidationService>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IApplicationUserMappingService, ApplicationUserMappingService>();
        services.AddScoped<IApplicationUserValidationService, ApplicationUserValidationService>();

        services.AddScoped<IApplicationUserAuthorizationService, ApplicationUserAuthorizationService>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<ICurrentApplicationUserAccessor, CurrentApplicationUserAccessor>();
        services.AddScoped<IApplicationUserTokenService, ApplicationUserTokenService>();

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<IdentityRole<int>>>();

        services.AddHttpContextAccessor();
        services.AddScoped<IOrderManagementAuthorizationService, OrderManagementAuthorizationService>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies["AccessToken"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                };
            });

        return services;
    }
}
