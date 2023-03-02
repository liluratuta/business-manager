using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;
using Scripts.Services;
using Scripts.Systems.Business;
using Scripts.Systems.Wallet;
using Scripts.Views.Factories;
using UnityEngine;

namespace Scripts
{
    sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;
        private StaticDataService _staticDataService;
        private DeltaTimeProvider _deltaTimeProvider;
        private AssetsProvider _assetsProvider;
        private UIFactory _uiFactory;
        private LocalizationService _localizationService;
        private LevelUpService _levelUpService;
        private LevelCostService _levelCostService;
        private IncomeService _incomeService;
        private ProgressService _progressService;
        private SaveLoadService _saveLoadService;
        private EntitiesSaveService _entitiesSaveService;

        private void Awake()
        {
            _staticDataService = new StaticDataService();
            _staticDataService.LoadBusinesses();
            _staticDataService.LoadGameStartData();

            _deltaTimeProvider = new DeltaTimeProvider();
            _assetsProvider = new AssetsProvider();
            _localizationService = new LocalizationService();
        }

        private void Start()
        {
            _world = new EcsWorld();

            _levelUpService = new LevelUpService(_world);
            _uiFactory = new UIFactory(_assetsProvider, _world, _localizationService, _levelUpService, _staticDataService);
            _levelCostService = new LevelCostService(_world, _staticDataService);
            _incomeService = new IncomeService(_world, _staticDataService);
            _saveLoadService = new SaveLoadService();
            _progressService = new ProgressService(_saveLoadService, _staticDataService);
            _progressService.LoadProgress();
            _entitiesSaveService = new EntitiesSaveService(_world);

            _systems = new EcsSystems(_world);
            _systems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif

                .Add(new CreateWalletSystem(_progressService))
                .Add(new CreateBusinessesSystem(_levelCostService, _staticDataService, _incomeService, _progressService))
                
                .Add(new WalletTransactionsApproveSystem())

                .Add(new CreateBusinessesUISystem(_uiFactory, _staticDataService))
                .Add(new LevelUpdateUISystem())

                .OneFrame<LevelUpgradedEvent>()
                .OneFrame<LevelUpRequest>()
                .Add(new LevelUpSystem(_levelCostService))

                .Add(new TimerSystem(_deltaTimeProvider))
                .OneFrame<IncomeCollectRequest>()
                .Add(new IncomeCollectSystem())

                .Add(new IncomeUpgradeSystem(_incomeService))

                .Add(new WalletUISystem())
                .OneFrame<WalletChangedEvent>()

                .Add(new LevelButtonStateUISystem())

                .Add(new TimerUISystem())

                .Add(new IncomeUpgradeUISystem())

                .Add(new BusinessProgressUpdateSystem(_progressService))
                .Add(new WalletProgressUpdateSystem(_progressService))

                .Init();
        }

        private void Update() => 
            _systems?.Run();

        private void OnDestroy()
        {
            _entitiesSaveService.SendSaveRequest();

            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }

            _progressService.SaveProgress();
        }
    }
}