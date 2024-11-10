using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;        // Đối tượng cần spawn (Prefab)
    public float spawnInterval;            // Khoảng thời gian giữa các lần spawn (giây)
    public int maxSpawnCount;              // Số lượng tối đa đối tượng được spawn trong scene
    public int spawnAmount;                // Số lượng đối tượng spawn mỗi lần (ban đầu là 2)

    public Transform spawnArea;            // Khu vực spawn (A1)
    public Transform noSpawnArea;          // Khu vực không spawn (A2)

    private BoxCollider spawnAreaCollider; // Box Collider của khu vực spawn
    private BoxCollider noSpawnAreaCollider; // Box Collider của khu vực không spawn

    private int spawnCount = 0;            // Đếm số lượng đối tượng đã spawn
    private float timeSinceLastSpawn = 0f; // Thời gian đã trôi qua từ lần spawn trước
    private float totalTime = 0f;          // Tổng thời gian đếm, dùng để kiểm tra 20 giây

    private bool hasDoubled = false;       // Cờ để đảm bảo chỉ nhân 2 một lần
    private List<SpawnedObject> spawnedObjects = new List<SpawnedObject>(); // Lưu trữ các đối tượng đã spawn

    void Start()
    {
        // Lấy Box Colliders từ các khu vực spawn
        spawnAreaCollider = spawnArea.GetComponent<BoxCollider>();
        noSpawnAreaCollider = noSpawnArea.GetComponent<BoxCollider>();
        Debug.Log("Script đã bắt đầu.");
    }

    void Update()
    {
        // Tăng thời gian đếm ngược
        timeSinceLastSpawn += Time.deltaTime;
        totalTime += Time.deltaTime;  // Cập nhật tổng thời gian đã trôi qua

        // Kiểm tra nếu đã đến thời gian spawn mới
        if (timeSinceLastSpawn >= spawnInterval && spawnCount < maxSpawnCount)
        {
            SpawnObjects();
            timeSinceLastSpawn = 0f; // Đặt lại bộ đếm sau mỗi lần spawn
        }

        // Kiểm tra nếu đã 10 giây trôi qua
        if (totalTime > 10f && totalTime <= 20f && !hasDoubled)
        {
            // 10 giây tiếp theo, thay đổi spawnAmount và spawnInterval một lần duy nhất
            spawnAmount *= 2;  // Tăng gấp đôi số lượng spawn mỗi lần
            spawnInterval /= 2f; // Giảm thời gian spawn còn một nửa
            maxSpawnCount *= 2;

            Debug.Log("Spawn Amount đã tăng lên: " + spawnAmount);
            Debug.Log("Spawn Interval đã giảm xuống: " + spawnInterval);
            Debug.Log("10 giây tiếp theo: Giá trị đã thay đổi");

            // Đánh dấu là đã nhân 2
            hasDoubled = true;
        }

        // Sau 20 giây, reset lại mọi thứ và bắt đầu lại vòng lặp
        if (totalTime >= 20f)
        {
            // Xóa hết các đối tượng dư thừa
            RemoveExcessSpawnedObjects();

            // Reset lại mọi thứ
            totalTime = 0f;   // Reset tổng thời gian
            spawnAmount = 2;  // Đặt lại giá trị spawnAmount ban đầu
            spawnInterval = 2f; // Đặt lại giá trị spawnInterval ban đầu
            maxSpawnCount = 4; // Đặt lại giá trị maxSpawnCount ban đầu
            hasDoubled = false; // Reset cờ để bắt đầu lại vòng lặp

            Debug.Log("Bắt đầu lại vòng lặp sau 20 giây.");
        }
    }

    void SpawnObjects()
    {
        // Kiểm tra nếu số lượng đối tượng đã spawn vượt quá maxSpawnCount
        if (spawnCount >= maxSpawnCount)
        {
            Debug.Log("Không thể spawn thêm đối tượng, đã đạt số lượng tối đa.");
            return; // Dừng việc spawn nếu đã đạt số lượng tối đa
        }

        // Spawn nhiều đối tượng mỗi lần
        for (int i = 0; i < spawnAmount; i++)
        {
            // Kiểm tra lại nếu tổng số đối tượng spawn đã đạt maxSpawnCount
            if (spawnCount >= maxSpawnCount)
            {
                Debug.Log("Đã đạt số lượng tối đa khi spawn, dừng lại.");
                break;  // Dừng spawn nếu đã đạt giới hạn
            }

            Vector3 spawnPosition;

            do
            {
                // Tạo vị trí ngẫu nhiên trong khu vực spawn từ Box Collider
                spawnPosition = new Vector3(
                    Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x),
                    Random.Range(spawnAreaCollider.bounds.min.y, spawnAreaCollider.bounds.max.y),
                    Random.Range(spawnAreaCollider.bounds.min.z, spawnAreaCollider.bounds.max.z)
                );
            }
            // Kiểm tra nếu spawnPosition rơi vào khu vực không spawn
            while (noSpawnAreaCollider.bounds.Contains(spawnPosition));

            // Spawn đối tượng tại vị trí spawnPosition
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Thêm đối tượng vào danh sách với thời gian spawn
            spawnedObjects.Add(new SpawnedObject(spawnedObject, totalTime));

            // Tăng đếm số lượng đối tượng đã spawn
            spawnCount++;
        }
    }

    // Phương thức được gọi khi player ăn một item (hoặc mất item)
    public void OnItemDestroyed(GameObject item)
{
    // Tìm đối tượng đã spawn và xóa nó
    for (int i = 0; i < spawnedObjects.Count; i++)
    {
        if (spawnedObjects[i].gameObject == item)
        {
            // Xóa đối tượng khỏi danh sách và scene
            Destroy(spawnedObjects[i].gameObject);
            spawnedObjects.RemoveAt(i);
            spawnCount--; // Giảm số lượng đã spawn

            Debug.Log("Đã xóa item đã bị ăn.");
            break;
        }
    }

    // Kiểm tra lại nếu spawnCount chưa đạt maxSpawnCount thì tiếp tục spawn
    if (spawnCount < maxSpawnCount)
    {
        // Gọi lại spawn để tạo item mới nếu chưa đạt maxSpawnCount
        SpawnObjects();
    }
}

    void RemoveExcessSpawnedObjects()
    {
        // Xóa bớt các đối tượng dư thừa nếu spawnCount vượt quá maxSpawnCount
        List<SpawnedObject> objectsToRemove = new List<SpawnedObject>();

        // Duyệt qua danh sách các đối tượng đã spawn
        foreach (var spawnedObj in spawnedObjects)
        {
            // Nếu đối tượng được spawn trong khoảng thời gian từ giây 10 đến giây 20, xóa nó
            if (spawnedObj.spawnTime >= 10f && spawnedObj.spawnTime <= 20f)
            {
                objectsToRemove.Add(spawnedObj);
            }
        }

        // Xóa các đối tượng trong danh sách objectsToRemove
        foreach (var obj in objectsToRemove)
        {
            spawnedObjects.Remove(obj);
            Destroy(obj.gameObject);
            spawnCount--;
            Debug.Log("Đã xóa đối tượng dư thừa.");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ khu vực spawn và không spawn để dễ quan sát trong Scene
        if (spawnAreaCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(spawnArea.position, spawnAreaCollider.size); // Vẽ khu vực spawn
        }

        if (noSpawnAreaCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(noSpawnArea.position, noSpawnAreaCollider.size); // Vẽ khu vực không spawn
        }
    }

    // Lớp phụ để lưu thông tin đối tượng đã spawn cùng với thời gian spawn
    public class SpawnedObject
    {
        public GameObject gameObject;
        public float spawnTime;

        public SpawnedObject(GameObject obj, float time)
        {
            gameObject = obj;
            spawnTime = time;
        }
    }
}
