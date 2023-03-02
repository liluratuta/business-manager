using System;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "BusinessData", menuName = "Game/StaticData/BusinessData")]
    public class BusinessStaticData : ScriptableObject
    {
        public BusinessID BusinessID;
        public string NameKey;
        
        [Range(0, 50)]
        public float RevenueDelay;
        
        public double BaseCost;
        public double BaseIncome;

        public BusinessImprovement[] Improvements;
    }
}