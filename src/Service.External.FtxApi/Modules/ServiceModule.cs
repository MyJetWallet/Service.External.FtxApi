using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using MyJetWallet.Connector.Ftx.Rest;
using MyJetWallet.Domain.Prices;
using MyJetWallet.Sdk.ExternalMarketsSettings.NoSql;
using MyJetWallet.Sdk.ExternalMarketsSettings.Services;
using MyJetWallet.Sdk.ExternalMarketsSettings.Settings;
using MyJetWallet.Sdk.NoSql;
using MyJetWallet.Sdk.ServiceBus;
using Service.External.FtxApi.Services;

namespace Service.External.FtxApi.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var ftxRestClient = FtxRestApiFactory.CreateClient(Program.Settings.ApiKey, Program.Settings.ApiSecret,
                Program.Settings.SubAccount);
            builder.RegisterInstance(ftxRestClient).AsSelf().SingleInstance();

            builder.RegisterType<BalanceCache>().As<IStartable>().AutoActivate().AsSelf().SingleInstance();

            builder
                .RegisterType<ExternalMarketSettingsManager>()
                .WithParameter("name", Program.Settings.ApiName)
                .As<IExternalMarketSettingsManager>()
                .As<IExternalMarketSettingsAccessor>()
                .AsSelf()
                .SingleInstance()
                .AutoActivate();

            builder.RegisterMyNoSqlWriter<ExternalMarketSettingsNoSql>(() =>
                Program.Settings.MyNoSqlWriterUrl, ExternalMarketSettingsNoSql.TableName);
        }
    }
}