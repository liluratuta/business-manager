using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class WalletView : MonoBehaviour
    {
        public TMP_Text Label;

        public void SetAmount(double amount)
        {
            Label.text = $"Balance: {amount}$";
        }
    }
}