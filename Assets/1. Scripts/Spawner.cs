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
        GameObject enemy = GameManager.Instance.pool.Get(0); 
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position; // 0번째는 자기 자신이므로 1부터 시작
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // 레벨에 맞는 데이터를 가져옴
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