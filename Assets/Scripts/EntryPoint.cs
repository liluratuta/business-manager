using System;
using Scripts.Infrastructure.States;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private Game _game;
        private AllServices _services;
        
        private void Awake()
        {
            _services = AllServices.Container;
            RegisterMonoServices();
            _game = new Game(_services);
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void RegisterMonoServices()
        {
            _services.RegisterSingle<IUpdater>(gameObject.AddComponent<MonoUpdater>());
        }

        private void OnDestroy()
        {
            _game.Complete();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                _game.OnPause();
        }
    }
}