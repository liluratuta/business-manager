using Leopotam.EcsLite;
using Scripts.Components;
using Scripts.Components.Business;
using Scripts.Components.Wallet;
using Scripts.Extensions;

namespace Scripts.Services
{
    public class EntitiesSaveService
    {
        private readonly EcsWorld _world;

        public EntitiesSaveService(EcsWorld world)
        {
            _world = world;
        }

        public void SendSaveRequest()
        {
            SendToBusinesses();
            SendToWallet();
        }

        private void SendToWallet()
        {
            var walletEntity = _world.FirstEntityWith<WalletComponent>();
            _world.GetPool<SaveRequest>().Add(walletEntity);
        }

        private void SendToBusinesses()
        {
            var businessFilter = _world.Filter<BusinessComponent>().End();

            foreach (var entity in businessFilter)
            {
                _world.GetPool<SaveRequest>().Add(entity);
            }
        }
    }
}