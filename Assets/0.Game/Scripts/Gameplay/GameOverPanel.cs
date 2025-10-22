using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _0.Game.Scripts.Gameplay
{
    public class GameOverPanel : MonoBehaviour
    {
        public TextMeshProUGUI txtGainLightCore;
        private bool isShow = false;
        public void Show()
        {
            if(isShow) return;
            isShow = true;
            txtGainLightCore.text = $"{GameController.instance.countLight}";
            AudioManager.ins?.PlayLose();
            GameController.instance.gameOver = true;
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