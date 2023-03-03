using Leopotam.EcsLite;
using Scripts.Extensions;
using Scripts.Infrastructure.States;
using Scripts.Services;

namespace Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        
        private readonly AllServices _services;
        private readonly EcsWorld _world;

        public Game(AllServices services)
        {
            _world = new EcsWorld();
            StateMachine = new GameStateMachine(_world, services);
            _services = services;
        }

        public void Complete()
        {
            StateMachine.Enter<CompleteGameState>();
        }

        public void OnPause()
        {
            _services.Single<EntitiesSaveService>().SendSaveRequest();
            _services.Single<EcsSystemsProvider>().Systems.Save();
            _services.Single<ProgressService>().SaveProgress();
        }
    }
}