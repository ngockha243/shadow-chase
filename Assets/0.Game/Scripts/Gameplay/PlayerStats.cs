using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float lightRadius = 5f;
        [SerializeField] private int maxHP = 100;
        [SerializeField] private float darknessToleranceSec = 3f;

        [Tooltip("Sát thương mỗi giây khi đã vượt tolerance và vẫn trong bóng tối.")] [SerializeField]
        private float darknessDamagePerSec = 10f;
        [SerializeField] private float lightDecay = 1f;

        public float GetHp()
        {
            return maxHP;
        }

        public float GetMaxLightRadius()
        {
            return lightRadius;
        }

        /// <summary>
        /// số đơn vị ánh sáng giảm mỗi giây
        /// </summary>
        public float GetLightDecayPerSecond()
        {
            return lightDecay / 3;
        }

        /// <summary>
        /// Thời gian chịu được bóng tối trước khi bắt đầu mất máu.
        /// </summary>
        public float GetDarknessToleranceSec()
        {
            return darknessToleranceSec;
        }

        public float GetDarknessDamagePerSec()
        {
            return darknessDamagePerSec;
        }

        public float GetMoveMentSpeed()
        {
            return movementSpeed;
        }
    }
}