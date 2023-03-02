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

            foreach (var entity in businessesFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var businessComponent = ref businesses.Get(entity);
                ref var timer = ref timers.Get(entity);

                uiProvider.NameView.SetNameKey(_staticDataService.ForBusinessID(businessComponent.BusinessID).NameKey);
                uiProvider.TimerView.SetCurrentTime(timer.CurrentTime);
            }
        }
    }
}