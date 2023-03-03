using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;
using Scripts.Services;

namespace Scripts.Systems.Business
{
    public class ImprovementButtonStateUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly StaticDataService _staticDataService;

        public ImprovementButtonStateUISystem(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var businessesFilter = world.Filter<BusinessComponent>().End();
            var uiProviders = world.GetPool<BusinessUIProviderComponent>();
            var businesses = world.GetPool<BusinessComponent>();
            var walletEntity = world.FirstEntityWith<WalletComponent>();
            ref var wallet = ref world.GetPool<WalletComponent>().Get(walletEntity);
            
            foreach (var entity in businessesFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var businessComponent = ref businesses.Get(entity);

                var businessData = _staticDataService.ForBusinessID(businessComponent.BusinessID);

                for (var improvementID = 0; improvementID < uiProvider.ImprovementButtons.Count; improvementID++)
                {
                    var improvementButton = uiProvider.ImprovementButtons[improvementID];
                    var improvementData = businessData.Improvements[improvementID];
                    var isBuyPossible = wallet.Amount >= improvementData.Cost;
                    
                    if (isBuyPossible == improvementButton.IsInteractable())
                        continue;

                    improvementButton.SetInteractable(isBuyPossible);
                }
            }
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var businessesFilter = world.Filter<BusinessComponent>().End();
            var uiProviders = world.GetPool<BusinessUIProviderComponent>();
            var businesses = world.GetPool<BusinessComponent>();
            var walletEntity = world.FirstEntityWith<WalletComponent>();
            ref var wallet = ref world.GetPool<WalletComponent>().Get(walletEntity);
            
            foreach (var entity in businessesFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var businessComponent = ref businesses.Get(entity);

                var businessData = _staticDataService.ForBusinessID(businessComponent.BusinessID);

                for (var improvementID = 0; improvementID < uiProvider.ImprovementButtons.Count; improvementID++)
                {
                    var improvementButton = uiProvider.ImprovementButtons[improvementID];
                    var improvementData = businessData.Improvements[improvementID];
                    var isBuyPossible = wallet.Amount >= improvementData.Cost;
                    
                    if (isBuyPossible == improvementButton.IsInteractable())
                        continue;

                    improvementButton.SetInteractable(isBuyPossible);
                }
            }

            var eventFilter = world.Filter<ImprovementEvent>().End();
            var improvements = world.GetPool<ImprovementsComponent>();
            
            foreach (var entity in eventFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var improvementsComponent = ref improvements.Get(entity);

                for (var improvementID = 0; improvementID < uiProvider.ImprovementButtons.Count; improvementID++)
                {
                    var isPurchased = improvementsComponent.Purchased.Exists(x => x == improvementID);
                    
                    if (!isPurchased)
                        continue;
                    
                    var improvementButton = uiProvider.ImprovementButtons[improvementID];
                    improvementButton.SetPurchased(true);
                }
            }
        }
    }
}