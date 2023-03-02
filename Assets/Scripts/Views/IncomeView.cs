using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class IncomeView : MonoBehaviour
    {
        public TMP_Text Label;

        public void SetIncome(double income) => 
            Label.text = $"Income: {income}";
    }
}