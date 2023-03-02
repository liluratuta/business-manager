using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Scripts.Services;

namespace Scripts.Infrastructure.States
{
    public class GameStateMachine : IService
    {
        private readonly Dictionary<Type, IState> _states;
        
        private IState _activeState;

        public GameStateMachine(EcsWorld ecsWorld, AllServices services)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services, ecsWorld),
                [typeof(RegisterEcsSystemsState)] = new RegisterEcsSystemsState(this, services),
                [typeof(GameLoopState)] = new GameLoopState(services),
                [typeof(CompleteGameState)] = new CompleteGameState(services)
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