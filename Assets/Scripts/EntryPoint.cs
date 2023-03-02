using Scripts.Infrastructure.States;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private Game _game;
        private MonoUpdater _updater;
        
        private void Awake()
        {
            _updater = gameObject.AddComponent<MonoUpdater>();
            _game = new Game(_updater);
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void OnDestroy()
        {
            _game.Complete();
        }
    }
}