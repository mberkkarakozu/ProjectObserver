using UnityEngine;

namespace GamePlay.Systems
{
    public class DoorController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Vector3 slideDirection = Vector3.right;
        [SerializeField] private float slideDistance = 3f;
        [SerializeField] private float speed = 2f;

        private Vector3 _closedPos;
        private Vector3 _openPos;
        private bool _isOpen;

        private void Start()
        {
            _closedPos = transform.position;
            _openPos = _closedPos + (slideDirection * slideDistance);
        }

        private void Update()
        {
            Vector3 target = _isOpen ? _openPos : _closedPos;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        public void OnScanHit() { }

        public ScanVisualData GetScanData()
        {
            return new ScanVisualData(Color.yellow, 0.5f);
        }

        public void Interact()
        {
            _isOpen = !_isOpen;
        }

        public string GetInteractionPrompt()
        {
            return _isOpen ? "Kapat" : "Aç";
        }
    }
}