using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.UI
{
    public class DynamicJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform joystickBase;   
        [SerializeField] private RectTransform joystickHandle; 
        [SerializeField] private CanvasGroup canvasGroup;     

        [Header("Settings")]
        [SerializeField] private float handleRange = 50f;     

        private Vector2 _inputVector;
        private Vector2 _joystickCenter;

        private void Start()
        {
            canvasGroup.alpha = 0f;
            joystickHandle.anchoredPosition = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;

            joystickBase.position = eventData.position;

            joystickHandle.anchoredPosition = Vector2.zero;

            _joystickCenter = eventData.position;

            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - _joystickCenter;

            _inputVector = Vector2.ClampMagnitude(direction, handleRange) / handleRange;

            joystickHandle.anchoredPosition = _inputVector * handleRange;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            joystickHandle.anchoredPosition = Vector2.zero;
            canvasGroup.alpha = 0f;
        }

        public Vector2 GetInputDirection()
        {
            return _inputVector;
        }
    }
}