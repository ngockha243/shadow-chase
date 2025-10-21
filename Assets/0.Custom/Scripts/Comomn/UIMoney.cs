using System;
using _0.Game.Scripts;
using TMPro;
using UnityEngine;

namespace _0.Custom.Scripts.Comomn
{
    public class UIMoney : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;

        private void Start()
        {
            PlayerData.onChangeCoin += SetMoneyText;
            SetMoneyText();
        }

        private void OnDestroy()
        {
            PlayerData.onChangeCoin -= SetMoneyText;
        }

        private void SetMoneyText()
        {
            moneyText.text = PlayerData.currentGold.ToString();
        }
    }
}