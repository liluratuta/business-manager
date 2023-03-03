using Leopotam.EcsLite;
using Scripts.Components;
using Scripts.Components.Wallet;
using Scripts.Services;

namespace Scripts.Systems.Wallet
{
    public class WalletProgressUpdateSystem : IEcsDestroySystem
    {
        private readonly ProgressService _progressService;

        public WalletProgressUpdateSystem(ProgressService progressService)
        {
            _progressService = progressService;
        }

        public void Destroy(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var saveRequestFilter = world.Filter<SaveRequest>().Inc<WalletComponent>().End();

            if (saveRequestFilter.GetEntitiesCount() == 0)
                return;

            foreach (var entity in saveRequestFilter)
            {
                ref var wallet = ref world.GetPool<WalletComponent>().Get(entity);

                _progressService.UpdateWalletAmount(wallet.Amount);

                world.GetPool<SaveRequest>().Del(entity);
            }
        }
    }
}