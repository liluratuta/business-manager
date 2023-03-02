using System;
using TMPro;
using UnityEngine;

namespace Scripts.Views
{
    public class LevelView : MonoBehaviour
    {
        public TMP_Text Label;

        public void SetLevel(int level) => 
            Label.text = $"LVL: {level}";
    }
}