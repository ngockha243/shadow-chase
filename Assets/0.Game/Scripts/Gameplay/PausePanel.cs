using UnityEngine;
using UnityEngine.SceneManagement;

namespace _0.Game.Scripts.Gameplay
{
    public class PausePanel : MonoBehaviour
    {
        public Transform content;
        public void Show()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            content.ShowPopup();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void ResumeClick()
        {
            Time.timeScale = 1;
            AudioManager.ins?.PlayButtonClick();
            gameObject.SetActive(false);
        }
        public void OptionCLick()
        {
            AudioManager.ins?.PlayButtonClick();
            gameObject.SetActive(false);
            GameplayUIController.instance.ShowSettingPanel();
        }

        public void ExitClick()
        {
            Time.timeScale = 1;
            AudioManager.ins?.PlayButtonClick();
            SceneManager.LoadScene("Menu");
        }
    }
}