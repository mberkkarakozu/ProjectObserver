using UnityEngine;
using System.Collections;
using GamePlay.Systems;

public class LidarFX : MonoBehaviour
{
    [SerializeField] private LidarScanner scanner;


    private static readonly int _PosID = Shader.PropertyToID("_GlobalScannerPosition");
    private static readonly int _RadID = Shader.PropertyToID("_GlobalScannerRadius");

    private void OnEnable()
    {
        if (scanner != null) scanner.OnScanStarted += OnScanStart;
    }

    private void OnDisable()
    {
        if (scanner != null) scanner.OnScanStarted -= OnScanStart;
    }

    private void OnScanStart(float duration)
    {
    }
}