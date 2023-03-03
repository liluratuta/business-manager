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
            ref var improvementsComponent = ref _world.GetPool<ImprovementsComponent>().Get(entity);

            var businessData = _staticDataService.ForBusinessID(business.BusinessID);

            double improvementsMultiplier = 1;

            for (var improvementID = 0; improvementID < businessData.Improvements.Length; improvementID++)
            {
                if (!improvementsComponent.Purchased.Exists(id => id == improvementID))
                    continue;

                improvementsMultiplier += businessData.Improvements[improvementID].Multiplier;
            }

            return levelComponent.Level * businessData.BaseIncome * improvementsMultiplier;
        }
    }
}