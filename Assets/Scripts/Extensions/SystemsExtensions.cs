using Leopotam.EcsLite;
using Scripts.Systems;

namespace Scripts.Extensions
{
    public static class SystemsExtensions
    {
        public static IEcsSystems OneFrame<TComponent>(this IEcsSystems systems) where TComponent : struct
        {
            return systems.Add(new OnFrameSystem<TComponent>());
        }

        public static int FirstEntityWith<TComponent>(this EcsWorld ecsWorld) where TComponent : struct
        {
            EcsFilter filter = ecsWorld.Filter<TComponent>().End();

            foreach (int entity in filter)
            {
                return entity;
            }

            return -1;
        }

        public static void Save(this EcsSystems systems)
        {
            var allSystems = systems.GetAllSystems();

            foreach (var system in allSystems)
                (system as IEcsSaveSystem)?.Save(systems);
        }
    }
}