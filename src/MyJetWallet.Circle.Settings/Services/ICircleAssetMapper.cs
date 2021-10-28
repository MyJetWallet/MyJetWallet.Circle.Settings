using MyJetWallet.Circle.Settings.NoSql;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface ICircleAssetMapper
    {
        CircleAssetEntity AssetToCircleAsset(string brokerId, string assetSymbol);
        CircleAssetEntity CircleAssetToAsset(string brokerId, string circleAsset);
    }
}