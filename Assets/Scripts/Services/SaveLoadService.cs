using Scripts.ProgressData;
using UnityEngine;

namespace Scripts.Services
{
    public class SaveLoadService : IService
    {
        private const string ProgressKey = "Progress";

        public PlayerProgress LoadProgress()
        {
            var json = PlayerPrefs.GetString(ProgressKey);

            if (json == null)
                return null;

            var progress = JsonUtility.FromJson<PlayerProgress>(json);

            return progress;
        }

        public void SaveProgress(PlayerProgress progress)
        {
            var json = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString(ProgressKey, json);
        }
    }
}