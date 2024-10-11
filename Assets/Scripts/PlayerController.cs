using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    public Animator animator;
    public Vector3 moveInput;
    
    [SerializeField]

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();    

    }
    void Update()
    {

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                transform.localScale = new Vector3(5, 5, 5);

            else
                transform.localScale = new Vector3(-5, 5, 5);
        }

        
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.GetComponent<Coin>())
        {
            Destroy(collision.gameObject);
        }
    }

   

    public PlayerHeath playerHeath;
    public void TakeDamage(int damage)
    {
        playerHeath.TakeDam(damage);
    }
}

    
