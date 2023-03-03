using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Services;
using Scripts.Views.Factories;

namespace Scripts.Systems.Business
{
    public class CreateBusinessesUISystem : IEcsInitSystem
    {
        private readonly UIFactory _uiFactory;
        private readonly StaticDataService _staticDataService;

        public CreateBusinessesUISystem(UIFactory uiFactory, StaticDataService staticDataService)
        {
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var businessesFilter = world.Filter<BusinessComponent>().End();

            _uiFactory.CreateUIRoot();
            var window = _uiFactory.CreateBusinessesWindow();

            foreach (var entity in businessesFilter)
            {
                _uiFactory.CreateBusinessView(entity, window.Content);
                world.GetPool<LevelUpgradedEvent>().Add(entity);
            }

            var uiProviders = world.GetPool<BusinessUIProviderComponent>();
            var businesses = world.GetPool<BusinessComponent>();
            var timers = world.GetPool<TimerComponent>();
            var improvements = world.GetPool<ImprovementsComponent>();
            
            foreach (var entity in businessesFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var businessComponent = ref businesses.Get(entity);
                ref var timer = ref timers.Get(entity);
                ref var improvementsComponent = ref improvements.Get(entity);
                
                var businessData = _staticDataService.ForBusinessID(businessComponent.BusinessID);
                uiProvider.NameView.SetNameKey(businessData.NameKey);
                uiProvider.TimerView.SetCurrentTime(timer.CurrentTime);
                uiProvider.IncomeView.SetIncome(businessComponent.Income);

                for (var improvementID = 0; improvementID < uiProvider.ImprovementButtons.Count; improvementID++)
                {
                    var improvementButton = uiProvider.ImprovementButtons[improvementID];
                    var improvementData = businessData.Improvements[improvementID];
                    improvementButton.SetNameKey(improvementData.NameKey);
                    improvementButton.SetCost(improvementData.Cost);
                    improvementButton.SetMultiplier(improvementData.Multiplier);
                    improvementButton.SetPurchased(improvementsComponent.Purchased.Exists(x => x == improvementID));
                }
            }
        }
    }
}