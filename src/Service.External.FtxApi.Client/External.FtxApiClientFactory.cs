using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.External.FtxApi.Grpc;

namespace Service.External.FtxApi.Client
{
    [UsedImplicitly]
    public class ExternalFtxApiClientFactory: MyGrpcClientFactory
    {
        public ExternalFtxApiClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
