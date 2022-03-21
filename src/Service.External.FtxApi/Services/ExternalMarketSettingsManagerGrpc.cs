using System.Threading.Tasks;
using MyJetWallet.Sdk.ExternalMarketsSettings.Grpc;
using MyJetWallet.Sdk.ExternalMarketsSettings.Grpc.Models;
using MyJetWallet.Sdk.ExternalMarketsSettings.Models;
using MyJetWallet.Sdk.ExternalMarketsSettings.Settings;

namespace Service.External.FtxApi.Services
{
    public class ExternalMarketSettingsManagerGrpc : IExternalMarketSettingsManagerGrpc
    {
        private readonly IExternalMarketSettingsAccessor _accessor;
        private readonly IExternalMarketSettingsManager _manager;

        public ExternalMarketSettingsManagerGrpc(IExternalMarketSettingsAccessor accessor,
            IExternalMarketSettingsManager manager)
        {
            _accessor = accessor;
            _manager = manager;
        }

        public Task GetExternalMarketSettings(GetMarketRequest request)
        {
            return Task.FromResult(_accessor.GetExternalMarketSettings(request.Symbol));
        }

        public Task<GrpcList<ExternalMarketSettings>> GetExternalMarketSettingsList()
        {
            return Task.FromResult(GrpcList<ExternalMarketSettings>.Create(_accessor.GetExternalMarketSettingsList()));
        }

        public Task AddExternalMarketSettings(ExternalMarketSettings settings)
        {
            return _manager.AddExternalMarketSettings(settings);
        }

        public Task UpdateExternalMarketSettings(ExternalMarketSettings settings)
        {
            return _manager.UpdateExternalMarketSettings(settings);
        }

        public Task RemoveExternalMarketSettings(RemoveMarketRequest request)
        {
            return _manager.RemoveExternalMarketSettings(request.Symbol);
        }
    }
}