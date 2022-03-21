using System;
using System.Threading.Tasks;
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
            await Task.Delay(1000);
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
