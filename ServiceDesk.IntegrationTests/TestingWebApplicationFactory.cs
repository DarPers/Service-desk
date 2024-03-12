using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.DAL;
using ServiceDeskAPI;

namespace ServiceDesk.IntegrationTests;

public class TestingWebApplicationFactory : WebApplicationFactory<IApiMarker>
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
        });
    }
}
