using System;
using System.Linq;
using MyJetWallet.Circle.Settings.NoSql;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace MyJetWallet.Circle.Settings.Services
{
    public class AssetMapper : IAssetMapper
    {
        private readonly IMyNoSqlServerDataReader<CircleAssetEntity> _circleCoins;

        public AssetMapper(IMyNoSqlServerDataReader<CircleAssetEntity> circleCoins)
        {
            _circleCoins = circleCoins;
        }

        public string AssetToCircleAsset(string brokerId, string assetSymbol)
        {
            var assetEntities = _circleCoins.Get(CircleAssetEntity.GeneratePartitionKey(brokerId)).Where(e => e.AssetSymbol == assetSymbol).ToList();

            if (!assetEntities.Any())
            {
                return string.Empty;
            }

            if (assetEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map Circle asset {assetEntities} to Asset. Table: {CircleAssetEntity.TableName}. Found many assets: {JsonConvert.SerializeObject(assetSymbol)}");
            }

            var entity = assetEntities.First();

            return entity.AssetSymbol;
        }

        public string CircleAssetToAsset(string brokerId, string circleAsset)
        {
            var entity = _circleCoins.Get(CircleAssetEntity.GeneratePartitionKey(brokerId), CircleAssetEntity.GeneratePartitionKey(circleAsset));

            return entity == null ? string.Empty : entity.AssetSymbol;
        }
    }
}