using Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class ImprovementButtonView : MonoBehaviour
    {
        public Button Button;

        private ImprovementService _improvementService;
        private BusinessID _businessID;
        private int _improvementID;

        public void Init(ImprovementService improvementService)
        {
            _improvementService = improvementService;
        }

        public void SetInfo(BusinessID businessID, int improvementID)
        {
            _businessID = businessID;
            _improvementID = improvementID;
        }

        private void OnEnable() => 
            Button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            Button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            _improvementService.Perform(_businessID, _improvementID);
    }
}