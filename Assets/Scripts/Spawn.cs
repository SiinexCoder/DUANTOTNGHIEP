using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{ public GameObject monsterPrefab; // Prefab quái (có thể điền từ Inspector)
    public float initialDelay = 5f; // Thời gian chờ trước khi bắt đầu spawn (có thể điền từ Inspector)
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn (có thể điền từ Inspector)
    public int monstersPerSpawn = 2; // Số lượng quái spawn mỗi lần (có thể điền từ Inspector)
    public int spawnLimit = 4; // Số lần spawn tối đa (có thể điền từ Inspector)

    // Mảng các vị trí spawn (có thể điền từ Inspector)
    public Vector3[] spawnPositions;

    void Start()
    {
        // Chờ một khoảng thời gian trước khi bắt đầu spawn quái
        StartCoroutine(StartSpawningAfterDelay());
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        // Chờ thời gian delay
        yield return new WaitForSeconds(initialDelay);

        // Bắt đầu coroutine spawn quái
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        // Lặp qua số lần spawn tối đa
        for (int i = 0; i < spawnLimit; i++)
        {
            // Spawn quái theo số lượng quy định
            SpawnMonster(monstersPerSpawn);

            // Chờ thời gian giữa các lần spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMonster(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Kiểm tra nếu có vị trí spawn
            if (spawnPositions.Length > 0)
            {
                // Chọn vị trí ngẫu nhiên từ mảng spawnPositions
                Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                // Nếu không có vị trí nào, spawn ở vị trí mặc định
                Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
