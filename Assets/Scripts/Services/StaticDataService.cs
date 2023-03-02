using Scripts.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Services
{
    public class StaticDataService
    {
        private const string BusinessesPath = "StaticData/Businesses";
        private const string GameStartDataPath = "StaticData/GameStartData";

        private Dictionary<BusinessID, BusinessStaticData> _businesses;
        private GameStartStaticData _startData;

        public void LoadBusinesses()
        {
            _businesses = Resources.LoadAll<BusinessStaticData>(BusinessesPath)
                .ToDictionary(x => x.BusinessID, y => y);
        }

        public void LoadGameStartData()
        {
            _startData = Resources.Load<GameStartStaticData>(GameStartDataPath);
        }

        public BusinessStaticData ForBusinessID(BusinessID id) =>
            _businesses.TryGetValue(id, out BusinessStaticData data) ? data : null;

        public GameStartStaticData GameStartData() =>
            _startData;
    }
}