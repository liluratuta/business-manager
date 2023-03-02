using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "GameStartData", menuName = "Game/StaticData/GameStartData")]
    public class GameStartStaticData : ScriptableObject
    {
        public double StartBalance;
    }
}