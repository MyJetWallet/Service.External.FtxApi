using System;
using System.Threading.Tasks;
using MyJetWallet.Domain.ExternalMarketApi;
using MyJetWallet.Domain.ExternalMarketApi.Dto;
using ProtoBuf.Grpc.Client;
using Service.External.FtxApi.Client;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            Console.Write("Press enter to start");
            Console.ReadLine();

            var factory = new ExternalFtxApiClientFactory("http://localhost:80/");
            var client = factory.CreateGrpcService<IExternalMarket>();
            await client.GetTradesAsync(new GetTradesRequest() {StartDate = DateTime.UtcNow.AddDays(-1), EndDate = DateTime.UtcNow});

            
            await Task.Delay(1000);
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
