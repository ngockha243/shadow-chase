using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _0.Game.Scripts.Gameplay
{
    public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [Header("Refs")] public RectTransform rect;
        public Image cooldownMask;
        public TextMeshProUGUI cooldownText;
        public GameObject tooltip;

        [Header("Config")] [Tooltip("Giữ lâu hơn ngưỡng này sẽ bật tooltip, không cast skill.")]
        public float longPressThreshold = 0.35f;

        [Tooltip("Thời gian hồi chiêu (giây).")]
        public float cooldownTime = 3f;

        [Tooltip("Có cho phép cast khi đang cooldown không?")]
        public bool blockWhenCooldown = true;

        [Header("Events")] public UnityEvent onSkillCast;
        public UnityEvent onShowTooltip;
        public UnityEvent onHideTooltip;

        bool pointerDown;
        bool longPressTriggered;
        bool pointerExited;
        float pressStartTime;
        float cooldownTimer;

        public bool IsOnCooldown => cooldownTimer > 0f;

        void Reset()
        {
            rect = GetComponent<RectTransform>();
        }

        void Awake()
        {
            if (rect == null) rect = GetComponent<RectTransform>();
            if (tooltip) tooltip.SetActive(false);
            if (cooldownMask)
            {
                cooldownMask.fillAmount = 0f;
            }
        }

        void Update()
        {
            if (pointerDown && !longPressTriggered)
            {
                if (Time.unscaledTime - pressStartTime >= longPressThreshold)
                {
                    longPressTriggered = true;
                    ShowTooltip(true);
                }
            }

            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.unscaledDeltaTime;
                if (cooldownTimer < 0f) cooldownTimer = 0f;

                if (cooldownMask)
                {
                    cooldownMask.fillAmount = cooldownTimer / cooldownTime;
                    cooldownText.text = cooldownTimer.ToString("0.00");
                }
            }
            else
            {
                cooldownText.text = "";
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pointerDown = true;
            longPressTriggered = false;
            pointerExited = false;
            pressStartTime = Time.unscaledTime;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pointerDown = false;

            if (longPressTriggered)
            {
                ShowTooltip(false);
                return;
            }

            bool inside = RectTransformUtility.RectangleContainsScreenPoint(rect, eventData.position);
            if (!inside || pointerExited)
                return;

            if (blockWhenCooldown && IsOnCooldown)
                return;

            onSkillCast?.Invoke();

            StartCooldown();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerExited = true;
        }

        void ShowTooltip(bool show)
        {
            if (tooltip) tooltip.SetActive(show);
            if (show) onShowTooltip?.Invoke();
            else onHideTooltip?.Invoke();
        }

        public void StartCooldown()
        {
            cooldownTimer = cooldownTime;
            if (cooldownMask) cooldownMask.fillAmount = 1f;
        }
    }
}