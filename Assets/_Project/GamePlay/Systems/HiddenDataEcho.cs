using UnityEngine;
using System.Collections;
using GamePlay.Systems; 
public class HiddenDataEcho : MonoBehaviour, IScannable
{
    [SerializeField] private float visibleDuration = 3f;

    private MeshRenderer _meshRenderer;
    private Coroutine _hideTimerRoutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (_meshRenderer) _meshRenderer.enabled = false;
    }

    public void OnScanHit()
    {
        Reveal();
    }

    public ScanVisualData GetScanData()
    {
        return new ScanVisualData(Color.cyan, 1f);
    }

    public void Reveal()
    {
        if (_hideTimerRoutine != null) StopCoroutine(_hideTimerRoutine);
        if (_meshRenderer) _meshRenderer.enabled = true;
        _hideTimerRoutine = StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(visibleDuration);
        if (_meshRenderer) _meshRenderer.enabled = false;
        _hideTimerRoutine = null;
    }
}