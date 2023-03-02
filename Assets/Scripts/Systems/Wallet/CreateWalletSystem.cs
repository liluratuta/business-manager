using Leopotam.EcsLite;
using Scripts.Components.Wallet;
using Scripts.Services;

namespace Scripts.Systems.Wallet
{
    public class CreateWalletSystem : IEcsInitSystem
    {
        private readonly ProgressService _progressService;

        public CreateWalletSystem(ProgressService progressService)
        {
            _progressService = progressService;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            world.GetPool<WalletComponent>().Add(entity);
            world.GetPool<WalletUIProviderComponent>().Add(entity);
            world.GetPool<WalletTransactionsComponent>().Add(entity);
            world.GetPool<WalletChangedEvent>().Add(entity);

            ref var walletComponent = ref world.GetPool<WalletComponent>().Get(entity);
            walletComponent.Amount = _progressService.WalletAmount();
        }
    }
}