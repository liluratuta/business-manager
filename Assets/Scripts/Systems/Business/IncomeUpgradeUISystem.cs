using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Systems.Business
{
    public class IncomeUpgradeUISystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var incomeUpgradedFilter = world.Filter<IncomeUpgradedEvent>().End();
            var businesses = world.GetPool<BusinessComponent>();
            var uiProviders = world.GetPool<BusinessUIProviderComponent>();

            foreach (var entity in incomeUpgradedFilter)
            {
                ref var business = ref businesses.Get(entity);
                ref var uiProvider = ref uiProviders.Get(entity);

                uiProvider.IncomeView.SetIncome(business.Income);

                world.GetPool<IncomeUpgradedEvent>().Del(entity);
            }
        }
    }
}