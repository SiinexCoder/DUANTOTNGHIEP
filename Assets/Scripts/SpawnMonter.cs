using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonter : MonoBehaviour
{
 
[Header("Spawn Settings")]
    public GameObject objectToSpawn;        // Đối tượng cần spawn (Prefab)
    public float spawnInterval = 2f;        // Khoảng thời gian giữa các lần spawn (giây)
    public int maxSpawnCount = 10;          // Số lượng tối đa đối tượng được spawn
    public int spawnAmount = 1;             // Số lượng đối tượng spawn mỗi lần

    public Transform spawnArea;             // Khu vực spawn (A1)
    public Transform noSpawnArea;          // Khu vực không spawn (A2)

    private BoxCollider spawnAreaCollider;  // Box Collider của khu vực spawn
    private BoxCollider noSpawnAreaCollider; // Box Collider của khu vực không spawn

    private int spawnCount = 0;             // Đếm số lượng đối tượng đã spawn
    private float timeSinceLastSpawn = 0f;  // Thời gian đã trôi qua từ lần spawn trước

    void Start()
    {
        // Lấy Box Colliders từ các khu vực spawn
        spawnAreaCollider = spawnArea.GetComponent<BoxCollider>();
        noSpawnAreaCollider = noSpawnArea.GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Kiểm tra nếu đạt số lượng tối đa
        if (spawnCount >= maxSpawnCount)
            return;

        // Tăng thời gian đếm ngược
        timeSinceLastSpawn += Time.deltaTime;

        // Kiểm tra nếu đã đến thời gian spawn mới
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObjects();
            timeSinceLastSpawn = 0f; // Đặt lại bộ đếm sau mỗi lần spawn
        }
    }

    void SpawnObjects()
    {
        // Spawn nhiều đối tượng mỗi lần
        for (int i = 0; i < spawnAmount; i++)
        {
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
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Tăng đếm số lượng đối tượng đã spawn
            spawnCount++;
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
}
