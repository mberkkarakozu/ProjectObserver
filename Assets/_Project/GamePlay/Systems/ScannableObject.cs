using UnityEngine;
using System.Collections;

namespace GamePlay.Systems
{
    public class ScannableObject : MonoBehaviour, IScannable
    {
        [SerializeField] private ScanMaterialType materialType = ScanMaterialType.Inert;
        [SerializeField] private float glowDuration = 2f;
        [SerializeField] private float glowIntensity = 2f;

        private Renderer _renderer;
        private Material _material;
        private Coroutine _glowRoutine;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            if (_renderer != null)
            {
                _material = _renderer.material;
                _material.EnableKeyword("_EMISSION");
            }
        }

        public ScanVisualData GetScanData()
        {
            switch (materialType)
            {
                case ScanMaterialType.Metallic: return new ScanVisualData(Color.yellow, 0.4f);
                case ScanMaterialType.Thermal: return new ScanVisualData(Color.red, 0.4f);
                case ScanMaterialType.Radioactive: return new ScanVisualData(Color.green, 0.4f);
                case ScanMaterialType.Organic: return new ScanVisualData(new Color(0.5f, 0f, 0.5f), 0.3f);
                default: return new ScanVisualData(Color.cyan, 0.2f);
            }
        }

        public void OnScanHit()
        {
            if (_material == null) return;
            if (_glowRoutine != null) StopCoroutine(_glowRoutine);
            _glowRoutine = StartCoroutine(GlowAnimation());
        }

        private IEnumerator GlowAnimation()
        {
            Color baseColor = GetScanData().Color;
            float timer = 0f;

            while (timer < glowDuration)
            {
                timer += Time.deltaTime;
                float intensity = Mathf.Lerp(glowIntensity, 0f, timer / glowDuration);
                _material.SetColor("_EmissionColor", baseColor * intensity);
                yield return null;
            }

            _material.SetColor("_EmissionColor", Color.black);
        }
    }
}