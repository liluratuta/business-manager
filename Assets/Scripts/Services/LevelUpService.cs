using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Services
{
    public class LevelUpService
    {
        private readonly EcsWorld _world;

        public LevelUpService(EcsWorld world)
        {
            _world = world;
        }

        public void LevelUp(BusinessID businessID)
        {
            EcsFilter businessFilter = _world.Filter<BusinessComponent>().End();
            EcsPool<BusinessComponent> businesses = _world.GetPool<BusinessComponent>();

            foreach (int entity in businessFilter)
            {
                ref BusinessComponent business = ref businesses.Get(entity);

                if (business.BusinessID != businessID)
                    continue;

                _world.GetPool<LevelUpRequest>().Add(entity);
            }
        }
    }
}