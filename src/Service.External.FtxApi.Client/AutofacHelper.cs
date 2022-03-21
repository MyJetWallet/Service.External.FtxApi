using Autofac;

// ReSharper disable UnusedMember.Global

namespace Service.External.FtxApi.Client
{
    public static class AutofacHelper
    {
        public static void RegisterExternalFtxApiClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new ExternalFtxApiClientFactory(grpcServiceUrl);
        }
    }
}
