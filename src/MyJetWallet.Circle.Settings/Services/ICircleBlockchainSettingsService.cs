using System.Threading.Tasks;
using MyJetWallet.Circle.Settings.NoSql;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface ICircleBlockchainSettingsService
    {
        ValueTask<bool> CreateCircleBlockchainMapEntityAsync(CircleBlockchainEntity entity);

        ValueTask<bool> UpdateCircleBlockchainMapEntityAsync(CircleBlockchainEntity entity);

        ValueTask<bool> DeleteCircleBlockchainMapEntityAsync(CircleBlockchainEntity entity);

        ValueTask<CircleBlockchainEntity[]> GetAllBlockchainMapsAsync();
    }
}