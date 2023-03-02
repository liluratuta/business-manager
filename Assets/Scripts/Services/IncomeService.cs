using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Services
{
    public class IncomeService
    {
        private readonly EcsWorld _world;
        private readonly StaticDataService _staticDataService;

        public IncomeService(EcsWorld world, StaticDataService staticDataService)
        {
            _world = world;
            _staticDataService = staticDataService;
        }

        public double Income(int entity)
        {
            ref var levelComponent = ref _world.GetPool<LevelComponent>().Get(entity);
            ref var business = ref _world.GetPool<BusinessComponent>().Get(entity);
            return levelComponent.Level * _staticDataService.ForBusinessID(business.BusinessID).BaseIncome;
        }
    }
}