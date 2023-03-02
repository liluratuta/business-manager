using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;
using Scripts.Services;

namespace Scripts.Systems.Business
{
    public class LevelUpSystem : IEcsRunSystem
    {
        private readonly LevelCostService _levelCostService;

        public LevelUpSystem(LevelCostService levelCostService)
        {
            _levelCostService = levelCostService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var upgradeRequestFilter = world.Filter<LevelUpRequest>().End();

            foreach (var entity in upgradeRequestFilter)
            {
                ref var levelComponent = ref world.GetPool<LevelComponent>().Get(entity);

                var walletFilter = world.Filter<WalletComponent>().End();
                var wallets = world.GetPool<WalletComponent>();

                var walletEntity = world.FirstEntityWith<WalletComponent>();

                if (!IsPossibleLevelUp(world, levelComponent, walletEntity))
                    continue;

                PayForLevelUp(walletEntity, world, levelComponent.NextLevelCost);
                levelComponent.Level += 1;
                levelComponent.NextLevelCost = _levelCostService.NextLevelCost(entity);
                SetIsWorking(world, entity, levelComponent);

                world.GetPool<LevelUpgradedEvent>().Add(entity);
            }
        }

        private static bool IsPossibleLevelUp(EcsWorld world, LevelComponent levelComponent, int walletEntity) =>
            world.GetPool<WalletComponent>().Get(walletEntity).Amount >= levelComponent.NextLevelCost;

        private void PayForLevelUp(int walletEntity, EcsWorld world, double nextLevelCost)
        {
            world.GetPool<WalletTransactionsComponent>()
                .Get(walletEntity).Transactions
                .Add(-nextLevelCost);
        }

        private static void SetIsWorking(EcsWorld world, int entity, LevelComponent levelComponent)
        {
            var isWorkingComponents = world.GetPool<IsWorking>();

            if (levelComponent.Level > 0 && !isWorkingComponents.Has(entity))
            {
                isWorkingComponents.Add(entity);
            }
        }
    }
}