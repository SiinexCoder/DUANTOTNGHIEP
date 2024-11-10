using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;  // Hiển thị thời gian
    [SerializeField] TextMeshProUGUI cycleText;  // Hiển thị trạng thái sáng/tối

    public static bool isNightTime;  // Biến tĩnh để kiểm tra trạng thái sáng/tối

    private float elapsedTime;  // Biến để tính thời gian trôi qua

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;  // Cập nhật thời gian trôi qua

        // Tính phút và giây
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Cập nhật văn bản hiển thị thời gian
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;

        // Chu kỳ sáng tối, chia thời gian thành 20 giây cho mỗi chu kỳ sáng/tối
        int cycleTime = Mathf.FloorToInt(elapsedTime) % 20;  // Lấy phần nguyên của elapsedTime chia thành 20 giây

        if (cycleTime < 10)  // 10 giây đầu là sáng
        {
            // Sáng
            cycleText.text = "Sáng";
            isNightTime = false;  // Thời gian sáng
        }
        else  // 10 giây còn lại là tối
        {
            // Tối
            cycleText.text = "Tối";
            isNightTime = true;  // Thời gian tối
        }

        Debug.Log("isNightTime: " + isNightTime);  // Kiểm tra giá trị của isNightTime
    }
}
