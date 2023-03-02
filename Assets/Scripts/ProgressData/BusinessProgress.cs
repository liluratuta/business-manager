using System;
using System.Collections.Generic;

namespace Scripts.ProgressData
{
    [Serializable]
    public class BusinessProgress
    {
        public BusinessID BusinessID;
        public int Level;
        public List<int> PurchasedImprovements;
        public float TimerProgress;
    }
}