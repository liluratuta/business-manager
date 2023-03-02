using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Services
{
    public class LevelCostService : IService
    {
        private readonly EcsWorld _world;
        private readonly StaticDataService _staticDataService;

        public LevelCostService(EcsSystemsProvider ecsSystemsProvider, StaticDataService staticDataService)
        {
            _world = ecsSystemsProvider.Systems.GetWorld();
            _staticDataService = staticDataService;
        }

        public double Cost(int entity)
        {
            ref var levelComponent = ref _world.GetPool<LevelComponent>().Get(entity);
            return LevelCost(entity, levelComponent.Level);
        }

        public double NextLevelCost(int entity)
        {
            ref var levelComponent = ref _world.GetPool<LevelComponent>().Get(entity);
            return LevelCost(entity, levelComponent.Level + 1);
        }

        private double LevelCost(int entity, int level)
        {
            ref var business = ref _world.GetPool<BusinessComponent>().Get(entity);
            var businessData = _staticDataService.ForBusinessID(business.BusinessID);
            return level * businessData.BaseCost;
        }
    }
}