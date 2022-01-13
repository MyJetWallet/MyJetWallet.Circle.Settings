using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyJetWallet.Circle.Settings.NoSql;
using MyNoSqlServer.DataWriter;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GenerateAssets();
        }

        private static async Task GenerateAssets()
        {
            var broker = "jetwallet";
            var nosqlWriterUrl = "http://192.168.70.80:5123";

            var clientAsset =
                new MyNoSqlServerDataWriter<CircleAssetEntity>(() => nosqlWriterUrl, CircleAssetEntity.TableName,
                    true);

            var list = new List<CircleAssetEntity>();

            list.Add(CircleAssetEntity.Create(new CircleAssetEntity{
                BrokerId = broker, 
                AssetSymbol = "USD", 
                AssetTokenSymbol = "USDC",
                CircleAsset = "CUSDC"}));

            await clientAsset.CleanAndKeepMaxPartitions(0);
            await clientAsset.BulkInsertOrReplaceAsync(list);
        }
    }
}