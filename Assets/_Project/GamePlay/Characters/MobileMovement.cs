using UnityEngine;
using GamePlay.Systems; // Lidar sistemine erismek icin

namespace GamePlay.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class MobileMovement : MonoBehaviour
    {
        [Header("Ayarlar")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;

        [Header("Referanslar")]
        [SerializeField] private DynamicJoystick joystick;

        private LidarScanner _scanner;

        private Rigidbody _rb;
        private Vector3 _inputVector;
        private bool _canMove = true; 

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _scanner = GetComponent<LidarScanner>();
        }

        private void OnEnable()
        {
            if (_scanner != null)
            {
                _scanner.OnScanStarted += HandleScanStart;
                _scanner.OnScanCompleted += HandleScanEnd;
            }
        }

        private void OnDisable()
        {
            if (_scanner != null)
            {
                _scanner.OnScanStarted -= HandleScanStart;
                _scanner.OnScanCompleted -= HandleScanEnd;
            }
        }

        private void Start()
        {
            if (joystick == null) joystick = FindFirstObjectByType<DynamicJoystick>();
        }

        private void Update()
        {
            if (!_canMove)
            {
                _inputVector = Vector3.zero;
                return;
            }

            HandleInput();
            HandleRotation();
        }

        private void FixedUpdate()
        {
            if (_canMove) HandleMovement();
        }

        private void HandleScanStart(float duration)
        {
            _canMove = false; 
            _rb.linearVelocity = Vector3.zero;
        }

        private void HandleScanEnd()
        {
            _canMove = true; 
        }

        private void HandleInput()
        {
            if (joystick == null) return;
            float h = joystick.Horizontal;
            float v = joystick.Vertical;
            _inputVector = new Vector3(h, 0, v);
        }

        private void HandleMovement()
        {
            if (_inputVector.magnitude > 0.1f)
            {
                Vector3 targetPosition = _rb.position + _inputVector * moveSpeed * Time.fixedDeltaTime;
                _rb.MovePosition(targetPosition);
            }
        }

        private void HandleRotation()
        {
            if (_inputVector.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_inputVector);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}