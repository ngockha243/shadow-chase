using System;
using UnityEngine;
using UnityEngine.UI;

namespace _0.Game.Scripts.Gameplay
{
    public class SkillActiveUI : MonoBehaviour
    {
        public enum SkillType
        {
            Skill1, Skill2, Skill3
        }

        public Image fill;
        public SkillType type;

        private float duration;
        private float currentTime;
        private bool isActive;

        public void Active(float time)
        {
            duration = Mathf.Max(0.01f, time); // tránh chia 0
            currentTime = 0f;
            isActive = true;

            // Bắt đầu từ fill = 0 (chạy lên 1)
            if (fill) fill.fillAmount = 0f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!isActive) return;

            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / duration);

            if (fill) fill.fillAmount = percent;

            if (currentTime >= duration)
            {
                isActive = false;
                gameObject.SetActive(false);
            }
        }

        public float GetPercent() => Mathf.Clamp01(currentTime / duration);
    }
}