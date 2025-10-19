using System.Collections;
using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class PlayerSkill1 : MonoBehaviour
    {
        public Light light;
        public void ActivateLightBoost(float targetRange, float riseTime, float fadeTime)
        {
            StopAllCoroutines();
            StartCoroutine(LightBoostRoutine(targetRange, riseTime, fadeTime));
        }

        private IEnumerator LightBoostRoutine(float targetRange, float riseTime, float fadeTime)
        {
            if (light == null) yield break;

            float originalRange = light.range;
            float t = 0f;

            // --- Giai đoạn tăng sáng ---
            while (t < riseTime)
            {
                t += Time.deltaTime;
                float lerp = Mathf.Clamp01(t / riseTime);
                light.range = Mathf.Lerp(originalRange, targetRange, lerp);
                yield return null;
            }

            // --- Giai đoạn giảm sáng về 0 ---
            t = 0f;
            float startRange = light.range;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float lerp = Mathf.Clamp01(t / fadeTime);
                light.range = Mathf.Lerp(startRange, 0f, lerp);
                yield return null;
            }

            light.range = 0f; // đảm bảo về 0 hoàn toàn
        }
    }
}