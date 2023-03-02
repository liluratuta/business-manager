using Scripts.Services;
using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class WalletView : MonoBehaviour
    {
        private const string WalletAmountLocaleKey = "wallet_amount";
        
        public TMP_Text Label;
        
        private LocalizationService _localizationService;

        public void Init(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        
        public void SetAmount(double amount) => 
            Label.text = $"{_localizationService.Localize(WalletAmountLocaleKey)}: {amount}$";
    }
}