namespace MyJetWallet.Circle.Settings.Services
{
    public interface IAssetMapper
    {
        string AssetToCircleAsset(string brokerId, string assetSymbol);
        string CircleAssetToAsset(string brokerId, string circleAsset);
    }
}