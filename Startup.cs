using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(OrderTransfer.Startup))]

namespace OrderTransfer
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IDownloadingService, DownloadingService>();
            builder.Services.AddTransient<IUpdatingService, UpdatingService>();
        }
    }
}
