using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Services
{
    public class ImprovementService : IService
    {
        private readonly EcsWorld _world;

        public ImprovementService(EcsSystemsProvider ecsSystemsProvider)
        {
            _world = ecsSystemsProvider.Systems.GetWorld();
        }

        public void Perform(BusinessID businessID, int improvementID)
        {
            var businessFilter = _world.Filter<BusinessComponent>().End();
            var businesses = _world.GetPool<BusinessComponent>();
            
            foreach (var entity in businessFilter)
            {
                ref var business = ref businesses.Get(entity);
                
                if (business.BusinessID != businessID)
                    continue;

                ref var request = ref _world.GetPool<ImprovementRequest>().Add(entity);
                request.BusinessID = businessID;
                request.ImprovementID = improvementID;
            }
        }
    }
}