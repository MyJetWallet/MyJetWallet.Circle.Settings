namespace MyJetWallet.Circle.Settings.Services
{
    public interface IBlockchainMapper
    {
        string BlockchainToCircleBlockchain(string brokerId, string blockchainSymbol);
        string CircleBlockchainToBlockchain(string brokerId, string circleBlockchain);
        string GetTagSeparator(string brokerId, string assetSymbol);
    }
}