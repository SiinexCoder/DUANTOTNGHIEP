using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f; // Tốc độ di chuyển của quái vật
    public bool isFlipped = false;

    // Danh sách các prefab đồ rơi
  

    // Khoảng cách ngẫu nhiên (trong đơn vị) cho các món đồ rơi ra
    public float dropDistance = 1.0f; 

    void Start()
    {
        // Tìm người chơi bằng tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Bắt đầu coroutine để quái vật biến mất sau 5 giây
        StartCoroutine(DisappearAfterDelay(5f));
    }
    

    void Update()
    {
        if (player != null)
        {
            // Rượt đuổi người chơi
            ChasePlayer();

            // Xoay quái vật để đối diện với người chơi
            LookAtPlayer();
        }
    }
    

    public void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;

        // Nếu người chơi ở bên phải quái vật
        if (direction.x > 0 && isFlipped)
        {
            // Lật quái vật để đối diện bên phải
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }
        // Nếu người chơi ở bên trái quái vật
        else if (direction.x < 0 && !isFlipped)
        {
            // Lật quái vật để đối diện bên trái
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isFlipped = true;
        }
    }

    public void ChasePlayer()
    {
        // Di chuyển về phía người chơi, chỉ tính trên trục X và Y cho góc nhìn top-down
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    }

    private IEnumerator DisappearAfterDelay(float delay)
    {
        // Chờ trong khoảng thời gian delay (5 giây)
        yield return new WaitForSeconds(delay);
        
        
        // Biến mất (có thể bạn muốn dùng Destroy(gameObject) hoặc tắt gameObject)
        Destroy(gameObject); // Xóa quái vật
        // Hoặc bạn có thể sử dụng: gameObject.SetActive(false); // Ẩn quái vật
    }

    
}
