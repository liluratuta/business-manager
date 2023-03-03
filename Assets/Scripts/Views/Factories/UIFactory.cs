using System.Collections.Generic;
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
        private const string ImprovementButtonPath = "UI/ImprovementButton";

        private readonly AssetsProvider _assetsProvider;
        private readonly EcsWorld _world;
        private readonly LocalizationService _localizationService;
        private readonly LevelUpService _levelUpService;
        private readonly StaticDataService _staticDataService;
        private readonly ImprovementService _improvementService;

        private Transform _uiRoot;

        public UIFactory(AssetsProvider assetsProvider,
            EcsSystemsProvider ecsSystemsProvider,
            LocalizationService localizationService,
            LevelUpService levelUpService,
            StaticDataService staticDataService,
            ImprovementService improvementService)
        {
            _assetsProvider = assetsProvider;
            _world = ecsSystemsProvider.Systems.GetWorld();
            _localizationService = localizationService;
            _levelUpService = levelUpService;
            _staticDataService = staticDataService;
            _improvementService = improvementService;
        }

        public BusinessView CreateBusinessView(int businessEntity, Transform container)
        {
            var prefab = _assetsProvider.FromResources(BusinessViewPath);
            var view = Object.Instantiate(prefab, container);

            ref var uiProvider = ref _world.GetPool<BusinessUIProviderComponent>().Get(businessEntity);
            ref var businessComponent = ref _world.GetPool<BusinessComponent>().Get(businessEntity);
            var businessData = _staticDataService.ForBusinessID(businessComponent.BusinessID);

            var levelView = view.GetComponentInChildren<LevelView>();
            levelView.Init(_localizationService);
            uiProvider.LevelView = levelView;

            var levelUpButtonView = view.GetComponentInChildren<LevelUpButtonView>();
            levelUpButtonView.Init(_levelUpService, _localizationService);
            levelUpButtonView.SetBusinessID(businessComponent.BusinessID);
            uiProvider.LevelUpButtonView = levelUpButtonView;

            var timerView = view.GetComponentInChildren<TimerView>();
            timerView.SetTimerGoal(businessData.RevenueDelay);
            uiProvider.TimerView = timerView;

            var incomeView = view.GetComponentInChildren<IncomeView>();
            incomeView.Init(_localizationService);
            uiProvider.IncomeView = incomeView;

            var businessNameView = view.GetComponentInChildren<BusinessNameView>();
            businessNameView.Init(_localizationService);
            uiProvider.NameView = businessNameView;

            var improvementsContainer = view.GetComponentInChildren<ImprovementsContainerView>().transform;
            var improvementButtonCount = businessData.Improvements.Length;

            uiProvider.ImprovementButtons = new List<ImprovementButtonView>();
                
            for (var improvementID = 0; improvementID < improvementButtonCount; improvementID++)
            {
                var improvementButton =
                    CreateImprovementButtonView(improvementsContainer, businessComponent.BusinessID, improvementID);
                uiProvider.ImprovementButtons.Add(improvementButton);
            }

            return view.GetComponent<BusinessView>();
        }

        private ImprovementButtonView CreateImprovementButtonView(Transform container, BusinessID businessID, int improvementID)
        {
            var prefab = _assetsProvider.FromResources(ImprovementButtonPath);
            var view = Object.Instantiate(prefab, container);
            var buttonView = view.GetComponent<ImprovementButtonView>();

            buttonView.Init(_improvementService, _localizationService);
            buttonView.SetBusinessID(businessID);
            buttonView.SetImprovementID(improvementID);

            return buttonView;
        }

        public BusinessesWindow CreateBusinessesWindow()
        {
            var prefab = _assetsProvider.FromResources(BusinessesWindowPath);
            var view = Object.Instantiate(prefab.GetComponent<BusinessesWindow>(), _uiRoot);
            var walletEntity = _world.FirstEntityWith<WalletComponent>();
            ref var uiProvider = ref _world.GetPool<WalletUIProviderComponent>().Get(walletEntity);

            var walletView = view.GetComponentInChildren<WalletView>();
            walletView.Init(_localizationService);
            uiProvider.WalletView = walletView;

            var resetProgressButton = view.GetComponentInChildren<ResetProgressButton>();
            resetProgressButton.Init(_localizationService);

            return view;
        }

        public void CreateUIRoot()
        {
            var prefab = _assetsProvider.FromResources(UIRootPath);
            _uiRoot = Object.Instantiate(prefab).transform;
        }
    }
}