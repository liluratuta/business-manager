using System.Collections.Generic;
using Leopotam.EcsLite;
using Scripts.Components;
using Scripts.Components.Business;
using Scripts.Extensions;
using Scripts.Services;

namespace Scripts.Systems.Business
{
    public class BusinessProgressUpdateSystem : IEcsDestroySystem
    {
        private readonly ProgressService _progressService;

        public BusinessProgressUpdateSystem(ProgressService progressService)
        {
            _progressService = progressService;
        }

        public void Destroy(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var saveRequestFilter = world.Filter<SaveRequest>().Inc<BusinessComponent>().End();

            if (saveRequestFilter.GetEntitiesCount() == 0)
                return;

            var businesses = world.GetPool<BusinessComponent>();
            var levelComponents = world.GetPool<LevelComponent>();
            var timers = world.GetPool<TimerComponent>();
            var improvements = world.GetPool<ImprovementsComponent>();

            foreach (var entity in saveRequestFilter)
            {
                ref var business = ref businesses.Get(entity);
                ref var levelComponent = ref levelComponents.Get(entity);
                ref var timer = ref timers.Get(entity);
                ref var improvementsComponent = ref improvements.Get(entity);

                var businessProgress = _progressService.ForBusinessID(business.BusinessID);
                businessProgress.Level = levelComponent.Level;
                businessProgress.TimerProgress = timer.CurrentTime;
                businessProgress.PurchasedImprovements = new List<int>(improvementsComponent.Purchased);

                world.GetPool<SaveRequest>().Del(entity);
            }
        }
    }
}