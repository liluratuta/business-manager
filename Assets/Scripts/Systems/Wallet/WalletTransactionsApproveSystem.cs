using Leopotam.EcsLite;
using Scripts.Components.Wallet;
using System.Linq;

namespace Scripts.Systems.Wallet
{
    public class WalletTransactionsApproveSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var changeableWalletFilter = world.Filter<WalletComponent>().Inc<WalletTransactionsComponent>().End();
            var wallets = world.GetPool<WalletComponent>();
            var changeRequests = world.GetPool<WalletTransactionsComponent>();

            foreach (var entity in changeableWalletFilter)
            {
                ref var wallet = ref wallets.Get(entity);
                ref var transactionsComponent = ref changeRequests.Get(entity);

                if (transactionsComponent.Transactions.Count == 0)
                    continue;

                var previousAmunt = wallet.Amount;
                var nextAmount = previousAmunt + transactionsComponent.Transactions.Sum();
                transactionsComponent.Transactions.Clear();

                if (nextAmount == previousAmunt)
                    continue;

                wallet.Amount = nextAmount < 0 ? 0 : nextAmount;
                
                world.GetPool<WalletChangedEvent>().Add(entity);
            }
        }
    }
}