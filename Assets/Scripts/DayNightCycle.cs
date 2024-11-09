using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
     [Header("Settings")]
    public float dayLengthInMinutes = 1; // Thời gian cho một ngày (tính bằng phút)
    public Light sunLight; // Đối tượng ánh sáng
    public Gradient lightColor; // Màu sắc ánh sáng theo thời gian
    public AnimationCurve lightIntensity; // Cường độ ánh sáng theo thời gian

    private float timeOfDay = 0f; // Biến thời gian trong ngày

    void Update()
    {
        // Tăng thời gian trong ngày
        timeOfDay += Time.deltaTime / (dayLengthInMinutes * 60f);

        // Đặt lại thời gian khi hoàn tất một chu kỳ ngày đêm
        if (timeOfDay >= 1)
            timeOfDay = 0;

        // Cập nhật màu sắc và cường độ ánh sáng theo thời gian trong ngày
        if (sunLight != null)
        {
            sunLight.color = lightColor.Evaluate(timeOfDay);
            sunLight.intensity = lightIntensity.Evaluate(timeOfDay);
        }
    }
}
