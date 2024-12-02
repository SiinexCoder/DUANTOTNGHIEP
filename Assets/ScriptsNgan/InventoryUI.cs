using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform inventoryPanel; // Panel chứa các ô item
    public GameObject inventorySlotPrefab; // Prefab slot
    public Inventory playerInventory; // Tham chiếu đến Inventory của Player
    private bool isInventoryOpen = false; // Biến theo dõi trạng thái mở hay đóng của inventory

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
    // Kiểm tra khi người chơi nhấn phím số 3 để sử dụng vật phẩm hồi máu
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        Debug.Log("Phím 3 được nhấn");
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.UseHealingItem(); // Gọi hàm sử dụng vật phẩm hồi máu
        }
    }

    // Kiểm tra phím B để mở hoặc đóng inventory
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen; // Đảo ngược trạng thái
            inventoryPanel.gameObject.SetActive(isInventoryOpen); // Hiển thị hoặc ẩn panel

            if (isInventoryOpen)
            {
                UpdateUI(); // Cập nhật UI khi mở inventory
            }
        }
}

}


