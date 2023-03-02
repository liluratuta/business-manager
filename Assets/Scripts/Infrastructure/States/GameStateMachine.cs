using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Scripts.Services;

namespace Scripts.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        
        private IState _activeState;

        public GameStateMachine(EcsWorld ecsWorld, MonoUpdater monoUpdater)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, AllServices.Container, ecsWorld, monoUpdater),
                [typeof(RegisterEcsSystemsState)] = new RegisterEcsSystemsState(this, AllServices.Container),
                [typeof(GameLoopState)] = new GameLoopState(AllServices.Container),
                [typeof(CompleteGameState)] = new CompleteGameState(AllServices.Container)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter();
        }
    }
}