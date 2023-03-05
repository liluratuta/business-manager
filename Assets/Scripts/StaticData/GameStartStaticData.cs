using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "GameStartData", menuName = "Game/StaticData/GameStartData")]
    public class GameStartStaticData : ScriptableObject
    {
        public double StartBalance;

        public List<BusinessID> Businesses = new List<BusinessID>();

        public List<BusinessStartLevel> BusinessesStartLevel = new List<BusinessStartLevel>();
    }
}