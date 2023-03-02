using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Scripts.Systems
{
    sealed class OnFrameSystem<TComponent> : IEcsRunSystem where TComponent : struct
    {
        private readonly List<int> _entitiesToDel = new List<int>();

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var pool = world.GetPool<TComponent>();

            _entitiesToDel.ForEach(e => pool.Del(e));
            _entitiesToDel.Clear();

            var filter = world.Filter<TComponent>().End();

            foreach (var entity in filter)
                _entitiesToDel.Add(entity);
        }
    }
}