using MyJetWallet.Domain;

namespace MyJetWallet.Circle.Settings.Services
{
    public interface ICircleWalletMapper
    {
        IJetWalletIdentity CircleLabelToWallet(string label);
        string WalletToCircleLabel(IJetWalletIdentity wallet);
    }
}