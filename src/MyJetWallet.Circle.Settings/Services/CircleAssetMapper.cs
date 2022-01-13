using System;
using System.Linq;
using MyJetWallet.Circle.Settings.NoSql;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace MyJetWallet.Circle.Settings.Services
{
    public class CircleAssetMapper : ICircleAssetMapper
    {
        private readonly IMyNoSqlServerDataReader<CircleAssetEntity> _circleCoins;

        public CircleAssetMapper(IMyNoSqlServerDataReader<CircleAssetEntity> circleCoins)
        {
            _circleCoins = circleCoins;
        }

        public CircleAssetEntity AssetToCircleAsset(string brokerId, string assetSymbol)
        {
            var assetEntities = _circleCoins.Get(CircleAssetEntity.GeneratePartitionKey(brokerId)).Where(e => e.AssetSymbol == assetSymbol).ToList();

            if (!assetEntities.Any())
            {
                return null;
            }

            if (assetEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map Circle asset {assetEntities} to Asset. Table: {CircleAssetEntity.TableName}. Found many assets: {JsonConvert.SerializeObject(assetSymbol)}");
            }

            var entity = assetEntities.First();

            return entity;
        }

        public CircleAssetEntity AssetToCircleTokenAsset(string brokerId, string assetTokenSymbol)
        {
            var assetEntities = _circleCoins.Get(CircleAssetEntity.GeneratePartitionKey(brokerId)).Where(e => e.AssetTokenSymbol == assetTokenSymbol).ToList();

            if (!assetEntities.Any())
            {
                return null;
            }

            if (assetEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map Circle asset {assetEntities} to Asset. Table: {CircleAssetEntity.TableName}. Found many assets: {JsonConvert.SerializeObject(assetTokenSymbol)}");
            }

            var entity = assetEntities.First();

            return entity;
        }

        public CircleAssetEntity CircleAssetToAsset(string brokerId, string circleAsset)
        {
            return _circleCoins.Get(CircleAssetEntity.GeneratePartitionKey(brokerId), CircleAssetEntity.GenerateRowKey(circleAsset));
        }
    }
}