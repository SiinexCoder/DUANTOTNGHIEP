using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monter_Run : StateMachineBehaviour
{
   public float speed = 2.5f;
public float attackRange = 1f;
public float attackCooldown = 2f;  // Thời gian chờ giữa các lần tấn công (2 giây)
private float lastAttackTime = -Mathf.Infinity;  // Lưu thời gian lần tấn công cuối cùng

Transform player;
Rigidbody2D rb;
Monter monter;

// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    player = GameObject.FindGameObjectWithTag("Player").transform;
    rb = animator.GetComponent<Rigidbody2D>();
    monter = animator.GetComponent<Monter>();
}

// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    monter.LookAtPlayer();

    // Di chuyển quái vật về phía người chơi theo cả X và Y
    Vector2 target = new Vector2(player.position.x, player.position.y);
    Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
    rb.MovePosition(newPos);

    // Kiểm tra khoảng cách giữa player và quái vật
    float distanceToPlayer = Vector2.Distance(player.position, rb.position);

    // Nếu khoảng cách nhỏ hơn hoặc bằng phạm vi tấn công và đủ thời gian tấn công lại
    if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
    {
        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;  // Cập nhật thời gian tấn công cuối cùng
    }
}

// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    animator.ResetTrigger("Attack");
}

}

