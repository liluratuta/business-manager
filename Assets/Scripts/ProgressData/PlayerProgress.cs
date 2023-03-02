using System;
using System.Collections.Generic;

namespace Scripts.ProgressData
{
    [Serializable]
    public class PlayerProgress
    {
        public double WalletAmount;
        public List<BusinessProgress> Businesses;
    }
}