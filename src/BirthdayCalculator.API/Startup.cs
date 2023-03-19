using BirthdayCalculator.Domain;
using BirthdayCalculator.Domain.Models;
using BirthdayCalculator.Domain.Services;
using BirthdayCalculator.ViewModels;
using BirthdayCalculator.ViewModels.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace BirthdayCalculator.API;

public class Startup
{
    public Startup(IConfiguration configuration, IHostEnvironment env)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        //Register Fluent Validator           
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        }).AddValidatorsFromAssemblyContaining<PersonValidator>();

        //Register Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"BirthdayCalculator.API {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}", Version = "v1" });
            c.DocInclusionPredicate((name, api) => true);
            c.UseInlineDefinitionsForEnums();           

        });

        // Register Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<BirthdayHandler>());

        // Add AutoMapper
        services.AddAutoMapper(typeof(ApiMappingProfile));

        // Register App Services
        services.AddScoped<IBirthdayService, BirthdayService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddHealthChecks();

        // Http Client Registration
        services.AddHttpClient();

        //Automatic Handling of validation errors on Api request models
        HandleValidationErrors(services);

    }

    private static void HandleValidationErrors(IServiceCollection services)
    {
        services.Configure((Action<ApiBehaviorOptions>)(options =>
                    options.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = c.ModelState.Where(v => v.Value?.Errors.Count > 0)
                            .Select(v => new ErrorResponseItem { Field = v.Key, Errors = v.Value?.Errors.Select(e => e.ErrorMessage) })
                            .ToList();

                        return new BadRequestObjectResult(new ErrorResponse
                        {
                            Message = "One or more Validation errors Occurred",
                            Errors = errors
                        });
                    }));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DisplayRequestDuration();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BirthdayCalculator.API v1");
            });

        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
