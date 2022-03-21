using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;

namespace Service.External.FtxApi.Client
{
    [UsedImplicitly]
    public class ExternalFtxApiClientFactory: MyGrpcClientFactory
    {
        public ExternalFtxApiClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

    }
}
