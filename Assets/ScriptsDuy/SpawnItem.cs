using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;        // Đối tượng cần spawn (Prefab)
    public float spawnInterval = 2f;        // Khoảng thời gian giữa các lần spawn (giây)
    public int maxSpawnCount = 5;           // Số lượng tối đa đối tượng được spawn
    public Transform spawnArea;             // Khu vực spawn (A1)
    public Transform noSpawnArea;           // Khu vực không spawn (A2)

    private BoxCollider spawnAreaCollider;  // Box Collider của khu vực spawn
    private BoxCollider noSpawnAreaCollider; // Box Collider của khu vực không spawn

    private List<GameObject> activeObjects = new List<GameObject>(); // Danh sách các đối tượng hiện tại
    private float timeSinceLastSpawn = 0f;  // Bộ đếm thời gian cho việc spawn

    [Header("Audio Settings")]
    public AudioClip spawnSound;            // Âm thanh phát khi spawn
    private AudioSource audioSource;        // Audio Source để phát âm thanh

    void Start()
    {
        // Lấy Box Colliders từ các khu vực spawn
        spawnAreaCollider = spawnArea.GetComponent<BoxCollider>();
        noSpawnAreaCollider = noSpawnArea.GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>(); // Lấy AudioSource từ GameObject
    }

    void Update()
    {
        // Loại bỏ các đối tượng đã bị tiêu diệt khỏi danh sách
        activeObjects.RemoveAll(obj => obj == null);

        // Kiểm tra nếu cần spawn thêm đối tượng
        if (activeObjects.Count < maxSpawnCount)
        {
            // Tăng bộ đếm thời gian
            timeSinceLastSpawn += Time.deltaTime;

            // Spawn khi đủ thời gian
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnObject();
                timeSinceLastSpawn = 0f; // Đặt lại bộ đếm
            }
        }
    }

    void SpawnObject()
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
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Thêm đối tượng spawn vào danh sách
        activeObjects.Add(spawnedObject);

        // Phát âm thanh khi spawn
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound); // Phát âm thanh spawn
        }
    }

    void OnDrawGizmosSelected()
    {
        // Vẽ khu vực spawn và không spawn để dễ quan sát trong Scene
        if (spawnArea != null && spawnAreaCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(spawnArea.position, spawnAreaCollider.size); // Vẽ khu vực spawn
        }

        if (noSpawnArea != null && noSpawnAreaCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(noSpawnArea.position, noSpawnAreaCollider.size); // Vẽ khu vực không spawn
        }
    }
}
