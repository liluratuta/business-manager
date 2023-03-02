using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Services
{
    public class MonoUpdater : MonoBehaviour, IUpdater
    {
        private LinkedList<IUpdateable> _updateables = new LinkedList<IUpdateable>();

        private void Update()
        {
            foreach (var updateable in _updateables)
            {
                updateable.Update();
            }
        }
        
        public void Register(IUpdateable updateable) => 
            _updateables.AddLast(updateable);

        public void Unregister(IUpdateable updateable) => 
            _updateables.Remove(updateable);
    }
}