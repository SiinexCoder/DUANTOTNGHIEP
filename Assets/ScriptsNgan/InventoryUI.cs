using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryPanel; // Panel chứa các ô item
    public GameObject inventorySlotPrefab; // Prefab slot
    public Inventory playerInventory; // Tham chiếu đến Inventory của Player

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {

        // Xóa các slot cũ trong UI
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Tạo slot mới cho từng item trong inventory
        foreach (ItemStack stack in playerInventory.items)
        {
            if (stack != null && stack.item != null)
            {
                GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);

                Image icon = slot.GetComponentInChildren<Image>();
                TextMeshProUGUI quantityText = slot.GetComponentInChildren<TextMeshProUGUI>();

                if (icon != null)
                {
                    icon.sprite = stack.item.icon;
                }

                if (quantityText != null)
                {
                    quantityText.text = stack.item.isStackable && stack.quantity > 1 ? stack.quantity.ToString() : "";
                }
            }
        }
    }

    void Update()
    {
        // Kiểm tra khi người chơi nhấn phím số 1 để sử dụng vật phẩm hồi máu
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Phím 1 được nhấn");
            Inventory inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                inventory.UseHealingItem(); // Gọi hàm sử dụng vật phẩm hồi máu
            }
        }
        // Kiểm tra phím 2 để sử dụng thuốc tăng tốc trong InventoryUI
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                inventory.UseSpeedPotion(); // Sử dụng thuốc tăng tốc
            }
        }


        // Kiểm tra phím B để mở hoặc đóng inventory
    }



}


