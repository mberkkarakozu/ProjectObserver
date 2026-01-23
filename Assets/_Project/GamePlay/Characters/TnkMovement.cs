using UnityEngine;

namespace GamePlay.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class TnkMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float turnSpeed = 10f;


        private Rigidbody _rb;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            Move();
            Turn();
        }

        private void HandleInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            _moveDirection = new Vector3(horizontal, 0, vertical);

            if (_moveDirection.magnitude > 1)
            {
                _moveDirection.Normalize();
            }
        }

        private void Move()
        {
            if (_moveDirection.magnitude >= 0.1f)
            {
                Vector3 targetVelocity = _moveDirection * moveSpeed;
                targetVelocity.y = _rb.linearVelocity.y; 

                _rb.linearVelocity = targetVelocity;
            }
            else
            {
                _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
            }
        }

        private void Turn()
        {
            if (_moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
            }
        }
    }
}