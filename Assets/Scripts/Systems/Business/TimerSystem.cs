using Leopotam.EcsLite;
using Scripts.Components.Business;

namespace Scripts.Systems.Business
{
    public class TimerSystem : IEcsRunSystem
    {
        private readonly DeltaTimeProvider _deltaTimeProvider;

        public TimerSystem(DeltaTimeProvider deltaTimeProvider)
        {
            _deltaTimeProvider = deltaTimeProvider;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var timerFilter = world.Filter<TimerComponent>().Inc<IsWorking>().End();
            var timers = world.GetPool<TimerComponent>();

            foreach (var entity in timerFilter)
            {
                ref var timer = ref timers.Get(entity);
                timer.CurrentTime += _deltaTimeProvider.Value;

                if (timer.CurrentTime >= timer.TimerGoal)
                {
                    timer.CurrentTime = timer.CurrentTime - timer.TimerGoal;
                    world.GetPool<IncomeCollectRequest>().Add(entity);
                }
            }
        }
    }
}