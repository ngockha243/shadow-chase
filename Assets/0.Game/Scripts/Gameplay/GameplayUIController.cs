using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _0.Game.Scripts.Gameplay
{
    public class GameplayUIController : MonoBehaviour
    {
        public static GameplayUIController instance;
        public GameOverPanel gameOverPanel;
        public PausePanel PausePanel;
        public SettingPanel settingPanel;
        public List<Sprite> avatarSprites;
        public Image avatar;
        public Image hpFill;
        public Image lightFill;
        public Text hpText;
        public Text lightText;
        public Text scoreTxt;
        public TextMeshProUGUI lightCoreCountTxt;
        public SkillActiveUI skill2UI;
        public SkillActiveUI skill3UI;
        public GameObject handButton;
        public GameObject tutorialPanel;
        private void Awake()
        {
            instance = this;
            lightCoreCountTxt.text = "";
            avatar.sprite = avatarSprites[PlayerData.CharacterId];
            if (!GameLevel.firstTimeShowTutorial)
            {
                tutorialPanel.SetActive(true);
                GameLevel.firstTimeShowTutorial = true;
            }
        }

        public void OnHighestScore()
        {
            scoreTxt.color = Color.yellow;
            scoreTxt.transform.DOScale(1.2f, 0.15f).OnComplete(() =>
            {
                scoreTxt.transform.DOScale(1, 0.15f);
            });
        }
        public void OnChangeScore(int score)
        {
            scoreTxt.text = score.ToString();
        }
        public void PauseClick()
        {
            AudioManager.ins?.PlayButtonClick();
            PausePanel.Show();
        }
        public void PlayerSkill1()
        {
            GameController.instance.ActiveSkill1();
        }

        public void PlayerSkill2()
        {
            GameController.instance.ActiveSkill2();
        }

        public void PlayerSkill3()
        {
            GameController.instance.ActiveSkill3();
        }

        public void SetLightCoreTxt(int value)
        {
            lightCoreCountTxt.text = value.ToString();
        }
        public void OnHpChange(float currentHp, float maxHp)
        {
            hpFill.fillAmount = Common.GetPercent(currentHp, maxHp);
            hpText.text = $"{Common.GetPercent100(currentHp, maxHp):F2}%" ;
        }

        public void OnLightRangeChange(float currentLightRange, float maxLightRange)
        {
            lightFill.fillAmount = Common.GetPercent(currentLightRange, maxLightRange);
            lightText.text = $"{Common.GetPercent100(currentLightRange, maxLightRange):F2}%";
        }

        public void HandCollectClick()
        {
            handButton.gameObject.SetActive(false);
            GameController.instance.CollectLightCore();
        }

        public void ShowSettingPanel()
        {
            settingPanel.gameObject.SetActive(true);
        }
    }
}