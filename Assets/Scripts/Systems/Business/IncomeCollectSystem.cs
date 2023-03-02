using Leopotam.EcsLite;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;

namespace Scripts.Systems.Business
{
    public class IncomeCollectSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var collectRequestFilter = world.Filter<IncomeCollectRequest>().End();
            var businesses = world.GetPool<BusinessComponent>();
            var walletEntity = world.FirstEntityWith<WalletComponent>();

            foreach (var entity in collectRequestFilter)
            {
                ref var business = ref businesses.Get(entity);

                ref var transactionsComponent = ref world.GetPool<WalletTransactionsComponent>().Get(walletEntity);
                transactionsComponent.Transactions.Add(business.Income);
            }
        }
    }
}