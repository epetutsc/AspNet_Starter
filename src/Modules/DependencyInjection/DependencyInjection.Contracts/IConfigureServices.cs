using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Contracts
{
    public interface IConfigureServices
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
