using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay.Systems
{
    public class LidarScanner : MonoBehaviour
    {
        [Header("Scanner Settings")]
        [SerializeField] private float maxScanRadius = 20f;
        [SerializeField] private float scanSpeed = 15f;
        [SerializeField] private LayerMask scanLayer;

        [Header("Visual Settings")]
        [SerializeField] private ParticleSystem scanParticle;
        [Tooltip("Effect Transform")]
        [SerializeField] private Transform handTransform; 

        public event Action<float> OnScanStarted;
        public event Action OnScanCompleted;

        private bool _isScanning;
        private HashSet<int> _scannedObjects = new HashSet<int>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Scan();
        }

        public void Scan()
        {
            if (!_isScanning) StartCoroutine(PerformScanRoutine());
        }

        private IEnumerator PerformScanRoutine()
        {
            _isScanning = true;
            _scannedObjects.Clear();

            Vector3 originPoint = handTransform != null ? handTransform.position : transform.position;

            float currentRadius = 0f;
            float duration = maxScanRadius / scanSpeed;

            OnScanStarted?.Invoke(duration);

            if (scanParticle != null)
            {
                scanParticle.transform.position = originPoint;

                scanParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                var main = scanParticle.main;
                main.duration = duration;
                main.startLifetime = duration;


                scanParticle.Play();
            }

            while (currentRadius < maxScanRadius)
            {
                currentRadius += scanSpeed * Time.deltaTime;

                Collider[] hits = Physics.OverlapSphere(originPoint, currentRadius, scanLayer);

                foreach (Collider hit in hits)
                {
                    int id = hit.GetInstanceID();
                    if (_scannedObjects.Contains(id)) continue;

                    float dist = Vector3.Distance(originPoint, hit.transform.position);
                    if (dist > currentRadius) continue;

                    if (hit.TryGetComponent(out IScannable scannable))
                    {
                        scannable.OnScanHit();
                        _scannedObjects.Add(id);
                    }
                }

                yield return null;
            }

            OnScanCompleted?.Invoke();
            _isScanning = false;
        }
    }
}