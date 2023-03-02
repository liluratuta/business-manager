using Scripts.Services;
using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class IncomeView : MonoBehaviour
    {
        private const string IncomeLocaleKey = "income";
        
        public TMP_Text Label;
        
        private LocalizationService _localizationService;

        public void Init(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public void SetIncome(double income) => 
            Label.text = $"{_localizationService.Localize(IncomeLocaleKey)}: {income}";
    }
}