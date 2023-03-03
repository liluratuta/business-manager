using Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class ImprovementButtonView : MonoBehaviour
    {
        private const string CostLocaleKey = "improvement_cost";
        private const string PurchasedLocaleKey = "improvement_purchased";
        private const string MultiplierLocaleKey = "improvement_multiplier";
        
        public Button Button;
        public TMP_Text NameLabel;
        public TMP_Text MultiplierLabel;
        public TMP_Text CostLabel;

        private ImprovementService _improvementService;
        private BusinessID _businessID;
        private int _improvementID;
        private LocalizationService _localizationService;
        private bool _isPurchased;

        public void Init(ImprovementService improvementService,
            LocalizationService localizationService)
        {
            _improvementService = improvementService;
            _localizationService = localizationService;
        }

        public void SetPurchased(bool isPurchased)
        {
            Button.interactable = !isPurchased;
            _isPurchased = isPurchased;

            if (isPurchased)
                CostLabel.text = _localizationService.Localize(PurchasedLocaleKey);
        }

        public void SetBusinessID(BusinessID businessID) => 
            _businessID = businessID;

        public void SetImprovementID(int improvementID) =>
            _improvementID = improvementID;

        private void OnEnable() => 
            Button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            Button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            _improvementService.Perform(_businessID, _improvementID);

        public void SetNameKey(string nameKey) => 
            NameLabel.text = $"{_localizationService.Localize(nameKey)}";

        public void SetCost(double cost) => 
            CostLabel.text = $"{_localizationService.Localize(CostLocaleKey)}: {cost}";

        public void SetMultiplier(double multiplier) => 
            MultiplierLabel.text = $"{_localizationService.Localize(MultiplierLocaleKey)}: {multiplier:P}";

        public bool IsInteractable() => 
            Button.interactable;

        public void SetInteractable(bool interactable)
        {
            if (_isPurchased)
                return;

            Button.interactable = interactable;
        }
    }
}