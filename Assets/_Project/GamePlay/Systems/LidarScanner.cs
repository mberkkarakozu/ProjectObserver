using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay.Systems
{
    public class LidarScanner : MonoBehaviour
    {
        [Header("Scanner Ayarlarý")]
        [SerializeField] private float maxScanRadius = 20f;
        [SerializeField] private float scanSpeed = 15f;
        [SerializeField] private LayerMask scanLayer;

        [Header("Görsel Efekt")]
        [SerializeField] private ParticleSystem scanParticle; // Buraya Efekti Sürükle!

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

            float currentRadius = 0f;
            float duration = maxScanRadius / scanSpeed;

            OnScanStarted?.Invoke(duration);

            if (scanParticle != null)
            {
                scanParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                var main = scanParticle.main;
                main.duration = duration;
                main.startLifetime = duration;
                main.startSize = maxScanRadius * 2.5f; 
                scanParticle.Play();
            }

            while (currentRadius < maxScanRadius)
            {
                currentRadius += scanSpeed * Time.deltaTime;

                Collider[] hits = Physics.OverlapSphere(transform.position, currentRadius, scanLayer);
                foreach (Collider hit in hits)
                {
                    int id = hit.GetInstanceID();
                    if (_scannedObjects.Contains(id)) continue;

                    float dist = Vector3.Distance(transform.position, hit.transform.position);
                    if (dist > currentRadius) continue;

                    if (hit.TryGetComponent(out IScannable scannable))
                    {
                        scannable.OnScanHit();
                        _scannedObjects.Add(id);
                    }
                    else if (hit.TryGetComponent(out HiddenDataEcho hidden))
                    {
                        hidden.Reveal();
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