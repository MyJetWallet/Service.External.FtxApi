using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.External.FtxApi.Settings
{
    public class SettingsModel
    {
        [YamlProperty("ExternalFtxApi.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("ExternalFtxApi.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("ExternalFtxApi.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }   
  
        [YamlProperty("ExternalFtxApi.ApiKey")]
        public string ApiKey { get; set; }

        [YamlProperty("ExternalFtxApi.ApiSecret")]
        public string ApiSecret { get; set; }

        [YamlProperty("ExternalFtxApi.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }

        [YamlProperty("ExternalFtxApi.ServiceBusHostPort")]
        public string ServiceBusHostPort { get; set; }
        
        [YamlProperty("ExternalFtxApi.SubAccount")]
        public string SubAccount { get; set; }
        
        [YamlProperty("ExternalFtxApi.ApiName")]
        public string ApiName { get; set; }
    }
}
