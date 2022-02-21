using System;
using Autofac;
using MyJetWallet.Circle.Settings.NoSql;
using MyJetWallet.Circle.Settings.Services;
using MyJetWallet.Sdk.NoSql;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;
using MyNoSqlServer.DataWriter;

namespace MyJetWallet.Circle.Settings.Ioc
{
    public static class AutofacHelper
    {
        public static void RegisterCircleSettingsReader(this ContainerBuilder builder, MyNoSqlTcpClient myNoSqlClient)
        {
            builder
                .RegisterMyNoSqlReader<CircleAssetEntity>(myNoSqlClient, CircleAssetEntity.TableName);

            builder
                .RegisterMyNoSqlReader<CircleBlockchainEntity>(myNoSqlClient, CircleBlockchainEntity.TableName);

            builder
                .RegisterType<CircleWalletMapper>()
                .As<ICircleWalletMapper>()
                .SingleInstance();

            builder
                .RegisterType<CircleAssetMapper>()
                .As<ICircleAssetMapper>()
                .SingleInstance();

            builder
                .RegisterType<CircleBlockchainMapper>()
                .As<ICircleBlockchainMapper>()
                .SingleInstance();
        }

        public static void RegisterCircleSettingsWriter(this ContainerBuilder builder, Func<string> myNoSqlWriterUrl)
        {
            builder
                .RegisterMyNoSqlWriter<CircleAssetEntity>(myNoSqlWriterUrl, CircleAssetEntity.TableName);
            
            builder
                .RegisterMyNoSqlWriter<CircleBlockchainEntity>(myNoSqlWriterUrl, CircleBlockchainEntity.TableName);

            builder.RegisterType<CircleAssetSettingsService>()
                .As<ICircleAssetSettingsService>()
                .SingleInstance();

            builder.RegisterType<CircleBlockchainSettingsService>()
                .As<ICircleBlockchainSettingsService>()
                .SingleInstance();
        }

        public static void RegisterCircleCommonServices(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CircleWireCountriesService>()
                .As<ICircleWireCountriesService>()
                .SingleInstance();
        }
    }
}