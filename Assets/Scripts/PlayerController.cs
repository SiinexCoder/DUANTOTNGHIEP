using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
=======


>>>>>>> origin/Ng2
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    public Animator animator;
    public Vector3 moveInput;
<<<<<<< HEAD
    
    [SerializeField]

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();    

    }
    void Update()
    {

=======

    [SerializeField]
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
>>>>>>> origin/Ng2
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
<<<<<<< HEAD
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

=======
                transform.localScale = new Vector3(1, 1, 1);

            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //   if (collision.GetComponent<Coin>())
    //     {
    //         Destroy(collision.gameObject);
    //     }
    // }

>>>>>>> origin/Ng2
   

    public PlayerHeath playerHeath;
    public void TakeDamage(int damage)
    {
        playerHeath.TakeDam(damage);
    }
}
<<<<<<< HEAD

    
=======
>>>>>>> origin/Ng2
