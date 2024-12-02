using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel chứa các slot item (balo)
    public GameObject inventoryUI;    // UI chứa các nút, thông tin khác

    private bool isInventoryVisible = false; // Trạng thái của inventory (mở/đóng)

    // Hàm bật/tắt giao diện balo
    public void ToggleInventoryUI()
    {
        isInventoryVisible = !isInventoryVisible; // Đảo ngược trạng thái
        inventoryPanel.SetActive(isInventoryVisible); // Hiển thị hoặc ẩn panel chứa item
        inventoryUI.SetActive(isInventoryVisible);    // Có thể để ẩn nếu không muốn UI phụ xuất hiện
    }
}
