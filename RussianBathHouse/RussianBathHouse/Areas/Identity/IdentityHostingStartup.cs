using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RussianBathHouse.Areas.Identity.IdentityHostingStartup))]
namespace RussianBathHouse.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}