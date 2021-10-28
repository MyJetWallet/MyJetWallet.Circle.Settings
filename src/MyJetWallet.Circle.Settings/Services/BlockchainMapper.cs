using System;
using System.Linq;
using MyJetWallet.Circle.Settings.NoSql;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace MyJetWallet.Circle.Settings.Services
{
    public class BlockchainMapper : IBlockchainMapper
    {
        private readonly IMyNoSqlServerDataReader<CircleBlockchainEntity> _circleBlockchains;

        public BlockchainMapper(IMyNoSqlServerDataReader<CircleBlockchainEntity> circleBlockchains)
        {
            _circleBlockchains = circleBlockchains;
        }

        public string BlockchainToCircleBlockchain(string brokerId, string blockchainSymbol)
        {
            var blockchainEntities = _circleBlockchains.Get(CircleBlockchainEntity.GeneratePartitionKey(brokerId))
                .Where(e => e.Blockchain == blockchainSymbol).ToList();

            if (!blockchainEntities.Any())
            {
                return string.Empty;
            }

            if (blockchainEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map Circle Blockchain {blockchainEntities} to Blockchain. Table: {CircleBlockchainEntity.TableName}. Found many Blockchains: {JsonConvert.SerializeObject(blockchainSymbol)}");
            }

            var entity = blockchainEntities.First();

            return entity.CircleBlockchain;
        }

        public string CircleBlockchainToBlockchain(string brokerId, string circleBlockchain)
        {
            var entity = _circleBlockchains.Get(CircleBlockchainEntity.GeneratePartitionKey(brokerId),
                CircleBlockchainEntity.GeneratePartitionKey(circleBlockchain));

            return entity == null ? string.Empty : entity.Blockchain;
        }

        public string GetTagSeparator(string brokerId, string assetSymbol)
        {
            var map = _circleBlockchains.Get(CircleAssetEntity.GeneratePartitionKey(brokerId),
                CircleAssetEntity.GenerateRowKey(assetSymbol));

            return map == null ? string.Empty : map.TagSeparator;
        }
    }
}