using System;
using System.Collections.Generic;
using _0.Game.Scripts.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _0.Game.Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        public List<GameObject> characters;
        public List<MenuCharacter> menuCharacterUI;
        public GameObject buttonBuy;
        public GameObject buttonPlay;
        public Text characterNameText;
        public Text priceTxt;
        public Text highScoreTxt;
        public TextMeshProUGUI moveSpeedText;
        public TextMeshProUGUI lightRadiusText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI lightDecayText;
        private int currentPrice;
        private MenuCharacter selectMenuCharacter;
        private void Awake()
        {
            instance = this;
            PlayerPrefs.SetInt($"CharacterUnlocked_0", 1);
            highScoreTxt.text = $"{PlayerData.HighScore}";
        }

        private void Start()
        {
            ActiveCharacter(PlayerData.CharacterId);
        }

        public void ActiveCharacter(int id)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                character.gameObject.SetActive(i == id);
            }

            for (int i = 0; i < menuCharacterUI.Count; i++)
            {
                var ui = menuCharacterUI[i];
                ui.ActiveFrame(id == ui.id);
            }

            ActiveButtonState(id);
        }

        public void ActiveButtonState(int id)
        {
            selectMenuCharacter = menuCharacterUI[id];
            currentPrice = selectMenuCharacter.price;
            buttonBuy.SetActive(!selectMenuCharacter.IsUnlocked);
            priceTxt.gameObject.SetActive(!selectMenuCharacter.IsUnlocked);
            priceTxt.text = $"{currentPrice}";
            buttonPlay.SetActive(selectMenuCharacter.IsUnlocked);
            if (selectMenuCharacter.IsUnlocked) PlayerData.CharacterId = id;

        }

        public void Buy()
        {
            AudioManager.ins?.PlayButtonClick();
            if(PlayerData.currentGold < currentPrice) return;
            PlayerData.currentGold -= currentPrice;
            selectMenuCharacter.IsUnlocked = true;
            ActiveButtonState(selectMenuCharacter.id);
        }
        
        public void Play()
        {
            SceneManager.LoadScene(GameLevel.currentMap.ToString());
        }

        public void SetValueInfo(string characterName, PlayerStats playerData)
        {
            characterNameText.text = characterName;
            moveSpeedText.text = $"{playerData.GetMoveMentSpeed() * 100}";
            lightRadiusText.text = $"{playerData.GetMaxLightRadius() * 100}";
            healthText.text = $"{playerData.GetHp() * 100}";
            lightDecayText.text = $"{(int)(playerData.GetLightDecayPerSecond() * 100)}";
        }
    }
}