using UnityEngine;
// using GamePlay.UI; // Eski namespace'i sil (gerekirse)

namespace GamePlay.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class MobileMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float turnSpeed = 10f;

        [Header("References")]
        [SerializeField] private Joystick joystick;

        private Rigidbody _rb;
        private Vector3 _moveInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
        }

        private void Update()
        {
            if (joystick != null)
            {
                float moveX = joystick.Horizontal;
                float moveZ = joystick.Vertical;

                _moveInput = new Vector3(moveX, 0f, moveZ);
            }
        }

        private void FixedUpdate()
        {
            if (_moveInput.magnitude >= 0.1f)
            {
                Move();
                Rotate();
            }
        }

        private void Move()
        {
            Vector3 targetPosition = _rb.position + _moveInput * moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(targetPosition);
        }

        private void Rotate()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveInput);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }
}