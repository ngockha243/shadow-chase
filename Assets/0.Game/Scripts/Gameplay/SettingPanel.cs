using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _0.Game.Scripts.Gameplay
{
    public class SettingPanel : MonoBehaviour
    {
        public Text musicTxtValue;
        public Text fxTxtValue;
        public Slider musicSlider;
        public Slider fxSlider;
        public Transform content;

        private void OnEnable()
        {
            content.ShowPopup();
        }

        private void Start()
        {
            
            musicSlider.value = PlayerData.MusicVolume;
            fxSlider.value = PlayerData.SfxVolume;
            
            musicTxtValue.text = $"{(int)PlayerData.MusicVolume * 100}%";
            fxTxtValue.text = $"{(int)PlayerData.SfxVolume * 100}%";
            musicSlider.onValueChanged.AddListener(MusicChange);
            fxSlider.onValueChanged.AddListener(FxChange);
        }

        public void MusicChange(float value)
        {
            PlayerData.MusicVolume = value;
            musicTxtValue.text = $"{(int)PlayerData.MusicVolume * 100}%";
        }

        public void FxChange(float value)
        {
            PlayerData.SfxVolume = value;
            fxTxtValue.text = $"{(int)PlayerData.SfxVolume * 100}%";
        }
        public void Close()
        {
            AudioManager.ins?.PlayButtonClick();
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}