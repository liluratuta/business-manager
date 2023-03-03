using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;
using Scripts.Services;

namespace Scripts.Systems.Business
{
    public class ImprovementSystem : IEcsRunSystem
    {
        private readonly StaticDataService _staticDataService;

        public ImprovementSystem(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var requestFilter = world.Filter<ImprovementRequest>().End();

            foreach (var entity in requestFilter)
            {
                ref var improvementsComponent = ref world.GetPool<ImprovementsComponent>().Get(entity);
                ref var request = ref world.GetPool<ImprovementRequest>().Get(entity);

                var improvementID = request.ImprovementID;
                
                if (improvementsComponent.Purchased.Exists(x => x == improvementID))
                    continue;

                if (!TryPayForImprovement(world, request.BusinessID, request.ImprovementID))
                    continue;

                improvementsComponent.Purchased.Add(request.ImprovementID);

                world.GetPool<ImprovementEvent>().Add(entity);
            }
        }

        private bool TryPayForImprovement(EcsWorld world, BusinessID businessID, int improvementID)
        {
            var cost = _staticDataService.ForBusinessID(businessID).Improvements[improvementID].Cost;
            var walletEntity = world.FirstEntityWith<WalletComponent>();

            if (world.GetPool<WalletComponent>().Get(walletEntity).Amount < cost)
                return false;
            
            world.GetPool<WalletTransactionsComponent>().Get(walletEntity).Transactions.Add(-cost);
            return true;
        }
    }
}