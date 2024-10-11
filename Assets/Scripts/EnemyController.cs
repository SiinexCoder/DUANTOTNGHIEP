using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Player player;
    public int minDamage;
    public int maxDamage;
    int currentHeath;
    public int maxHeath = 100;

    public Animator animator;

    void Start(){
        currentHeath = maxHeath;
    }

    public void TakeDamageEnemy(int damage){
        currentHeath -= damage;

        animator.SetTrigger("Hurt");

        if(currentHeath <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Die");
        
        animator.SetBool("IsDead", true);

        this.enabled = false;

        GetComponent<Collider2D>().enabled = false;

        GetComponent<LootBag>().InstantiateLoot(transform.position);

        Destroy(gameObject, 1f);
    }

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
