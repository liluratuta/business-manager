using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Systems.Business
{
    public class TimerUISystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var timerFilter = world.Filter<TimerComponent>().Inc<IsWorking>().End();
            var timers = world.GetPool<TimerComponent>();
            var uiProviders = world.GetPool<BusinessUIProviderComponent>();

            foreach (var entity in timerFilter)
            {
                ref var uiProvider = ref uiProviders.Get(entity);
                ref var timer = ref timers.Get(entity);
                uiProvider.TimerView.SetCurrentTime(timer.CurrentTime);
            }
        }
    }
}