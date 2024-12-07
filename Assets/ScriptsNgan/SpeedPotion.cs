using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedPotion", menuName = "Inventory/SpeedPotion")]
public class SpeedPotion : Item
{
    public float speedBoostAmount;  // Số lượng tăng tốc
    public float duration;  // Thời gian hiệu lực
    public float cooldownDuration;  // Thời gian hồi của thuốc

    public SpeedPotion()
    {
        itemName = "Speed Potion";  // Đặt tên item
        isStackable = true;         // Đảm bảo item có thể stack
    }

    public override void Use()
    {
        Debug.Log($"Using {itemName} to boost speed.");
    }
}
