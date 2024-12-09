using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Danh sách các âm thanh có thể được gắn qua Inspector
    public List<AudioClip> soundClips;

    // AudioSource để phát âm thanh
    private AudioSource audioSource;

    void Start()
    {
        // Lấy hoặc thêm AudioSource từ đối tượng
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Player"
        if (collision.CompareTag("Player"))
        {
            // Phát âm thanh ngẫu nhiên từ danh sách, nếu có âm thanh
            if (soundClips.Count > 0)
            {
                AudioClip clipToPlay = soundClips[Random.Range(0, soundClips.Count)];
                audioSource.PlayOneShot(clipToPlay);
            }
            else
            {
                Debug.LogWarning("Chưa có âm thanh nào được gắn vào danh sách soundClips!");
            }
        }
    }
}
