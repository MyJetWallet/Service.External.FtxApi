using System.Runtime.Serialization;
using Service.External.FtxApi.Domain.Models;

namespace Service.External.FtxApi.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}