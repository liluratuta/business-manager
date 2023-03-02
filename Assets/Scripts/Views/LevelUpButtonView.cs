using Scripts.Services;
using System;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class LevelUpButtonView : MonoBehaviour
    {
        public Button Button;
        public TMP_Text Label;
        
        private LevelUpService _levelUpService;
        private BusinessID _businessID;

        public void Init(LevelUpService levelUpService)
        {
            _levelUpService = levelUpService;
        }

        public void SetBusinessID(BusinessID businessID) => 
            _businessID = businessID;

        public void SetLevelUpPossible(bool isPossible) => 
            Button.interactable = isPossible;

        public void SetCost(double cost) => 
            Label.text = $"LVL UP\nCost: {cost}$";

        private void OnEnable() => 
            Button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            Button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _levelUpService.LevelUp(_businessID);
        }
    }
}