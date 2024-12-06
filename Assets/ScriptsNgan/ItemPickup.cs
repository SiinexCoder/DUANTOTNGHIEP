using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;  // Đảm bảo item có thể là SpeedPotion
    public InventoryUI inventoryUI;

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        Inventory inventory = collision.GetComponent<Inventory>();
        if (inventory != null && inventory.AddItem(item)) // Thêm item vào inventory
        {
            if (inventoryUI != null)
            {
                inventoryUI.UpdateUI(); // Cập nhật UI nếu có
            }
            else
            {
                Debug.LogError("InventoryUI not assigned in ItemPickup!"); // Kiểm tra lỗi nếu chưa có UI
            }

            Destroy(gameObject); // Xóa item khỏi scene sau khi nhặt
        }
    }
}

}


