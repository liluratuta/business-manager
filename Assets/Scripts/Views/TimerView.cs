using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class TimerView : MonoBehaviour
    {
        public Image Bar;
        public TMP_Text ProgressValue;

        private float _timerGoal;

        public void SetTimerGoal(float value) => 
            _timerGoal = value;

        public void SetCurrentTime(float time)
        {
            var progress = _timerGoal == 0 ? 1 : time / _timerGoal;
            Bar.fillAmount = progress;
            ProgressValue.text = $"{string.Format("{0:0.00}", (1 - progress) * _timerGoal)}sec";
        }
    }
}