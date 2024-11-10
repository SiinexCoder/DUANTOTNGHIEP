using UnityEngine;

public class DayNightBackgroundCycle : MonoBehaviour
{
    public Camera mainCamera; // Camera cần thay đổi màu nền
    public Color dayColor = Color.cyan; // Màu sắc cho ban ngày
    public Color nightColor = Color.black; // Màu sắc cho ban đêm
    public float cycleDuration = 20f; // Tổng thời gian của chu kỳ ngày-đêm

    private float timeElapsed;

    void Update()
    {
        // Tăng thời gian đã trôi qua mỗi frame
        timeElapsed += Time.deltaTime;

        // Tính toán tỉ lệ của chu kỳ (từ 0 đến 1)
        float cycleProgress = (timeElapsed % cycleDuration) / cycleDuration;

        // Thay đổi màu nền của Camera từ đêm (nightColor) sang ngày (dayColor) và ngược lại
        mainCamera.backgroundColor = Color.Lerp(nightColor, dayColor, Mathf.Sin(cycleProgress * Mathf.PI * 2) * 0.5f + 0.5f);
    }
}
