using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] int maxHeath;

    int currentHeath;

    // public Animator animator;

    public HeathBar heathBar;
    public UnityEvent OnDeath;

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
        // animator.SetTrigger("Hurt");
        if (currentHeath <= 0)
        {
            currentHeath = 0;
            OnDeath.Invoke();
        }
        heathBar.UpdateBar(currentHeath, maxHeath);
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
