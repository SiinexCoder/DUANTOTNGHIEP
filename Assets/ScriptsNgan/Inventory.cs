using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public List<ItemStack> items = new List<ItemStack>(); // Danh sách các item trong inventory
    public int maxSlots = 10; // Số lượng slot tối đa
    public SlotManager slotManager; // Tham chiếu đến SlotManager

    private float speedPotionCooldown = 0f; // Thời gian cooldown của thuốc tăng tốc
    private bool isSpeedPotionActive = false; // Kiểm tra xem thuốc tăng tốc có đang hoạt động không

    private void Update()
    {
        // Cập nhật thời gian cooldown
        if (speedPotionCooldown > 0f)
        {
            speedPotionCooldown -= Time.deltaTime;  // Giảm thời gian cooldown mỗi frame
        }

        // Kiểm tra phím bấm và cooldown trước khi sử dụng
        if (Input.GetKeyDown(KeyCode.Alpha1) && speedPotionCooldown <= 0f)
        {
            UseHealingItem();  // Sử dụng Speed Potion
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && speedPotionCooldown <= 0f)
        {
            UseSpeedPotion();  // Sử dụng Speed Potion
        }
    }

    private void UpdateSlots()
{
    // Tìm Healing Item và Speed Potion trong Inventory
    ItemStack healingStack = items.Find(stack => stack.item is HealingItem);
    ItemStack speedPotionStack = items.Find(stack => stack.item is SpeedPotion);

    // Đảm bảo `null` khi không tìm thấy item
    Item healingItem = healingStack?.item;
    int healingCount = healingStack?.quantity ?? 0;

    Item speedItem = speedPotionStack?.item;
    int speedCount = speedPotionStack?.quantity ?? 0;

    // Cập nhật UI thông qua SlotManager
    slotManager.UpdateSlot(healingItem, healingCount, speedItem, speedCount);
}


    public bool AddItem(Item item)
{
    foreach (ItemStack stack in items)
    {
        if (stack.item == item && stack.item.isStackable)
        {
            stack.quantity += 1;
            UpdateSlots(); // Cập nhật UI khi thêm item
            return true;
        }
    }

    if (items.Count < maxSlots)
    {
        items.Add(new ItemStack(item, 1));
        UpdateSlots(); // Cập nhật UI khi thêm item
        return true;
    }

    Debug.LogWarning("Không đủ slot để thêm vật phẩm.");
    return false;
}

public void RemoveItem(ItemStack stack)
{
    if (items.Contains(stack))
    {
        items.Remove(stack);
        UpdateSlots(); // Cập nhật UI khi xóa item
    }
}


    public void UseHealingItem()
    {
        ItemStack healingStack = items.Find(stack => stack.item is HealingItem);
        if (healingStack == null)
        {
            Debug.LogWarning("Không có vật phẩm hồi máu trong inventory.");
            return;
        }

        HealingItem healingItem = healingStack.item as HealingItem;
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            // Kiểm tra nếu máu hiện tại đã đầy
            if (playerHealth.currentHealth == playerHealth.maxHealth)
            {
                Debug.Log("Máu đã đầy, không thể sử dụng vật phẩm hồi máu.");
                return; // Dừng việc sử dụng vật phẩm nếu máu đã đầy
            }

            // Hồi máu
            playerHealth.Heal(healingItem.healingAmount);
        }

        // Giảm số lượng vật phẩm sau khi sử dụng
        healingStack.quantity -= 1;

        // Xóa item khỏi inventory nếu số lượng bằng 0
        if (healingStack.quantity <= 0)
        {
            items.Remove(healingStack); // Xóa item khỏi inventory
        }

        // Cập nhật lại UI sau khi sử dụng item
        UpdateSlots(); // Cập nhật UI
    }


    public void UseSpeedPotion()
    {
        if (isSpeedPotionActive)
        {
            Debug.Log("Thuốc tăng tốc đang có hiệu lực. Không thể sử dụng thêm.");
            return;
        }

        ItemStack speedPotionStack = items.Find(stack => stack.item is SpeedPotion);
        if (speedPotionStack == null || speedPotionStack.quantity == 0)
        {
            Debug.LogWarning("Không có vật phẩm tăng tốc trong inventory.");
            return;
        }
    
        SpeedPotion speedPotion = speedPotionStack.item as SpeedPotion;
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            StartCoroutine(ApplySpeedBoost(playerController, speedPotion)); // Áp dụng hiệu ứng tăng tốc
        }

        // Giảm số lượng vật phẩm sau khi sử dụng
        speedPotionStack.quantity -= 1;
        if (speedPotionStack.quantity <= 0)
        {
            items.Remove(speedPotionStack); // Xóa item nếu số lượng là 0
        }

        // Thiết lập cooldown và đánh dấu hiệu ứng đang hoạt động
        speedPotionCooldown = speedPotion.cooldownDuration;
        isSpeedPotionActive = true;

        // Cập nhật lại UI sau khi sử dụng item
        UpdateSlots(); // Cập nhật UI
    }


    private IEnumerator ApplySpeedBoost(PlayerController playerController, SpeedPotion speedPotion)
    {
        float originalSpeed = playerController.moveSpeed;
        playerController.moveSpeed += speedPotion.speedBoostAmount;

        yield return new WaitForSeconds(speedPotion.duration); // Thời gian hiệu lực

        playerController.moveSpeed = originalSpeed;  // Kết thúc hiệu ứng tăng tốc
        isSpeedPotionActive = false; // Đánh dấu hiệu ứng đã kết thúc
        Debug.Log("Hiệu ứng tăng tốc kết thúc.");
    }

}


