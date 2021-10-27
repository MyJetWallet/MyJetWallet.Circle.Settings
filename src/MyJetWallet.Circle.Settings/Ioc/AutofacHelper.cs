using System;
using Autofac;
using MyJetWallet.Circle.Settings.NoSql;
using MyJetWallet.Circle.Settings.Services;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;
using MyNoSqlServer.DataWriter;

namespace MyJetWallet.Circle.Settings.Ioc
{
    public static class AutofacHelper
    {
        public static void RegisterCircleSettingsReader(this ContainerBuilder builder, IMyNoSqlSubscriber myNoSqlClient)
        {
            builder
                .RegisterInstance(
                    new MyNoSqlReadRepository<CircleAssetEntity>(myNoSqlClient, CircleAssetEntity.TableName))
                .As<IMyNoSqlServerDataReader<CircleAssetEntity>>()
                .SingleInstance();

            builder
                .RegisterType<WalletMapper>()
                .As<IWalletMapper>()
                .SingleInstance();

            builder
                .RegisterType<AssetMapper>()
                .As<IAssetMapper>()
                .SingleInstance();
        }

        public static void RegisterCircleSettingsWriter(this ContainerBuilder builder, Func<string> myNoSqlWriterUrl)
        {
            builder
                .RegisterInstance(new MyNoSqlServerDataWriter<CircleAssetEntity>(myNoSqlWriterUrl,
                    CircleAssetEntity.TableName, true))
                .As<IMyNoSqlServerDataWriter<CircleAssetEntity>>()
                .SingleInstance();

            builder.RegisterType<CircleAssetSettingsService>()
                .As<ICircleAssetSettingsService>()
                .SingleInstance();
        }
    }
}