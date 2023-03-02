using Scripts.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Services
{
    public class StaticDataService : IService
    {
        private const string BusinessesPath = "StaticData/Businesses";
        private const string GameStartDataPath = "StaticData/GameStartData";
        private const string LocalizationPath = "StaticData/Localizations";

        private Dictionary<string, string> _locales;

        private Dictionary<BusinessID, BusinessStaticData> _businesses;
        private GameStartStaticData _startData;

        public void LoadBusinesses()
        {
            _businesses = Resources.LoadAll<BusinessStaticData>(BusinessesPath)
                .ToDictionary(x => x.BusinessID, y => y);
        }

        public void LoadLocalization(LocalizationType localizationType)
        {
            var localizations = Resources.LoadAll<LocalizationStaticData>(LocalizationPath);
            var targetLocalizationData = localizations
                .FirstOrDefault(x => x.LocalizationType == localizationType);

            if (targetLocalizationData == null)
            {
                _locales = new Dictionary<string, string>();
                return;
            }

            _locales = targetLocalizationData
                .KeyValuePairs
                .ToDictionary(x => x.Key, y => y.Value);
        }

        public void LoadGameStartData() => 
            _startData = Resources.Load<GameStartStaticData>(GameStartDataPath);

        public BusinessStaticData ForBusinessID(BusinessID id) =>
            _businesses.TryGetValue(id, out BusinessStaticData data) ? data : null;

        public GameStartStaticData GameStartData() =>
            _startData;

        public string LocaleFor(string key) => 
            _locales.TryGetValue(key, out var value) ? value : null;
    }
}