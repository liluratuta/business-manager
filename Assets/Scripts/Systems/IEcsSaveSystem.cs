using Leopotam.EcsLite;

namespace Scripts.Systems
{
    public interface IEcsSaveSystem : IEcsSystem
    {
        void Save(IEcsSystems systems);
    }
}