using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public TMP_Text healingItemCount;
    public TMP_Text speedItemCount;
    public GameObject healingItemSlot;  // Các ô slot cho healing item
    public GameObject speedItemSlot;    // Các ô slot cho speed potion

    // Cập nhật slot với item và số lượng
    public void UpdateSlot(Item healingItem, int healingCount, Item speedItem, int speedCount)
    {
        Debug.Log("Updating slots...");

        // Kiểm tra nếu có healing item và cập nhật UI
        if (healingItem != null && healingCount > 0)
        {
            healingItemSlot.SetActive(true);  // Hiển thị ô slot
            healingItemCount.text = healingCount.ToString();  // Cập nhật số lượng
            Debug.Log($"Healing item count: {healingCount}");
        }
        else
        {
            healingItemSlot.SetActive(false);  // Ẩn ô slot nếu không có item hoặc số lượng là 0
            healingItemCount.text = "";  // Xóa số lượng khi không có item
        }

        // Kiểm tra nếu có speed potion và cập nhật UI
        if (speedItem != null && speedCount > 0)
        {
            speedItemSlot.SetActive(true);  // Hiển thị ô slot
            speedItemCount.text = speedCount.ToString();  // Cập nhật số lượng
            Debug.Log($"Speed potion count: {speedCount}");
        }
        else
        {
            speedItemSlot.SetActive(false);  // Ẩn ô slot nếu không có item hoặc số lượng là 0
            speedItemCount.text = "";  // Xóa số lượng khi không có item
        }
    }


}

