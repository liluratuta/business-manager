using System.Collections.Generic;
using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Services;

namespace Scripts.Systems.Business
{
    public class CreateBusinessesSystem : IEcsInitSystem
    {
        private readonly LevelCostService _levelCostService;
        private readonly StaticDataService _staticDataService;
        private readonly IncomeService _incomeService;
        private readonly ProgressService _progressService;

        public CreateBusinessesSystem(LevelCostService levelCostService,
            StaticDataService staticDataService,
            IncomeService incomeService,
            ProgressService progressService)
        {
            _levelCostService = levelCostService;
            _staticDataService = staticDataService;
            _incomeService = incomeService;
            _progressService = progressService;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            CreateBusiness(BusinessID.Business1, world);
            CreateBusiness(BusinessID.Business2, world);
            CreateBusiness(BusinessID.Business3, world);
            CreateBusiness(BusinessID.Business4, world);
            CreateBusiness(BusinessID.Business5, world);
        }

        private void CreateBusiness(BusinessID businessID, EcsWorld world)
        {
            var entity = world.NewEntity();

            var staticData = _staticDataService.ForBusinessID(businessID);
            var progress = _progressService.ForBusinessID(businessID);

            ref var levelComponent = ref world.GetPool<LevelComponent>().Add(entity);
            ref var businessComponent = ref world.GetPool<BusinessComponent>().Add(entity);
            ref var timerComponent = ref world.GetPool<TimerComponent>().Add(entity);
            ref var improvementsComponent = ref world.GetPool<ImprovementsComponent>().Add(entity);

            improvementsComponent.Purchased = new List<int>(progress.PurchasedImprovements);
            businessComponent.BusinessID = businessID;

            world.GetPool<BusinessUIProviderComponent>().Add(entity);
            world.GetPool<LevelButtonStateComponent>().Add(entity);

            timerComponent.CurrentTime = progress.TimerProgress;
            timerComponent.TimerGoal = staticData.RevenueDelay;

            levelComponent.Level = progress.Level;
            levelComponent.NextLevelCost = _levelCostService.NextLevelCost(entity);

            businessComponent.Income = _incomeService.Income(entity);


            if (levelComponent.Level > 0)
                world.GetPool<IsWorking>().Add(entity);
        }
    }
}