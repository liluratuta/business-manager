using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;
using Scripts.Services;
using Scripts.Systems.Business;
using Scripts.Systems.Wallet;
using Scripts.Views.Factories;

namespace Scripts.Infrastructure.States
{
    public class RegisterEcsSystemsState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;

        public RegisterEcsSystemsState(GameStateMachine gameStateMachine, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;
        }

        public void Enter()
        {
            var systems = _services.Single<EcsSystemsProvider>().Systems;
            RegisterSystems(systems);
            systems.Init();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void RegisterSystems(EcsSystems ecsSystems)
        {
            ecsSystems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                    
                .Add(new CreateWalletSystem(_services.Single<ProgressService>()))
                .Add(new CreateBusinessesSystem(
                    _services.Single<LevelCostService>(),
                    _services.Single<StaticDataService>(),
                    _services.Single<IncomeService>(),
                    _services.Single<ProgressService>()))

                .OneFrame<WalletChangedEvent>()
                .Add(new WalletTransactionsApproveSystem())

                .Add(new CreateBusinessesUISystem(
                    _services.Single<UIFactory>(),
                    _services.Single<StaticDataService>()))

                .Add(new LevelUpdateUISystem())

                .OneFrame<LevelUpgradedEvent>()
                .Add(new LevelUpSystem(_services.Single<LevelCostService>()))
                .OneFrame<LevelUpRequest>()
                
                .OneFrame<ImprovementEvent>()
                .Add(new ImprovementSystem(_services.Single<StaticDataService>()))
                .OneFrame<ImprovementRequest>()
                
                .OneFrame<IncomeCollectRequest>()
                .Add(new TimerSystem(_services.Single<DeltaTimeProvider>()))
                
                .Add(new IncomeCollectSystem())

                .Add(new IncomeUpgradeSystem(_services.Single<IncomeService>()))

                .Add(new WalletUISystem())

                .Add(new LevelButtonStateUISystem())

                .Add(new TimerUISystem())

                .Add(new IncomeUpgradeUISystem())

                .Add(new BusinessProgressUpdateSystem(_services.Single<ProgressService>()))
                .Add(new ImprovementButtonStateUISystem(_services.Single<StaticDataService>()))
                .Add(new WalletProgressUpdateSystem(_services.Single<ProgressService>()));
        }

        public void Exit()
        {
        }
    }
}