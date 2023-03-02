using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Services
{
    public class IncomeService : IService
    {
        private readonly EcsWorld _world;
        private readonly StaticDataService _staticDataService;

        public IncomeService(EcsSystemsProvider ecsSystemsProvider, StaticDataService staticDataService)
        {
            _world = ecsSystemsProvider.Systems.GetWorld();
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