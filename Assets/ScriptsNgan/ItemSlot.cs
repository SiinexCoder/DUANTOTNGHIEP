using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;  // Item liên kết với ô này
    public Inventory inventory;  // Tham chiếu đến inventory để sử dụng item

    // Các thành phần giao diện (UI)
    public Image itemIcon;  // Hiển thị icon của item
    

    // Hàm này được gọi để cập nhật giao diện ô item
    public void UpdateUI()
    {
        if (item != null)
        {
            itemIcon.sprite = item.icon;  // Gán icon từ item
            
        }
        else
        {
            itemIcon.sprite = null;  // Xóa icon nếu không có item
            
        }
    }

    // Hàm này được gọi khi nhấn vào ô item
    public void OnClick()
    {
        if (item is HealingItem)
        {
            inventory.UseHealingItem();  // Gọi phương thức sử dụng Healing Item
        }
        else if (item is SpeedPotion)
        {
            inventory.UseSpeedPotion();  // Gọi phương thức sử dụng Speed Potion
        }
    }
}
