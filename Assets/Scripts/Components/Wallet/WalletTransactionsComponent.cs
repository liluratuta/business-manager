using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Scripts.Components.Wallet
{
    public struct WalletTransactionsComponent : IEcsAutoReset<WalletTransactionsComponent>
    {
        public List<double> Transactions;

        public void AutoReset(ref WalletTransactionsComponent c)
        {
            c.Transactions = new List<double>();
        }
    }
}