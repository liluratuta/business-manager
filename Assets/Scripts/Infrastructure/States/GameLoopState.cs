using Leopotam.EcsLite;
using Scripts.Services;

namespace Scripts.Infrastructure.States
{
    public class GameLoopState : IState, IUpdateable
    {
        private readonly AllServices _services;
        
        private EcsSystems _systems;

        public GameLoopState(AllServices services)
        {
            _services = services;
        }

        public void Enter() => 
            _services.Single<IUpdater>().Register(this);

        public void Exit() => 
            _services.Single<IUpdater>().Unregister(this);

        public void Update()
        {
            if (_systems == null)
                _systems = _services.Single<EcsSystemsProvider>().Systems;
            
            _systems.Run();
        }
    }
}