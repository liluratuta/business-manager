using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Views.Factories
{
    public class UIFactory : IService
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string BusinessesWindowPath = "UI/BusinessesWindow";
        private const string BusinessViewPath = "UI/BusinessView";

        private readonly AssetsProvider _assetsProvider;
        private readonly EcsWorld _world;
        private readonly LocalizationService _localizationService;
        private readonly LevelUpService _levelUpService;
        private readonly StaticDataService _staticDataService;
        private Transform _uiRoot;

        public UIFactory(AssetsProvider assetsProvider,
            EcsSystemsProvider ecsSystemsProvider,
            LocalizationService localizationService,
            LevelUpService levelUpService,
            StaticDataService staticDataService)
        {
            _assetsProvider = assetsProvider;
            _world = ecsSystemsProvider.Systems.GetWorld();
            _localizationService = localizationService;
            _levelUpService = levelUpService;
            _staticDataService = staticDataService;
        }

        public BusinessView CreateBusinessView(int businessEntity, Transform container)
        {
            var prefab = _assetsProvider.FromResources(BusinessViewPath);
            var view = Object.Instantiate(prefab, container);

            ref var uiProvider = ref _world.GetPool<BusinessUIProviderComponent>().Get(businessEntity);
            ref BusinessComponent businessComponent = ref _world.GetPool<BusinessComponent>().Get(businessEntity);

            uiProvider.LevelView = view.GetComponentInChildren<LevelView>();

            var levelUpButtonView = view.GetComponentInChildren<LevelUpButtonView>();
            levelUpButtonView.Init(_levelUpService);
            levelUpButtonView.SetBusinessID(businessComponent.BusinessID);
            uiProvider.LevelUpButtonView = levelUpButtonView;

            var timerView = view.GetComponentInChildren<TimerView>();
            timerView.SetTimerGoal(_staticDataService.ForBusinessID(businessComponent.BusinessID).RevenueDelay);
            uiProvider.TimerView = timerView;

            var incomeView = view.GetComponentInChildren<IncomeView>();
            uiProvider.IncomeView = incomeView;

            var businessNameView = view.GetComponentInChildren<BusinessNameView>();
            businessNameView.Init(_localizationService);
            uiProvider.NameView = businessNameView;

            return view.GetComponent<BusinessView>();
        }

        public BusinessesWindow CreateBusinessesWindow()
        {
            var prefab = _assetsProvider.FromResources(BusinessesWindowPath);
            var view = Object.Instantiate(prefab.GetComponent<BusinessesWindow>(), _uiRoot);

            var walletView = view.GetComponentInChildren<WalletView>();
            var walletEntity = _world.FirstEntityWith<WalletComponent>();
            _world.GetPool<WalletUIProviderComponent>().Get(walletEntity).WalletView = walletView;

            return view;
        }

        public void CreateUIRoot()
        {
            var prefab = _assetsProvider.FromResources(UIRootPath);
            _uiRoot = Object.Instantiate(prefab).transform;
        }
    }
}