using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _0.Game.Scripts.Gameplay
{
    public class LightCoreController : MonoBehaviour
    {
        public List<GameObject> deActiveLight;
        public List<GameObject> activeLight;
        public int maxLightActive;
        public float timeSpawn;
        private Coroutine spawnIE;
        private void Start()
        {
            for (int i = 0; i < maxLightActive; i++)
            {
                var obj = deActiveLight[Random.Range(0, deActiveLight.Count)];
                ActiveLight(obj);
            }
            StartSpawn();
        }

        private void OnDestroy()
        {
            if(spawnIE != null) StopCoroutine(spawnIE);
        }

        private void StartSpawn()
        {
            if(spawnIE != null) StopCoroutine(spawnIE);
            spawnIE = StartCoroutine(CheckAndSpawnLight());
        }
        private IEnumerator CheckAndSpawnLight()
        {
            yield return new WaitForSeconds(timeSpawn);
            while (activeLight.Count < maxLightActive)
            {
                var obj = deActiveLight[Random.Range(0, deActiveLight.Count)];
                ActiveLight(obj);
                yield return new WaitForSeconds(0.01f);
            }
            StartSpawn();
        }


        public void RemoveLight(GameObject obj)
        {
            DeActiveLight(obj);
        }

        private void ActiveLight(GameObject obj)
        {
            if (deActiveLight.Contains(obj))
            {
                deActiveLight.Remove(obj);
                activeLight.Add(obj);
                obj.gameObject.SetActive(true);
            }
        }

        private void DeActiveLight(GameObject obj)
        {
            if (activeLight.Contains(obj))
            {
                activeLight.Remove(obj);
                deActiveLight.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
    }
}