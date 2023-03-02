using Leopotam.EcsLite;

namespace Scripts.Systems
{
    sealed class OnFrameSystem<TComponent> : IEcsRunSystem where TComponent : struct
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<TComponent>();

            var filter = world.Filter<TComponent>().End();

            foreach (var entity in filter)
                pool.Del(entity);
        }
    }
}