using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float initialDelay = 5f;
    public float spawnInterval = 2f;
    public int monstersPerSpawn = 2;
    public int spawnLimit = 4;

    public Vector3[] spawnPositions;

    public float minDistanceBetweenMonsters = 2f; // Khoảng cách tối thiểu giữa các quái

    // Danh sách chứa các vị trí đã spawn
    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(StartSpawningAfterDelay());
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            SpawnMonster(monstersPerSpawn);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMonster(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool validPositionFound = false;

            // Thử tìm vị trí hợp lệ
            for (int attempt = 0; attempt < 10; attempt++) // Giới hạn số lần thử để tránh vòng lặp vô tận
            {
                if (spawnPositions.Length > 0)
                {
                    spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
                }
                else
                {
                    spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                }

                // Kiểm tra xem vị trí mới có đè lên các vị trí đã spawn không
                if (IsValidSpawnPosition(spawnPosition))
                {
                    validPositionFound = true;
                    break;
                }
            }

            // Nếu tìm được vị trí hợp lệ, spawn quái và lưu vị trí lại
            if (validPositionFound)
            {
                Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                spawnedPositions.Add(spawnPosition);
            }
        }
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minDistanceBetweenMonsters)
            {
                return false; // Vị trí quá gần với quái đã spawn trước đó
            }
        }
        return true; // Vị trí hợp lệ
    }
}
