using UnityEngine;

namespace GamePlay.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target Settings")]
        [SerializeField] private Transform target;

        [Header("Room Settings")]
        [SerializeField] private Vector2 roomSize = new Vector2(20f, 20f);
        [SerializeField] private float transitionSpeed = 5f;

        [Header("Offset Settings")]
        [SerializeField] private Vector3 offset = new Vector3(-10f, 15f, -10f);

        private void LateUpdate()
        {
            if (target == null) return;

            float gridX = Mathf.Round(target.position.x / roomSize.x) * roomSize.x;
            float gridZ = Mathf.Round(target.position.z / roomSize.y) * roomSize.y;

            Vector3 targetGridPosition = new Vector3(gridX, 0, gridZ);
            Vector3 finalPosition = targetGridPosition + offset;

            transform.position = Vector3.Lerp(transform.position, finalPosition, transitionSpeed * Time.deltaTime);
        }
    }
}