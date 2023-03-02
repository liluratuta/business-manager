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

            var levelUpgradedFilter = world.Filter<LevelUpgradedEvent>().End();
            var businesses = world.GetPool<BusinessComponent>();

            foreach (var entity in levelUpgradedFilter)
            {
                ref var business = ref businesses.Get(entity);
                business.Income = _incomeService.Income(entity);

                world.GetPool<IncomeUpgradedEvent>().Add(entity);
            }
        }
    }
}