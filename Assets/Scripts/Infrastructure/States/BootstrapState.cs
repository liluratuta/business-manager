using Leopotam.EcsLite;
using Scripts.Services;
using Scripts.Views.Factories;

namespace Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;
        private readonly EcsWorld _ecsWorld;
        private readonly MonoUpdater _monoUpdater;

        public BootstrapState(GameStateMachine gameStateMachine, AllServices services, EcsWorld ecsWorld)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;
            _ecsWorld = ecsWorld;
        }

        public void Enter()
        {
            RegisterServices();
            
            _services.Single<StaticDataService>().LoadBusinesses();
            _services.Single<StaticDataService>().LoadGameStartData();
            _services.Single<ProgressService>().LoadProgress();
            
            _gameStateMachine.Enter<RegisterEcsSystemsState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle(_gameStateMachine);
            _services.RegisterSingle(new EcsSystemsProvider(new EcsSystems(_ecsWorld)));
            _services.RegisterSingle(new StaticDataService());
            _services.RegisterSingle(new DeltaTimeProvider());
            _services.RegisterSingle(new AssetsProvider());
            _services.RegisterSingle(new LocalizationService());
            _services.RegisterSingle(new LevelUpService(_services.Single<EcsSystemsProvider>()));
            
            _services.RegisterSingle(new UIFactory(
                _services.Single<AssetsProvider>(), 
                _services.Single<EcsSystemsProvider>(),
                _services.Single<LocalizationService>(),
                _services.Single<LevelUpService>(),
                _services.Single<StaticDataService>()));
            
            _services.RegisterSingle(new LevelCostService(_services.Single<EcsSystemsProvider>(),
                _services.Single<StaticDataService>()));
            
            _services.RegisterSingle(new IncomeService(_services.Single<EcsSystemsProvider>(),
                _services.Single<StaticDataService>()));
            
            _services.RegisterSingle(new SaveLoadService());
            _services.RegisterSingle(new ProgressService(_services.Single<SaveLoadService>(),
                _services.Single<StaticDataService>()));
            
            _services.RegisterSingle(new EntitiesSaveService(_services.Single<EcsSystemsProvider>()));
        }
    }
}