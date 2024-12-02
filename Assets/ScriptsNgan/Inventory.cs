using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemStack> items = new List<ItemStack>(); // Danh sách các ItemStack
    public int maxSlots = 10; // Số lượng slot tối đa

    // Thêm item vào inventory
    public bool AddItem(Item item)
    {
        // Kiểm tra nếu item có thể stack
        foreach (ItemStack stack in items)
        {
            if (stack.item == item && stack.item.isStackable)
            {
                stack.quantity += 1; // Tăng số lượng nếu có thể stack
                return true;
            }
        }

        // Nếu không có item stackable, thêm item mới vào inventory
        if (items.Count < maxSlots)
        {
            items.Add(new ItemStack(item, 1)); // Tạo mới một ItemStack
            return true;
        }

        return false; // Nếu inventory đầy
    }


    // Kiểm tra xem có vật phẩm hồi máu trong inventory hay không
    public ItemStack GetHealingItem()
    {
        // Tìm vật phẩm hồi máu đầu tiên trong inventory
        return items.Find(stack => stack.item is HealingItem);
    }
    public void UseHealingItem()
    {
        // Tìm vật phẩm hồi máu trong inventory
        ItemStack healingStack = items.Find(stack => stack.item is HealingItem);

        if (healingStack != null)
        {
            HealingItem healingItem = healingStack.item as HealingItem;
            if (healingItem != null)
            {
                // Tìm đối tượng PlayerHeath và gọi phương thức Heal
                PlayerHeath playerHealth = FindObjectOfType<PlayerHeath>();
                playerHealth.Heal(healingItem.healingAmount); // Hồi máu cho player

                // Giảm số lượng vật phẩm trong inventory
                healingStack.quantity -= 1;

                if (healingStack.quantity <= 0)
                {
                    items.Remove(healingStack); // Nếu số lượng = 0, xóa vật phẩm khỏi inventory
                }

                // Cập nhật UI
                FindObjectOfType<InventoryUI>().UpdateUI();

                Debug.Log($"Đã sử dụng Healing Item, hồi {healingItem.healingAmount} máu.");
            }
        }
        else
        {
            Debug.Log("Không có vật phẩm hồi máu trong inventory.");
        }
    }

}

