using System;
using System.Linq;
using System.Threading.Tasks;
using MyJetWallet.Circle.Settings.NoSql;
using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Circle.Settings.Services
{
    public class CircleBlockchainSettingsService : ICircleBlockchainSettingsService
    {
        private readonly IMyNoSqlServerDataWriter<CircleBlockchainEntity> _circleBlockchains;

        public CircleBlockchainSettingsService(IMyNoSqlServerDataWriter<CircleBlockchainEntity> circleBlockchains)
        {
            _circleBlockchains = circleBlockchains;
        }

        public async ValueTask<bool> CreateCircleBlockchainMapEntityAsync(CircleBlockchainEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot create circle Blockchain. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.Blockchain))
                throw new Exception("Cannot create circle Blockchain. Blockchain cannot be empty");
            if (string.IsNullOrEmpty(entity.CircleBlockchain))
                throw new Exception("Cannot create circle Blockchain. CircleBlockchain cannot be empty");

            var newEntity = CircleBlockchainEntity.Create(entity);

            var existingItem = await _circleBlockchains.GetAsync(newEntity.PartitionKey, newEntity.RowKey);
            if (existingItem != null) throw new Exception("Cannot create circle Blockchain. Already exist");

            await _circleBlockchains.InsertAsync(entity);

            return true;
        }

        public async ValueTask<bool> UpdateCircleBlockchainMapEntityAsync(CircleBlockchainEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot update circle Blockchain. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.Blockchain))
                throw new Exception("Cannot update circle Blockchain. Blockchain cannot be empty");
            if (string.IsNullOrEmpty(entity.CircleBlockchain))
                throw new Exception("Cannot update circle Blockchain. CircleBlockchain cannot be empty");
            
            var newEntity = CircleBlockchainEntity.Create(entity);

            var existingEntity = await _circleBlockchains.GetAsync(newEntity.PartitionKey, newEntity.RowKey);
            if (existingEntity == null) throw new Exception("Cannot update circle Blockchain. circle Blockchain not found");

            await _circleBlockchains.InsertOrReplaceAsync(entity);

            return true;
        }

        public async ValueTask<bool> DeleteCircleBlockchainMapEntityAsync(CircleBlockchainEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot delete circle Blockchain. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.Blockchain))
                throw new Exception("Cannot delete circle Blockchain. Blockchain cannot be empty");

            var existingEntity = await _circleBlockchains.GetAsync(CircleBlockchainEntity.GeneratePartitionKey(entity.BrokerId),
                CircleBlockchainEntity.GenerateRowKey(entity.CircleBlockchain));

            if (existingEntity != null)
            {
                await _circleBlockchains.DeleteAsync(existingEntity.PartitionKey, existingEntity.RowKey);
            }

            return true;
        }

        public async ValueTask<CircleBlockchainEntity[]> GetAllBlockchainMapsAsync()
        {
            var entities = await _circleBlockchains.GetAsync();
            return entities.ToArray();
        }
    }
}