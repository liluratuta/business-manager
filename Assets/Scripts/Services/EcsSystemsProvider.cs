using Leopotam.EcsLite;

namespace Scripts.Services
{
    public class EcsSystemsProvider : IService
    {
        public EcsSystems Systems { get; }

        public EcsSystemsProvider(EcsSystems systems)
        {
            Systems = systems;
        }
    }
}