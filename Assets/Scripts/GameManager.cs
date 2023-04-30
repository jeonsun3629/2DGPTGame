using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")] // inspector â���� ����� �������
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

    //���� �� ���� ��, �̱��� ���

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

        if (exp == nextExp[level]) // ����ġ�� ������ ����ġ�� ������
        {
            level++; // ������
            exp = 0; // ����ġ �ʱ�ȭ

        }
    }
}
