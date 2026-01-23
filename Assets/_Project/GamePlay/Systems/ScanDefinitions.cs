using UnityEngine;

namespace GamePlay.Systems
{
    public enum ScanMaterialType
    {
        Inert,
        Metallic,
        Thermal,
        Radioactive,
        Organic
    }

    public struct ScanVisualData
    {
        public Color Color;
        public float Size;

        public ScanVisualData(Color color, float size)
        {
            Color = color;
            Size = size;
        }
    }

    public struct ScanResult
    {
        public Vector3 Point;
        public Color Color;
        public float Size;

        public ScanResult(Vector3 point, Color color, float size)
        {
            Point = point;
            Color = color;
            Size = size;
        }
    }
}