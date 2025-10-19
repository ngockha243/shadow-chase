using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _0.Game.Scripts.Gameplay
{
    public class GameOverPanel : MonoBehaviour
    {
        public TextMeshProUGUI txtGainLightCore;

        public void Show()
        {
            txtGainLightCore.text = $"{GameController.instance.countLight}";
            gameObject.SetActive(true);
        }

        public void Replay()
        {
            AudioManager.ins?.PlayButtonClick();
            PlayerData.currentGold += GameController.instance.countLight;

            SceneManager.LoadScene($"{GameLevel.currentMap}");
        }

        public void Menu()
        {
            AudioManager.ins?.PlayButtonClick();
            PlayerData.currentGold += GameController.instance.countLight;
            SceneManager.LoadScene($"Menu");
        }
    }
}