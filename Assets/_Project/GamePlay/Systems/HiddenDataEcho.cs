using UnityEngine;
using System.Collections;

public class HiddenDataEcho : MonoBehaviour
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
        _meshRenderer.enabled = false;
    }

    public void Reveal()
    {
        if (_hideTimerRoutine != null)
        {
            StopCoroutine(_hideTimerRoutine);
        }

        _meshRenderer.enabled = true;
        _hideTimerRoutine = StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(visibleDuration);
        _meshRenderer.enabled = false;
        _hideTimerRoutine = null;
    }
}