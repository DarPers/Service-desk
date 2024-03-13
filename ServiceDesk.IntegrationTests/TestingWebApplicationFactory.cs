using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceDesk.DAL;

namespace ServiceDesk.IntegrationTests;

public class TestingWebApplicationFactory
{
    internal readonly WebApplicationFactory<Program> WebHost;

    public TestingWebApplicationFactory()
    {
        WebHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
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
            }));
    }
}
