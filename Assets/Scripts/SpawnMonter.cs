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

    private Timer timerScript;              // Tham chiếu đến script Timer  
public static bool isNightTime; 
    // Start is called before the first frame update  
    void Start()  
    {  
        // Kiểm tra xem spawnArea và noSpawnArea có được gán chưa  
        if (spawnArea != null)  
        {  
            spawnAreaCollider = spawnArea.GetComponent<BoxCollider>();  
        }  
        else  
        {  
            Debug.LogError("Spawn Area is not assigned!");  
        }  

        if (noSpawnArea != null)  
        {  
            noSpawnAreaCollider = noSpawnArea.GetComponent<BoxCollider>();  
        }  
        else  
        {  
            Debug.LogError("No Spawn Area is not assigned!");  
        }  

        // Lấy tham chiếu đến Timer script  
        timerScript = FindObjectOfType<Timer>(); // Tìm đối tượng Timer trong scene  
        if (timerScript == null)  
        {  
            Debug.LogError("Timer script not found!");  
        }  
    }  

    // Update is called once per frame  
  private void Update()  
{  
    if (spawnCount >= maxSpawnCount)  
        return;  
    
    UpdateSpawnSettings(); // Cập nhật các thông số spawn mỗi lần trong Update

    timeSinceLastSpawn += Time.deltaTime;  
    
    if (timeSinceLastSpawn >= spawnInterval)  
    {  
        Debug.Log("Spawning " + spawnAmount + " monsters.");  
        SpawnObjects();  
        timeSinceLastSpawn = 0f;  
    }  
}  

private void UpdateSpawnSettings()  
{  
    if (Timer.isNightTime)  // Sử dụng Timer.isNightTime thay vì timerScript.isNightTime
    {  
        spawnInterval = Mathf.Max(0.5f, spawnInterval / 2f);  
        spawnAmount = Mathf.Clamp(spawnAmount * 2, 1, maxSpawnCount - spawnCount);  
        Debug.Log("It's night time. Spawn Amount: " + spawnAmount + " Interval: " + spawnInterval);  
    }  
    else  
    {  
        spawnInterval = 2f;  
        spawnAmount = 1;  
        Debug.Log("It's day time. Spawn Amount: " + spawnAmount + " Interval: " + spawnInterval);  
    }  
}

   void SpawnObjects()  
{  
    Debug.Log("Attempting to spawn " + spawnAmount + " monsters."); // Thêm log đây  
    // Kiểm tra số lượng tối đa và tăng đếm spawnCount   
    for (int i = 0; i < spawnAmount && spawnCount < maxSpawnCount; i++)  
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

            Debug.Log("Tentative Spawn Position: " + spawnPosition);  

        } while (noSpawnAreaCollider.bounds.Contains(spawnPosition));  

        // Spawn đối tượng tại vị trí spawnPosition  
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);  

        // Tăng đếm số lượng đối tượng đã spawn  
        spawnCount++;  

        Debug.Log("Spawned Object: " + spawnedObject.name + " at Position: " + spawnPosition);  
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