using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 1.5f; // Khoảng cách gần để quái vật có thể tấn công
    public float attackCooldown = 3f; // Thời gian giữa các lần tấn công
    private float lastAttackTime = -Mathf.Infinity; 

    Transform player;
    Rigidbody2D rb;
    Monster monster;

   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        monster = animator.GetComponent<Monster>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster.LookAtPlayer();

    
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

      
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
          
            if (Time.time >= lastAttackTime + attackCooldown)
            {
              
                animator.SetTrigger("Attack");
                
           
                lastAttackTime = Time.time;
            }
        }
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
