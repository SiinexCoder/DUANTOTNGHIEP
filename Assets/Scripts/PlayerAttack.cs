using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
<<<<<<< HEAD

=======
>>>>>>> origin/Ng2
    public Animator animator;

    public Transform attackPoint;

    public float attackRange = 0.5f;

    public LayerMask emenyLayers;

    public int attackDamage = 40;

<<<<<<< HEAD

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
=======
    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
>>>>>>> origin/Ng2
        {
            Attack();
        }
    }

<<<<<<< HEAD
    void Attack()
=======
        void Attack()
>>>>>>> origin/Ng2
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, emenyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamageEnemy(attackDamage);
        }

        
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
<<<<<<< HEAD

=======
>>>>>>> origin/Ng2
    }
}
