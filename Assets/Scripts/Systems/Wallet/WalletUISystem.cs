using Leopotam.EcsLite;
using Scripts.Components.Wallet;

namespace Scripts.Systems.Wallet
{
    public class WalletUISystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var walletFilter = world.Filter<WalletComponent>().End();
            var wallets = world.GetPool<WalletComponent>();
            var providers = world.GetPool<WalletUIProviderComponent>();

            foreach (var entity in walletFilter)
            {
                ref var wallet = ref wallets.Get(entity);
                ref var provider = ref providers.Get(entity);

                provider.WalletView.SetAmount(wallet.Amount);
            }
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var changedFilter = world.Filter<WalletChangedEvent>().End();
            var wallets = world.GetPool<WalletComponent>();
            var providers = world.GetPool<WalletUIProviderComponent>();

            foreach (var entity in changedFilter)
            {
                ref var wallet = ref wallets.Get(entity);
                ref var provider = ref providers.Get(entity);

                provider.WalletView.SetAmount(wallet.Amount);
            }
        }
    }
}