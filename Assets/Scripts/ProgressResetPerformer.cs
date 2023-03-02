using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure
{
    public class ProgressResetPerformer : MonoBehaviour
    {
        private const string ProgressKey = "Progress";
        private const string GameSceneName = "GameScene";

        private void Awake()
        {
            PlayerPrefs.SetString(ProgressKey, string.Empty);
            SceneManager.LoadScene(GameSceneName);
        }
    }
}