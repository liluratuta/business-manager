using Leopotam.EcsLite;
using Scripts.Infrastructure.States;
using Scripts.Services;

namespace Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        private EcsWorld _world;

        public Game(AllServices services)
        {
            _world = new EcsWorld();
            StateMachine = new GameStateMachine(_world, services);
        }

        public void Complete()
        {
            StateMachine.Enter<CompleteGameState>();
        }
    }
}