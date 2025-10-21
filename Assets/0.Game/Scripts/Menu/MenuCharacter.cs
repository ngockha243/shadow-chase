using System.Collections.Generic;
using _0.Game.Scripts.Gameplay;
using UnityEngine;

namespace _0.Game.Scripts.Menu
{
    public class MenuCharacter : MonoBehaviour
    {
        public GameObject activeFrame;
        public int id;
        public int price;
        public PlayerStats stats;

        public bool IsUnlocked
        {
            get => PlayerPrefs.GetInt($"CharacterUnlocked_{id}", 0) == 1;

            set => PlayerPrefs.SetInt($"CharacterUnlocked_{id}", value ? 1 : 0);
        }

        public void SelectCharacter()
        {
            MenuController.instance.ActiveCharacter(id);
            MenuController.instance.SetValueInfo(gameObject.name, stats);
        }

        public void ActiveFrame(bool active)
        {
            activeFrame.SetActive(active);
        }
    }
}