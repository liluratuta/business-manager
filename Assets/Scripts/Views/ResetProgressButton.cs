using Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class ResetProgressButton : MonoBehaviour
    {
        private const string ProgressResetSceneName = "ResetProgressScene";
        private const string ProgressResetLocaleKey = "reset_progress";

        public Button Button;
        public TMP_Text Label;
        
        private LocalizationService _localizationService;

        public void Init(LocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        private void Start()
        {
            Label.text = _localizationService.Localize(ProgressResetLocaleKey);
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
        }

        private void OnDisable() => 
            Button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            SceneManager.LoadScene(ProgressResetSceneName);
    }
}