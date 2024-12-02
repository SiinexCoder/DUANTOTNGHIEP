using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] int maxHeath;
    int currentHeath;

    public HeathBar heathBar;

    private void Start()
    {
        currentHeath = maxHeath;
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

    public void TakeDam(int damage)
    {
        currentHeath -= damage;
        if (currentHeath <= 0)
        {
            currentHeath = 0;
            Destroy(gameObject);
            // Thêm logic chết nếu cần
        }
        heathBar.UpdateBar(currentHeath, maxHeath); // Cập nhật thanh máu
    }
    

    








    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDam(20);
        }
    }
}
