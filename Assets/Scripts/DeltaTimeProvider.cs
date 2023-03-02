using Scripts.Services;
using UnityEngine;

namespace Scripts
{
    public class DeltaTimeProvider : IService
    {
        public float Value => Time.deltaTime;
    }
}