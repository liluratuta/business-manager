using Leopotam.EcsLite;
using Scripts.Infrastructure.States;
using Scripts.Services;

namespace Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        private EcsWorld _world;

        public Game(MonoUpdater monoUpdater)
        {
            _world = new EcsWorld();
            StateMachine = new GameStateMachine(_world, monoUpdater);
        }

        public void Complete()
        {
            StateMachine.Enter<CompleteGameState>();
        }
    }
}