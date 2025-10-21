using System;
using _0.Game.Scripts.Gameplay;
using UnityEngine;
namespace _0.Game.Scripts.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovement movement;
        public PlayerStats stats;
        public PlayerAnimation anim;
        public Light light;
        public GameObject lightCoreObj;
        public PlayerSkill1 skill1;
        public Transform cameraRoot;
        private float lightDecayUnitPerSecond = 1f;

        private float darknessToleranceSec;
        private float maxLightRange;
        private float darknessDamagePerSec;
        private float currentLightRange;
        private float currentHp;

        private float darknessTimer;
        private float decayAccum;
        
        
        private Action<float, float> OnHpChange;
        private Action<float, float> OnLightRangeChange;

        void Start()
        {
            currentHp = stats.GetHp();
            maxLightRange = stats.GetMaxLightRadius();
            currentLightRange = maxLightRange;
            lightDecayUnitPerSecond = stats.GetLightDecayPerSecond();
            darknessToleranceSec = stats.GetDarknessToleranceSec();
            darknessDamagePerSec = stats.GetDarknessDamagePerSec();
            light.range = currentLightRange;
            OnHpChange += GameplayUIController.instance.OnHpChange;
            OnLightRangeChange += GameplayUIController.instance.OnLightRangeChange;

            OnHpChange?.Invoke(currentHp, currentHp);
            OnLightRangeChange?.Invoke(currentLightRange, maxLightRange);
        }

        private void OnDestroy()
        {
            OnHpChange -= GameplayUIController.instance.OnHpChange;
            OnLightRangeChange -= GameplayUIController.instance.OnLightRangeChange;
        }

        void Update()
        {
            HandleLightDecay();
            HandleDarknessHP();
            ApplyLightToComponent();
        }

        void HandleLightDecay()
        {
            if (currentLightRange <= 0) return;

            currentLightRange -= lightDecayUnitPerSecond * Time.deltaTime * GameController.instance.gameSpeed;
            OnLightRangeChange?.Invoke(currentLightRange, maxLightRange);
            if (currentLightRange < 0)
                currentLightRange = 0;
        }

        void ApplyLightToComponent()
        {
            if (!light) return;
            light.range = currentLightRange * GameController.instance.lightBoost;
        }

        void HandleDarknessHP()
        {
            bool outOfLight = currentLightRange <= 0 + 0.0001f;

            if (outOfLight)
            {
                darknessTimer += Time.deltaTime * GameController.instance.gameSpeed;

                if (darknessTimer >= darknessToleranceSec)
                {
                    float dmg = darknessDamagePerSec * Time.deltaTime * GameController.instance.gameSpeed;
                    ApplyDamage(dmg);
                }
            }
            else
            {
                darknessTimer = 0f;
            }
        }

        void ApplyDamage(float dmg)
        {
            currentHp -= dmg;
            if (currentHp < 0f) currentHp = 0f;

            OnHpChange?.Invoke(currentHp, stats.GetHp());
            if (currentHp <= 0f)
            {
                // TODO: xử lý chết
                GameplayUIController.instance.gameOverPanel.Show();
            }
        }

        public void AddLight(float amount, bool clampToMax = true)
        {
            currentLightRange += amount;
            if (clampToMax) currentLightRange = Mathf.Min(currentLightRange, maxLightRange);
            GameController.instance.score += 100;
            if (currentLightRange > 0) darknessTimer = 0f;
        }



        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "LightCore")
            {
                lightCoreObj = other.gameObject;
                GameplayUIController.instance.handButton.gameObject.SetActive(true);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag == "LightCore")
            {
                lightCoreObj = null;
                GameplayUIController.instance.handButton.gameObject.SetActive(false);
            }
        }

       
    }
}