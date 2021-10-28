using MyJetWallet.Circle.Settings.NoSql;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface ICircleBlockchainMapper
    {
        CircleBlockchainEntity BlockchainToCircleBlockchain(string brokerId, string blockchainSymbol);
        CircleBlockchainEntity CircleBlockchainToBlockchain(string brokerId, string circleBlockchain);
        string GetTagSeparator(string brokerId, string assetSymbol);
    }
}