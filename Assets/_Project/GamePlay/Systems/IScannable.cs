namespace GamePlay.Systems
{
    public interface IScannable
    {
        ScanVisualData GetScanData();
        void OnScanHit();
    }
}