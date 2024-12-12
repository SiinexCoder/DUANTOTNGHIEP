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

    // Healing item
    if (healingItem != null && healingCount > 0)
    {
        healingItemSlot.SetActive(true);
        healingItemCount.text = healingCount.ToString();
        Debug.Log($"Healing item count: {healingCount}");
    }
    else
    {
        healingItemSlot.SetActive(false); // Ẩn slot nếu không có item
        healingItemCount.text = "";      // Đảm bảo không hiển thị số dư thừa
        Debug.Log("Hiding healing slot.");
    }

    // Speed potion
    if (speedItem != null && speedCount > 0)
    {
        speedItemSlot.SetActive(true);
        speedItemCount.text = speedCount.ToString();
        Debug.Log($"Speed potion count: {speedCount}");
    }
    else
    {
        speedItemSlot.SetActive(false); // Ẩn slot nếu không có item
        speedItemCount.text = "";      // Xóa số lượng thừa
        Debug.Log("Hiding speed potion slot.");
    }
}



}

