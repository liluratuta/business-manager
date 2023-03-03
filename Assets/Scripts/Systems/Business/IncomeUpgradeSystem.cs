using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Services;

namespace Scripts.Systems.Business
{
    public class IncomeUpgradeSystem : IEcsRunSystem
    {
        private readonly IncomeService _incomeService;

        public IncomeUpgradeSystem(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var businesses = world.GetPool<BusinessComponent>();

            var levelUpgradedFilter = world.Filter<LevelUpgradedEvent>().End();
            var improvementFilter = world.Filter<ImprovementEvent>().End();

            foreach (var entity in levelUpgradedFilter)
            {
                UpgradeIncome(businesses, entity, world);
            }
            
            foreach (var entity in improvementFilter)
            {
                UpgradeIncome(businesses, entity, world);
            }
        }

        private void UpgradeIncome(EcsPool<BusinessComponent> businesses, int entity, EcsWorld world)
        {
            ref var business = ref businesses.Get(entity);
            business.Income = _incomeService.Income(entity);

            if (world.GetPool<IncomeUpgradedEvent>().Has(entity))
                return;
            
            world.GetPool<IncomeUpgradedEvent>().Add(entity);
        }
    }
}