using UnityEngine;

namespace Scripts.Services
{
    public class AssetsProvider
    {
        public GameObject FromResources(string path) =>
            Resources.Load<GameObject>(path);
    }
}