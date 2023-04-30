using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")] // inspector 창에서 헤더를 만들어줌
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Infol")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

    //마을 맵 만들 시, 싱글톤 고려

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        {
            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
            }
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level]) // 경험치가 레벨업 경험치와 같으면
        {
            level++; // 레벨업
            exp = 0; // 경험치 초기화

        }
    }
}
