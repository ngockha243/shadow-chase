using DG.Tweening;
using UnityEngine;

namespace _0.Game.Scripts
{
    public static class Common
    {
        public static void ShowPopup(this Transform obj)
        {
            obj.transform.localScale = Vector3.one * 0.5f;
            obj.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }

        public static float GetPercent(float currentValue, float maxValue)
        {
            if (maxValue <= 0f)
                return 0f;

            return Mathf.Clamp01(currentValue / maxValue);
        }

        public static float GetPercent100(float currentValue, float maxValue)
        {
            if (maxValue <= 0f)
                return 0f;

            return Mathf.Clamp(currentValue / maxValue * 100f, 0f, 100f);
        }
    }
}