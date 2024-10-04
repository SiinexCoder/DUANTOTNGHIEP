using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // Prefabs cho các loại quái
    public GameObject[] enemyPrefabs;

    // Thời gian giữa mỗi lần spawn
    public float spawnInterval = 2f;

    // Số lượng quái tối đa trong scene
    public int maxEnemies = 10;

    // Vị trí để spawn quái
    public Transform[] spawnPoints;

    // Biến để theo dõi số lượng quái hiện tại
    private int currentEnemyCount = 0;

    void Start()
    {
        // Bắt đầu vòng lặp spawn quái
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Kiểm tra nếu số lượng quái hiện tại nhỏ hơn số lượng tối đa
            if (currentEnemyCount < maxEnemies)
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

            // Đợi một khoảng thời gian trước khi spawn quái tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Hàm để giảm số lượng quái khi một quái bị tiêu diệt
    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
