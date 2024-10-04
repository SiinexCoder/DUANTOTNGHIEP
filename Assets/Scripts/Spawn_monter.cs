using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs; // Mảng các prefab quái

    [Header("Spawn Settings")]
    public float initialDelay = 2f;   // Thời gian chờ trước khi bắt đầu spawn (giây)
    public float spawnInterval = 1f;   // Thời gian giữa các lần spawn (giây)
    public int enemiesPerSpawn = 2;     // Số lượng quái tối đa có thể spawn mỗi lần
    public int totalSpawnCycles = 10;   // Số lần có thể spawn quái trong màn chơi

    public Transform[] spawnPoints;      // Vị trí để spawn quái

    private int currentEnemyCount = 0;   // Biến để theo dõi số lượng quái hiện tại
    private int currentSpawnCycles = 0;  // Biến để theo dõi số lần spawn đã xảy ra

    void Start()
    {
        // Bắt đầu vòng lặp spawn quái sau thời gian chờ
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        // Chờ thời gian chờ trước khi bắt đầu spawn
        yield return new WaitForSeconds(initialDelay);

        while (currentSpawnCycles < totalSpawnCycles)
        {
            // Kiểm tra xem có thể spawn quái hay không
            if (currentEnemyCount < enemiesPerSpawn * totalSpawnCycles)
            {
                // Tính số lượng quái có thể spawn lần này
                int enemiesToSpawn = Mathf.Min(enemiesPerSpawn, totalSpawnCycles - currentSpawnCycles);

                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    // Chọn ngẫu nhiên một vị trí spawn
                    int randomIndex = Random.Range(0, spawnPoints.Length);
                    Transform spawnPoint = spawnPoints[randomIndex];

                    // Chọn ngẫu nhiên một loại quái
                    int enemyIndex = Random.Range(0, enemyPrefabs.Length);
                    GameObject enemyPrefab = enemyPrefabs[enemyIndex];

                    // Sinh ra quái tại vị trí đã chọn
                    Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                    // Tăng số lượng quái hiện tại
                    currentEnemyCount++;
                }

                // Tăng số lần spawn
                currentSpawnCycles++;
            }

            // Đợi một khoảng thời gian trước khi spawn quái tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Hàm để giảm số lượng quái khi một quái bị tiêu diệt
    public void EnemyDestroyed()
    {
        if (currentEnemyCount > 0)
        {
            currentEnemyCount--;
        }
    }
}
