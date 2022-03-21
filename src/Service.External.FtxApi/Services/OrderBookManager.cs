using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Connector.Ftx.WebSocket;
using MyJetWallet.Connector.Ftx.WebSocket.Models;
using MyJetWallet.Domain.ExternalMarketApi.Models;
using MyJetWallet.Domain.Prices;
using MyJetWallet.Sdk.ExternalMarketsSettings.Settings;
using MyJetWallet.Sdk.Service.Tools;
using MyJetWallet.Sdk.ServiceBus;

namespace Service.External.FtxApi.Services
{
    public class OrderBookManager : IDisposable
    {

        private readonly FtxWsOrderBooks _wsFtx;
        private readonly IExternalMarketSettingsAccessor _externalMarketSettingsAccessor;
        private readonly IServiceBusPublisher<BidAsk> _publisher;

        private Dictionary<string, BidAsk> _updated = new Dictionary<string, BidAsk>();
        private MyTaskTimer _timer;

        public OrderBookManager(IExternalMarketSettingsAccessor externalMarketSettingsAccessor,
            ILoggerFactory loggerFactory, IServiceBusPublisher<BidAsk> publisher)
        {
            _externalMarketSettingsAccessor = externalMarketSettingsAccessor;
            _publisher = publisher;

            _wsFtx = new FtxWsOrderBooks(loggerFactory.CreateLogger<FtxWsOrderBooks>(), _externalMarketSettingsAccessor.GetExternalMarketSettingsList().Select(e => e.Market).ToArray());
            
            _timer = new MyTaskTimer(nameof(OrderBookManager), TimeSpan.FromMilliseconds(500), loggerFactory.CreateLogger<OrderBookManager>(), DoTime);
        }

        private async Task DoTime()
        {
            List<BidAsk> prices = new List<BidAsk>();
            
            var books = _wsFtx.GetOrderBooks();

            foreach (var book in books)
            {
                var price = new BidAsk
                {
                    LiquidityProvider = FtxConst.Name,
                    DateTime = book.GetTime().DateTime,
                    Id = book.id,
                    Ask = book.asks?.Min(e => e.GetFtxOrderBookPrice()) ?? 0,
                    Bid = book.bids?.Max(e => e.GetFtxOrderBookPrice()) ?? 0
                };

                if (price.Ask > 0 && price.Bid > 0)
                {
                    prices.Add(price);
                }
            }

            await _publisher.PublishAsync(prices);
        }

        public List<string> GetSymbols()
        {
            return _externalMarketSettingsAccessor.GetExternalMarketSettingsList().Select(e => e.Market).ToList();
        }

        public bool HasSymbol(string symbol)
        {
            return _externalMarketSettingsAccessor.GetExternalMarketSettingsList().Find(e => e.Market == symbol) !=
                   null;
        }

        public async Task Resubscribe(string symbol)
        {
            await _wsFtx.Reset(symbol);
        }

        public async Task Subscribe(string symbol)
        {
            await _wsFtx.Subscribe(symbol);
        }

        public async Task Unsubscribe(string symbol)
        {
            await _wsFtx.Unsubscribe(symbol);
        }

        public LeOrderBook GetOrderBook(string symbol)
        {
            var data = _wsFtx.GetOrderBookById(symbol);

            if (data == null)
                return null;

            var book = new LeOrderBook()
            {
                Symbol = symbol,
                Timestamp = data.GetTime().UtcDateTime,
                Asks = data.asks.Select(LeOrderBookLevel.Create).Where(e => e != null).ToList(),
                Bids = data.bids.Select(LeOrderBookLevel.Create).Where(e => e != null).ToList(),
                Source = FtxConst.Name
            };

            return book;
        }

        public void Start()
        {
            _wsFtx.Start();
            _timer.Start();
        }

        public void Stop()
        {
            _wsFtx.Stop();
        }


        public void Dispose()
        {
            _wsFtx?.Dispose();
            _timer?.Dispose();
        }
    }
}