using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Thêm thư viện TextMeshPro

public class TimeCycle : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image timeBar;         // Thanh thời gian (Image)
    [SerializeField] private TextMeshProUGUI timerText; // Văn bản hiển thị thời gian
    [SerializeField] private Text cycleText;        // Văn bản hiển thị trạng thái (sáng/tối)

    [Header("Game Time Settings")]
    public float dayDuration = 300f;  // Thời gian ban ngày (5 phút)
    public float nightDuration = 300f; // Thời gian ban đêm (5 phút)

    private float elapsedTime;  // Tổng thời gian đã trôi qua
    private bool isNight;       // Trạng thái ban ngày/ban đêm

    private Color morningColor = Color.yellow;  // Màu vàng sáng
    private Color afternoonColor = new Color(1f, 0.65f, 0f); // Màu cam (chiều)
    private Color duskColor = new Color(1f, 0.5f, 0.2f);      // Màu cam sậm (chạng vạng)
    private Color nightColor = Color.gray;       // Màu xám
    private Color midnightColor = Color.black;   // Màu đen
    private Color dawnColor = Color.white;       // Màu trắng (bình minh)

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Cập nhật đồng hồ hiển thị thời gian
        UpdateTimerText();

        float cycleTime = elapsedTime % (dayDuration + nightDuration); // Thời gian trong chu kỳ sáng/tối

        if (cycleTime < dayDuration) // Thời gian ban ngày
        {
            isNight = false;
            cycleText.text = "Ban ngày";
            UpdateDayColors(cycleTime / dayDuration); // Tính tỉ lệ ban ngày
        }
        else // Thời gian ban đêm
        {
            isNight = true;
            cycleText.text = "Ban đêm";
            UpdateNightColors((cycleTime - dayDuration) / nightDuration); // Tính tỉ lệ ban đêm
        }
    }

    void UpdateTimerText()
    {
        // Tính phút và giây từ thời gian trôi qua
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Định dạng thời gian thành chuỗi "mm:ss" và hiển thị trên timerText
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;
    }

    void UpdateDayColors(float timeRatio)
    {
        // Sáng -> Vàng -> Cam (Sáng sớm đến chiều)
        if (timeRatio < 0.5f)
        {
            // Chuyển từ trắng sang vàng
            timeBar.color = Color.Lerp(Color.white, morningColor, timeRatio * 2);
        }
        else
        {
            // Chuyển từ vàng sang cam
            timeBar.color = Color.Lerp(morningColor, afternoonColor, (timeRatio - 0.5f) * 2);
        }
    }

    void UpdateNightColors(float timeRatio)
    {
        // Cam -> Xám -> Đen -> Trắng (Chiều tối đến bình minh)
        if (timeRatio < 0.25f)
        {
            // Chuyển từ cam sang xám
            timeBar.color = Color.Lerp(afternoonColor, duskColor, timeRatio * 4);
        }
        else if (timeRatio < 0.5f)
        {
            // Chuyển từ xám sang đen
            timeBar.color = Color.Lerp(duskColor, nightColor, (timeRatio - 0.25f) * 4);
        }
        else if (timeRatio < 0.75f)
        {
            // Chuyển từ đen sang trắng
            timeBar.color = Color.Lerp(nightColor, midnightColor, (timeRatio - 0.5f) * 4);
        }
        else
        {
            // Chuyển từ trắng sang cam
            timeBar.color = Color.Lerp(midnightColor, dawnColor, (timeRatio - 0.75f) * 4);
        }
    }
}
