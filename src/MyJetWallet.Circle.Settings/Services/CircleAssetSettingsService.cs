using System;
using System.Linq;
using System.Threading.Tasks;
using MyJetWallet.Circle.Settings.NoSql;
using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Circle.Settings.Services
{
    public class CircleAssetSettingsService : ICircleAssetSettingsService
    {
        private readonly IMyNoSqlServerDataWriter<CircleAssetEntity> _circleAssets;

        public CircleAssetSettingsService(IMyNoSqlServerDataWriter<CircleAssetEntity> circleAssets)
        {
            _circleAssets = circleAssets;
        }

        public async ValueTask<bool> CreateCircleAssetMapEntityAsync(CircleAssetEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot create circle asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.AssetSymbol))
                throw new Exception("Cannot create circle asset. AssetSymbol cannot be empty");
            if (string.IsNullOrEmpty(entity.CircleAsset))
                throw new Exception("Cannot create circle asset. CircleAsset cannot be empty");
            if (string.IsNullOrEmpty(entity.CircleWalletId))
                throw new Exception("Cannot create circle asset. CircleWalletId cannot be empty");

            var newEntity = CircleAssetEntity.Create(entity);

            var existingItem = await _circleAssets.GetAsync(newEntity.PartitionKey, newEntity.RowKey);
            if (existingItem != null) throw new Exception("Cannot create circle asset. Already exist");

            await _circleAssets.InsertAsync(newEntity);

            return true;
        }

        public async ValueTask<bool> UpdateCircleAssetMapEntityAsync(CircleAssetEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot update circle asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.AssetSymbol))
                throw new Exception("Cannot update circle asset. AssetSymbol cannot be empty");
            if (string.IsNullOrEmpty(entity.CircleAsset))
                throw new Exception("Cannot update circle asset. CircleAsset cannot be empty");
            if (string.IsNullOrEmpty(entity.CircleWalletId))
                throw new Exception("Cannot update circle asset. CircleWalletId cannot be empty");
            
            var newEntity = CircleAssetEntity.Create(entity);

            var existingEntity = await _circleAssets.GetAsync(newEntity.PartitionKey, newEntity.RowKey);
            if (existingEntity == null) throw new Exception("Cannot update circle asset. circle asset not found");

            await _circleAssets.InsertOrReplaceAsync(newEntity);

            return true;
        }

        public async ValueTask<bool> DeleteCircleAssetMapEntityAsync(CircleAssetEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot delete circle asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.AssetSymbol))
                throw new Exception("Cannot delete circle asset. AssetSymbol cannot be empty");

            var existingEntity = await _circleAssets.GetAsync(CircleAssetEntity.GeneratePartitionKey(entity.BrokerId),
                CircleAssetEntity.GenerateRowKey(entity.CircleAsset));

            if (existingEntity != null)
            {
                await _circleAssets.DeleteAsync(existingEntity.PartitionKey, existingEntity.RowKey);
            }

            return true;
        }

        public async ValueTask<CircleAssetEntity[]> GetAllAssetMapsAsync()
        {
            var entities = await _circleAssets.GetAsync();
            return entities.ToArray();
        }
    }
}