using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;

namespace Scripts.Systems.Business
{
    public class LevelButtonStateUISystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var uiProviderFilter = world.Filter<BusinessUIProviderComponent>().End();
            var providers = world.GetPool<BusinessUIProviderComponent>();
            var buttonStates = world.GetPool<LevelButtonStateComponent>();

            foreach (var entity in uiProviderFilter)
            {
                ref var buttonState = ref buttonStates.Get(entity);
                ref var uiProvider = ref providers.Get(entity);

                uiProvider.LevelUpButtonView.SetLevelUpPossible(buttonState.IsActivePrevious);
            }
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var levelStateFilter = world.Filter<LevelButtonStateComponent>().End();

            var levelStates = world.GetPool<LevelButtonStateComponent>();
            var levels = world.GetPool<LevelComponent>();

            var walletComponent = world.GetPool<WalletComponent>().Get(world.FirstEntityWith<WalletComponent>());

            foreach (var entity in levelStateFilter)
            {
                ref var level = ref levels.Get(entity);

                var isButtonActiveNow = walletComponent.Amount >= level.NextLevelCost;

                ref var levelState = ref levelStates.Get(entity);

                if (isButtonActiveNow == levelState.IsActivePrevious)
                    continue;

                ref var uiProvider = ref world.GetPool<BusinessUIProviderComponent>().Get(entity);

                uiProvider.LevelUpButtonView.SetLevelUpPossible(isPossible: isButtonActiveNow);

                levelState.IsActivePrevious = isButtonActiveNow;
            }
        }
    }
}