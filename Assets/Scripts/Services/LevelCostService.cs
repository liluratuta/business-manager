using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Services
{
    public class LevelCostService
    {
        private readonly EcsWorld _world;
        private readonly StaticDataService _staticDataService;

        public LevelCostService(EcsWorld world, StaticDataService staticDataService)
        {
            _world = world;
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