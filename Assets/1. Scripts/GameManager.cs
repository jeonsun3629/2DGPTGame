using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Control")] // inspector â���� ����� �������
    public bool isLive; // ������ ����������
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Infol")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    //���� �� ���� ��, �̱��� ���

    private string currentSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǿ �ı����� �ʰ� ��
            currentSceneName = SceneManager.GetActiveScene().name; // �� �̸��� ������
            SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ �ڵ鷯�� �߰�

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �̹� �ٸ� �ν��Ͻ��� �ִٸ� �ı�
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
    }

    public void GameStart() 
    {
        health = maxHealth;
        //uiLevelUp.Select(0);
        Resume();

        AudioManager.instance.PlayBGM(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRountine());
    }

    IEnumerator GameVictoryRountine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }


    public void GameRetry()
    {
        SceneManager.LoadScene("Field");
    }

    void Update()
    {
        if (currentSceneName == "Field") // �ʵ� �������� ���� �ð��� �帧
        {
            if (!isLive)
                return;

            gameTime += Time.deltaTime;

            if (gameTime > maxGameTime)
            {
                gameTime = maxGameTime;
                GameVictory();
            }
        }
        else if (currentSceneName == "Town") // ���� �������� ���� �ð��� �帣�� ����
        {
            if (!isLive)
                return;
        }
    }

    public void GetExp()
    {
        if(!isLive) 
            return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) // �������� �ʿ��� ����ġ�� ������ �������� ũ�� ������ ������ ����
        {
            level++; // ������
            exp = 0; // ����ġ �ʱ�ȭ
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

}
