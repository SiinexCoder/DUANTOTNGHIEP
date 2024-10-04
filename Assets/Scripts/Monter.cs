using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monter : MonoBehaviour
{
    public float speed = 2.5f;  // Tốc độ di chuyển của quái
    private Transform player;    // Đối tượng player mà quái sẽ đuổi theo

    void Start()
    {
        // Tìm đối tượng có tag "Player" khi game bắt đầu
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Kiểm tra nếu tìm thấy đối tượng player
        if (playerObject != null)
        {
            player = playerObject.transform; // Lưu vị trí của đối tượng player
        }

        // Sau 10 giây kể từ khi đối tượng được kích hoạt, tự động hủy
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        // Kiểm tra xem player có được tìm thấy không
        if (player != null)
        {
            // Tính toán hướng đến player và di chuyển quái vật về phía player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Gọi hàm LookAtPlayer để đảm bảo quái vật luôn hướng về phía player
            LookAtPlayer();
        }
    }

    void OnDestroy()
{
    // Gọi hàm EnemyDestroyed từ EnemySpawner
    FindObjectOfType<EnemySpawner>().EnemyDestroyed();
}
    public void LookAtPlayer()
    {
        // Tính toán hướng đến player (chỉ quan tâm đến trục X)
        Vector2 direction = player.position - transform.position;

        // Xoay quái vật để hướng về phía player theo chiều ngang (chỉ xoay theo trục X)
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            // Nếu player ở bên phải và quái đang lật, lật lại
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            // Nếu player ở bên trái và quái không lật, lật lại
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

}
