using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Item", menuName = "Inventory/Healing Item")]
public class HealingItem : Item
{
    public int healingAmount;  // Lượng máu hồi khi sử dụng
}
