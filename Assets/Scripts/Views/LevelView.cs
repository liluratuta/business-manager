using System;
using Scripts.Services;
using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class LevelView : MonoBehaviour
    {
        private const string LevelLocaleKey = "level";
        
        public TMP_Text Label;
        
        private LocalizationService _localizationService;

        public void Init(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        
        public void SetLevel(int level) => 
            Label.text = $"{_localizationService.Localize(LevelLocaleKey)}: {level}";
    }
}