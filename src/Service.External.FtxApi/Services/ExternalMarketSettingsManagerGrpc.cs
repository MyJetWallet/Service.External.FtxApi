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
        private readonly OrderBookManager _orderBookManager;

        public ExternalMarketSettingsManagerGrpc(IExternalMarketSettingsAccessor accessor,
            IExternalMarketSettingsManager manager, OrderBookManager orderBookManager)
        {
            _accessor = accessor;
            _manager = manager;
            _orderBookManager = orderBookManager;
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
            _manager.AddExternalMarketSettings(settings);
            return _orderBookManager.Subscribe(settings.Market);
        }

        public Task UpdateExternalMarketSettings(ExternalMarketSettings settings)
        {
            _manager.UpdateExternalMarketSettings(settings);
            return _orderBookManager.Resubscribe(settings.Market);
        }

        public Task RemoveExternalMarketSettings(RemoveMarketRequest request)
        {
            _manager.RemoveExternalMarketSettings(request.Symbol);
            return _orderBookManager.Unsubscribe(request.Symbol);
        }
    }
}