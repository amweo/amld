namespace Amld.Extensions.Discovery
{
    public interface IServiceDiscovery
    {

         List<string> Address(string serviceName);
    }
}