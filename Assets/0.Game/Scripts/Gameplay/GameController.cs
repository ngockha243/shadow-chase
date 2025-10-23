using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public FrostEffect frostEffect;
        public List<PlayerController> players;
        public PlayerController player;
        public LightCoreController lightCore;
        public CameraFlower cameraFlower;
        public int countLight = 0;
        public int lightBoost = 1;
        public float gameSpeed = 1;

        private int highScore;
        private float scoreFloat;
        private bool reachedHighScore;
        public int score;
        public bool gameOver = false;

        private void Awake()
        {
            instance = this;
            player = players[PlayerData.CharacterId];
            // player = Instantiate(obj);
            // player.transform.position = spawnPos.position;
            player.gameObject.SetActive(true);
            cameraFlower.viewTarget = player.cameraRoot;
            score = 0;
            highScore = PlayerData.HighScore;
        }

        private void Update()
        {
            if (gameOver) return;
            scoreFloat += Time.deltaTime * 2f;
            score = (int)scoreFloat;

            GameplayUIController.instance.OnChangeScore(score);
            if (!reachedHighScore && score > highScore)
            {
                reachedHighScore = true;

                GameplayUIController.instance.OnHighestScore();
            }
        }

        private void OnDestroy()
        {
            if (score > highScore) PlayerData.HighScore = score;
        }

        public void CollectLightCore()
        {
            if (player.light == null) return;
            countLight += 1;
            var core = player.lightCoreObj;
            this.lightCore.RemoveLight(core);
            player.AddLight(1);

            scoreFloat += 10;
            score = (int)scoreFloat;
            
            GameplayUIController.instance.OnChangeScore(score);
            if (!reachedHighScore && score > highScore)
            {
                reachedHighScore = true;

                GameplayUIController.instance.OnHighestScore();
            }
            
            GameplayUIController.instance.SetLightCoreTxt(countLight);
        }

        public void ActiveSkill1()
        {
            player.skill1.ActivateLightBoost(25, 1, 3);
            AudioManager.ins?.PlaySkill1();
        }

        public void ActiveSkill2()
        {
            StartCoroutine(Skill2IE());
        }

        private IEnumerator Skill2IE()
        {
            AudioManager.ins?.PlaySkill2();
            float time = 10;
            lightBoost = 3;
            GameplayUIController.instance.skill2UI.Active(time);
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            lightBoost = 1;
        }

        public void ActiveSkill3()
        {
            StartCoroutine(Skill3IE());
        }

        private IEnumerator Skill3IE()
        {
            AudioManager.ins?.PlaySkill3();
            float time = 10;
            gameSpeed = 0.15f;
            DOTween.To(() => frostEffect.FrostAmount,   
                x => frostEffect.FrostAmount = x,  
                0.25f,                             
                0.5f);      
            GameplayUIController.instance.skill3UI.Active(time);
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }
            DOTween.To(() => frostEffect.FrostAmount,   
                x => frostEffect.FrostAmount = x,  
                0f,                             
                0.5f);     
            gameSpeed = 1;
        }
    }
}