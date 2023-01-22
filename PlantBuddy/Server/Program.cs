using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PlantBuddy.Server.Common.Behaviors;
using PlantBuddy.Server.Common.Errors;
using PlantBuddy.Server.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddSwagger();
    builder.Services.AddIdentity(builder.Configuration);
    builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

    builder.Services.AddSingleton<ProblemDetailsFactory, PlantBuddyProblemDetailsFactory>();
    
    builder.Services.AddMediatR(typeof(Program).Assembly);
    builder.Services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(opts =>
        {
            opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseBlazorFrameworkFiles();
    app.UseStaticFiles();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseRouting();

    app.MapRazorPages();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();
}