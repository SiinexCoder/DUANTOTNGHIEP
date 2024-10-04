using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monter : MonoBehaviour
{
    public Transform player;
public float speed = 2.5f;

public void LookAtPlayer()
{
    // Tính toán hướng đến player (chỉ quan tâm đến trục X và Y, không xoay)
    Vector2 direction = player.position - transform.position;

    // Xoay quái vật để hướng về phía player theo chiều ngang
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

void Update()
{
    // Quái vật di chuyển về phía player
    Vector2 direction = (player.position - transform.position).normalized;
    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    // Gọi hàm LookAtPlayer để đảm bảo quái vật luôn hướng về phía player
    LookAtPlayer();
}

}
