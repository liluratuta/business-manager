using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LocalizationData", menuName = "Game/StaticData/LocalizationData")]
    public class LocalizationStaticData : ScriptableObject
    {
        public LocalizationType LocalizationType;
        public List<LocalizationKeyValue> KeyValuePairs = new List<LocalizationKeyValue>();
    }
}