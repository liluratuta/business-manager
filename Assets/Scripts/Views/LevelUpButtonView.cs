using Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class LevelUpButtonView : MonoBehaviour
    {
        private const string LevelUpLocaleKey = "level_up";
        private const string CostLocaleKey = "cost";
        
        public Button Button;
        public TMP_Text Label;
        
        private LevelUpService _levelUpService;
        private BusinessID _businessID;
        private LocalizationService _localizationService;

        public void Init(LevelUpService levelUpService, LocalizationService localizationService)
        {
            _levelUpService = levelUpService;
            _localizationService = localizationService;
        }

        public void SetBusinessID(BusinessID businessID) => 
            _businessID = businessID;

        public void SetLevelUpPossible(bool isPossible) => 
            Button.interactable = isPossible;

        public void SetCost(double cost) => 
            Label.text = $"{_localizationService.Localize(LevelUpLocaleKey)}\n{_localizationService.Localize(CostLocaleKey)}: {cost}$";

        private void OnEnable() => 
            Button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            Button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _levelUpService.LevelUp(_businessID);
        }
    }
}