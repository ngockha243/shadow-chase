using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public Transform spawnPos;
        public List<PlayerController> players;
        public PlayerController player;
        public LightCoreController lightCore;
        public CameraFlower cameraFlower;
        public int countLight = 0;

        public int lightBoost = 1;
        public float gameSpeed = 1;

        private void Awake()
        {
            instance = this;
            player = players[PlayerData.CharacterId];
            // player = Instantiate(obj);
            // player.transform.position = spawnPos.position;
            player.gameObject.SetActive(true);
            cameraFlower.viewTarget = player.cameraRoot;
        }


        public void CollectLightCore()
        {
            if (player.light == null) return;
            countLight += 1;
            var core = player.lightCoreObj;
            this.lightCore.RemoveLight(core);
            player.AddLight(1);
            GameplayUIController.instance.SetLightCoreTxt(countLight);
        }

        public void ActiveSkill1()
        {
            player.skill1.ActivateLightBoost(25, 1, 3);
        }

        public void ActiveSkill2()
        {
            StartCoroutine(Skill2IE());
        }
        private IEnumerator Skill2IE()
        {
            float time = 5;
            lightBoost = 2;
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
            float time = 3;
            gameSpeed = 0.5f;
            GameplayUIController.instance.skill3UI.Active(time);
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            gameSpeed = 1;
        }
    }
}