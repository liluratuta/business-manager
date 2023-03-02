using UnityEngine;

namespace Scripts.Services
{
    public class AssetsProvider : IService
    {
        public GameObject FromResources(string path) =>
            Resources.Load<GameObject>(path);
    }
}