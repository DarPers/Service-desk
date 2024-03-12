using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.DAL;

namespace ServiceDesk.IntegrationTests;

public class TestingWebApplicationFactory<TEntityPoint> : WebApplicationFactory<Program> where TEntityPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor =
                services.SingleOrDefault(i => 
                    i.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(dbContextDescriptor!);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbContextTest");
            });

            //var serviceProvider = services.BuildServiceProvider();
            //using (var scope = serviceProvider.CreateScope())
            //using (var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            //{
            //    try
            //    {
            //        applicationDbContext.Database.EnsureCreated();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        });
    }
}
