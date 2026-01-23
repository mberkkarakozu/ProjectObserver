using UnityEngine;
using System.Collections;

public class LidarFX : MonoBehaviour
{
    [SerializeField] private LidarScanner scanner;
    [SerializeField] private float maxDistance = 10f;

    private static readonly int _PosID = Shader.PropertyToID("_ScannerPosition");
    private static readonly int _RadID = Shader.PropertyToID("_ScannerRadius");

    private Coroutine _waveCoroutine;

    private void OnEnable()
    {
        if (scanner != null)
        {
            scanner.OnScanStarted += StartWave;
        }
    }

    private void OnDisable()
    {
        if (scanner != null)
        {
            scanner.OnScanStarted -= StartWave;
        }
    }

    private void StartWave(float duration)
    {
        if (_waveCoroutine != null) StopCoroutine(_waveCoroutine);
        _waveCoroutine = StartCoroutine(AnimateWaveRoutine(duration));
    }

    private IEnumerator AnimateWaveRoutine(float duration)
    {
        float timer = 0f;

        if (scanner != null)
        {
            Shader.SetGlobalVector(_PosID, scanner.transform.position);
        }

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            float currentRadius = Mathf.Lerp(0f, maxDistance, progress);

            Shader.SetGlobalFloat(_RadID, currentRadius);

            yield return null;
        }

        Shader.SetGlobalFloat(_RadID, 0f);
        _waveCoroutine = null;
    }
}