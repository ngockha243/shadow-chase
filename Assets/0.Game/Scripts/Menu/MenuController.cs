using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _0.Game.Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;
        public List<GameObject> characters;
        public List<MenuCharacter> menuCharacterUI;
        private void Awake()
        {
            instance = this;
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
            PlayerData.CharacterId = id;
        }


        public void Play()
        {
            SceneManager.LoadScene(GameLevel.currentMap.ToString());
        }
    }
}