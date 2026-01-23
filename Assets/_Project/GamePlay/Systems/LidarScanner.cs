using UnityEngine;
using System;
using System.Collections;

public class LidarScanner : MonoBehaviour
{
    [SerializeField] private float maxScanRadius = 10f;
    [SerializeField] private float scanSpeed = 5f;
    [SerializeField] private LayerMask scanLayer;

    public event Action<float> OnScanStarted;
    public event Action OnScanCompleted;

    private bool _isScanning;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scan();
        }
    }

    public void Scan()
    {
        if (!_isScanning && scanSpeed > 0)
        {
            StartCoroutine(PerformScanRoutine());
        }
    }

    private IEnumerator PerformScanRoutine()
    {
        _isScanning = true;
        float currentRadius = 0f;
        float duration = maxScanRadius / scanSpeed;

        OnScanStarted?.Invoke(duration);

        while (currentRadius < maxScanRadius)
        {
            currentRadius += scanSpeed * Time.deltaTime;

            Collider[] hits = Physics.OverlapSphere(transform.position, currentRadius, scanLayer);

            foreach (Collider hit in hits)
            {
                HiddenDataEcho hiddenObject = hit.GetComponent<HiddenDataEcho>();
                if (hiddenObject != null)
                {
                    hiddenObject.Reveal();
                }
            }

            yield return null;
        }

        OnScanCompleted?.Invoke();
        _isScanning = false;
    }
}