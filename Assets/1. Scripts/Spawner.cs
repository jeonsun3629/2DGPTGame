using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;
    float timer;

    void Start()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.Instance.maxGameTime / spawnData.Length;
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        // 씬에 따른 스포너 활성화/비활성화 조건문 추가
        if (GameManager.Instance.currentSceneName == "Town")
        {
            return; // 마을 씬에서는 스포너를 비활성화
        }

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / levelTime), spawnData.Length - 1);  // 10초마다 난이도(레벨) 업
        
        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        if (Player.Instance != null && GameManager.Instance.spawner != null)
        {
            GameObject enemy = GameManager.Instance.pool.Get(0);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

            // 추가된 디버그 코드
            if (spawnData == null || spawnData.Length == 0)
            {
                Debug.LogError("spawnData is null or empty. Please check the Spawner's spawnData.");
                return;
            }
            if (spawnData[level] == null)
            {
                Debug.LogError($"spawnData[{level}] is null. Please check the Spawner's spawnData.");
                return;
            }
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent == null)
            {
                Debug.LogError("Enemy component not found on the enemy GameObject. Please check if the Enemy script is attached to the enemy GameObject.");
                return;
            }
            // 디버그 코드 종료

            enemyComponent.Init(spawnData[level]);
        }
        else
        {
            if (Player.Instance == null)
            {
                Debug.LogWarning("Player.Instance is null. Check if the Player object is in the scene.");
            }
            if (GameManager.Instance.spawner == null)
            {
                Debug.LogWarning("GameManager.Instance.spawner is null. Check if the Spawner object is in the scene.");
            }
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}