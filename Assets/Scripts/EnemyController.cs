using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
<<<<<<< HEAD
=======

>>>>>>> origin/Ng2
    Player player;
    public int minDamage;
    public int maxDamage;
    int currentHeath;
    public int maxHeath = 100;

    public Animator animator;

<<<<<<< HEAD
    void Start(){
        currentHeath = maxHeath;
    }

    public void TakeDamageEnemy(int damage){
=======
    void Start()
    {
        currentHeath = maxHeath;
    }

    public void TakeDamageEnemy(int damage)
    {
>>>>>>> origin/Ng2
        currentHeath -= damage;

        animator.SetTrigger("Hurt");

<<<<<<< HEAD
        if(currentHeath <= 0)
=======
        if (currentHeath <= 0)
>>>>>>> origin/Ng2
        {
            Die();
        }
    }
<<<<<<< HEAD

    void Die()
=======
        void Die()
>>>>>>> origin/Ng2
    {
        Debug.Log("Enemy Die");
        
        animator.SetBool("IsDead", true);

        this.enabled = false;

        GetComponent<Collider2D>().enabled = false;

<<<<<<< HEAD
        GetComponent<LootBag>().InstantiateLoot(transform.position);

        Destroy(gameObject, 1f);
    }
=======
        // GetComponent<LootBag>().InstantiateLoot(transform.position);

        Destroy(gameObject, 1f);
    }
    
>>>>>>> origin/Ng2

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            InvokeRepeating("DamagePlayer", 0, 1f);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = null;
            CancelInvoke("DamagePlayer");
        }
    }

    void DamagePlayer()
    {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);
        player.TakeDamage(damage);
    }
}
