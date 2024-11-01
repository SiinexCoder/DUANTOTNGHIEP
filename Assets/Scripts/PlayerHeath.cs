using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] int maxHeath;

    int currentHeath;

    public Animator animator;

    public HeathBar heathBar;
    public UnityEvent OnDeath;

    private Rigidbody2D rb; // Tham chiếu tới Rigidbody2D của nhân vật

    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }

    private void OnDisable()
    {
        OnDeath.RemoveListener(Death);
    }
    private void Start()
    {
        currentHeath = maxHeath;
        heathBar.UpdateBar(currentHeath, maxHeath);
    }

    public void TakeDam(int damage)
    {
        currentHeath -= damage;

        animator.SetTrigger("Hurt");

        if (currentHeath <= 0)
        {
            currentHeath = 0;
            OnDeath.Invoke();
        }
        heathBar.UpdateBar(currentHeath, maxHeath);
    }
    public void Heal(int healAmount)
    {
        currentHeath += healAmount; // Tăng máu hiện tại
        if (currentHeath > maxHeath)
        {
            currentHeath = maxHeath; // Đảm bảo không vượt quá sức khỏe tối đa
        }
        heathBar.UpdateBar(currentHeath, maxHeath); // Cập nhật thanh máu
    }



    public void Death()
    {
        Destroy(gameObject);
    }


    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDam(20);
        // }
    }
}
