using System.ServiceModel;
using System.Threading.Tasks;
using Service.External.FtxApi.Grpc.Models;

namespace Service.External.FtxApi.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}