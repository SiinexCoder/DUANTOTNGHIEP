using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public InventoryUI inventoryUI; // Tham chiếu đến InventoryUI


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();
            if (inventory != null && inventory.AddItem(item))
            {
                if (inventoryUI != null)
                {
                    inventoryUI.UpdateUI();
                }
                else
                {
                    Debug.LogError("InventoryUI not assigned in ItemPickup!");
                }

                Destroy(gameObject); // Xóa item khỏi Scene
            }
        }
    }
}
