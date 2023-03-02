using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class ResetProgressButton : MonoBehaviour
    {
        private const string ProgressResetSceneName = "ResetProgressScene";
        
        public Button Button;

        private void OnEnable() => 
            Button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            Button.onClick.RemoveListener(OnClick);

        private void OnClick() => 
            SceneManager.LoadScene(ProgressResetSceneName);
    }
}