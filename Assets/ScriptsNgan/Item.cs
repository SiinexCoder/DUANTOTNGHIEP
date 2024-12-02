using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Tên item
    public Sprite icon; // Icon hiển thị của item
    public bool isStackable; // Xác định item có thể stack hay không

    // Phương thức khi sử dụng item
    public virtual void Use()
    {
        Debug.Log($"Using {itemName}");
    }
}
