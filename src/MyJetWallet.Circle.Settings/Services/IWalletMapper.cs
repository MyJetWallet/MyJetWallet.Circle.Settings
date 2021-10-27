using MyJetWallet.Domain;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface IWalletMapper
    {
        IJetWalletIdentity CircleLabelToWallet(string label);
        string WalletToCircleLabel(IJetWalletIdentity wallet);
    }
}