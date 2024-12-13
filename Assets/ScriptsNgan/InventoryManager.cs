using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Inventory playerInventory; // Tham chiếu đến inventory của người chơi

    private void Awake()
    {
        // Kiểm tra xem instance đã tồn tại chưa, nếu có thì xóa đối tượng này
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Đảm bảo không bị hủy khi chuyển scene
        }
    }

    private void Start()
    {
        // Khi bắt đầu game, tải dữ liệu inventory
        playerInventory.LoadInventory();
    }

    public void SaveInventory()
    {
        playerInventory.SaveInventory();
    }

    public void LoadInventory()
    {
        playerInventory.LoadInventory();
    }
}
