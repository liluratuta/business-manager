using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Systems.Business
{
    public class LevelUpdateUISystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var levelUpgradedFilter = world.Filter<LevelUpgradedEvent>().End();
            var uiProviders = world.GetPool<BusinessUIProviderComponent>();
            var levels = world.GetPool<LevelComponent>();

            foreach (var entity in levelUpgradedFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var levelComponent = ref levels.Get(entity);

                uiProvider.LevelView.SetLevel(levelComponent.Level);
                uiProvider.LevelUpButtonView.SetCost(levelComponent.NextLevelCost);
            }
        }
    }
}