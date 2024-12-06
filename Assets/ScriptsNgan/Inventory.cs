using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    public List<ItemStack> items = new List<ItemStack>();  // Danh sách các item trong inventory
    public int maxSlots = 10;  // Số lượng slot tối đa

    // Thêm item vào inventory
    public bool AddItem(Item item)
    {
        foreach (ItemStack stack in items)
        {
            if (stack.item == item && stack.item.isStackable)
            {
                stack.quantity += 1;  // Tăng số lượng nếu có thể stack
                return true;
            }
        }

        if (items.Count < maxSlots)
        {
            items.Add(new ItemStack(item, 1));  // Thêm item mới vào inventory
            return true;
        }

        return false;
    }

    // Kiểm tra và sử dụng vật phẩm hồi máu
    public void UseHealingItem()
    {
        ItemStack healingStack = items.Find(stack => stack.item is HealingItem);
        if (healingStack != null)
        {
            HealingItem healingItem = healingStack.item as HealingItem;
            if (healingItem != null)
            {
                PlayerHeath playerHealth = FindObjectOfType<PlayerHeath>();
                playerHealth.Heal(healingItem.healingAmount);  // Hồi máu cho player
                healingStack.quantity -= 1;

                if (healingStack.quantity <= 0)
                {
                    items.Remove(healingStack);  // Nếu hết thuốc, xóa khỏi inventory
                }

                FindObjectOfType<InventoryUI>().UpdateUI();  // Cập nhật UI
            }
        }
    }

    // Kiểm tra và sử dụng thuốc tăng tốc
    public void UseSpeedPotion()
    {
        ItemStack speedPotionStack = items.Find(stack => stack.item is SpeedPotion);
        if (speedPotionStack != null)
        {
            SpeedPotion speedPotion = speedPotionStack.item as SpeedPotion;
            if (speedPotion != null)
            {
                PlayerController playerController = FindObjectOfType<PlayerController>();
                if (playerController != null)
                {
                    StartCoroutine(ApplySpeedBoost(playerController, speedPotion));  // Áp dụng thuốc tăng tốc
                }

                speedPotionStack.quantity -= 1;
                if (speedPotionStack.quantity <= 0)
                {
                    items.Remove(speedPotionStack);  // Nếu hết thuốc, xóa khỏi inventory
                }

                FindObjectOfType<InventoryUI>().UpdateUI();  // Cập nhật UI
            }
        }
    }

    // Áp dụng thuốc tăng tốc cho player
    private IEnumerator ApplySpeedBoost(PlayerController playerController, SpeedPotion speedPotion)
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed += speedPotion.speedBoostAmount;

        yield return new WaitForSeconds(speedPotion.duration);

        playerController.moveSpeed = originalSpeed;
    }


    // void Update()
    // {
    //      if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         UseHealingItem(); // Gọi phương thức sử dụng thuốc hồi máu
    //     }
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         UseSpeedPotion(); // Gọi phương thức sử dụng thuốc tăng tốc
    //     }

    // }
}






