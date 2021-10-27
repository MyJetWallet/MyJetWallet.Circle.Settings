using System.Threading.Tasks;
using MyJetWallet.Circle.Settings.NoSql;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface ICircleAssetSettingsService
    {
        ValueTask<bool> CreateCircleAssetMapEntityAsync(CircleAssetEntity entity);

        ValueTask<bool> UpdateCircleAssetMapEntityAsync(CircleAssetEntity entity);

        ValueTask<bool> DeleteCircleAssetMapEntityAsync(CircleAssetEntity entity);

        ValueTask<CircleAssetEntity[]> GetAllAssetMapsAsync();
    }
}