using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_random : MonoBehaviour
{
    // Danh sách các loot item
    public List<LootItem> lootItems; 

    // Khoảng cách ngẫu nhiên (trong đơn vị) cho các món đồ rơi ra
    public float dropDistance = 1.0f; 

    [System.Serializable]
public class LootItem
{
    public GameObject prefab; // prefab của món đồ
    [Range(0, 1)]
    public float dropChance; // tỉ lệ rơi đồ của món đồ
}


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         DropLoot();
    //         Destroy(gameObject);
    //     }
    // }

    private void DropLoot()
    {
        // Tạo một số ngẫu nhiên từ 0 đến 1 cho từng item
        foreach (LootItem lootItem in lootItems)
        {
            if (Random.value < lootItem.dropChance)
            {
                // Nếu tỉ lệ rơi đồ đạt yêu cầu, tạo món đồ
                Vector3 randomPosition = transform.position + new Vector3(
                    Random.Range(-dropDistance, dropDistance),
                    Random.Range(-dropDistance, dropDistance),
                    0); // Không thay đổi trục Z

                Instantiate(lootItem.prefab, randomPosition, Quaternion.identity);
            }
        }
    }
}
