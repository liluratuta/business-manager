using Scripts.Services;
using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class BusinessNameView : MonoBehaviour
    {
        public TMP_Text NameLabel;
        
        private LocalizationService _localizationService;

        public void Init(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public void SetNameKey(string nameKey) => 
            NameLabel.text = _localizationService.Localize(nameKey);
    }
}