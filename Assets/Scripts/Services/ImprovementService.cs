using Leopotam.EcsLite;

namespace Scripts.Services
{
    public class ImprovementService
    {
        private readonly EcsWorld _world;

        public ImprovementService(EcsWorld world)
        {
            _world = world;
        }

        public void Perform(BusinessID businessID, int improvementID)
        {
            throw new System.NotImplementedException();
        }
    }
}