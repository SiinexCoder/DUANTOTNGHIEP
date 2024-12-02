using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1.5f; // Khoảng cách gần để quái vật có thể tấn công
    public float attackCooldown = 3f; // Thời gian hồi chiêu giữa các lần tấn công
    private float lastAttackTime = -Mathf.Infinity; // Thời điểm của lần tấn công gần nhất

    Transform player;
    Rigidbody2D rb;
    Monster monster;

    // Hàm này được gọi khi trạng thái bắt đầu
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        monster = animator.GetComponent<Monster>();
    }

    // Hàm này được gọi liên tục khi trạng thái đang hoạt động
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.LookAtPlayer();

        // Di chuyển về phía người chơi (theo trục X)
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Kiểm tra khoảng cách giữa quái vật và người chơi
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            // Kiểm tra nếu đủ thời gian hồi chiêu thì cho phép tấn công
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                // Đặt trigger tấn công
                animator.SetTrigger("Attack");
                
                // Cập nhật thời gian của lần tấn công gần nhất
                lastAttackTime = Time.time;
            }
        }
    }

    // Hàm này được gọi khi trạng thái kết thúc
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
