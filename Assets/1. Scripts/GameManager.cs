using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Spawner spawner;
    [Header("# Game Control")] // inspector 창에서 헤더를 만들어줌
    public bool isLive; // 게임이 진행중인지
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Infol")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public int meatCount = 0;
    public int boneCount = 0;
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    //마을 맵 만들 시, 싱글톤 고려

    public string currentSceneName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 파괴되지 않게 함
            currentSceneName = SceneManager.GetActiveScene().name; // 씬 이름을 가져옴
            SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트 핸들러를 추가

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 이미 다른 인스턴스가 있다면 파괴
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;

        if (currentSceneName == "Field")
        {
            Spawner newSpawner = FindObjectOfType<Spawner>();
            if (newSpawner != null)
            {
                spawner = newSpawner;
                spawner.enabled = true;
            }
            else
            {
                Debug.LogWarning("Spawner not found in the Field scene. Make sure it exists and has the Spawner script.");
            }
        }
        else if (currentSceneName == "Town")
        {
            if (spawner != null)
            {
                spawner.enabled = false;
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameStart() 
    {
        if (currentSceneName == "Field") // 필드 씬에서는 게임 시간이 흐름
        {
            health = maxHealth;
            uiLevelUp.Select(0);
            Resume();

            AudioManager.instance.PlayBGM(true);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        }
        else if (currentSceneName == "Town") // 마을 씬에서는 게임 시간이 흐르지 않음
        {
            health = maxHealth;
            Resume();

            AudioManager.instance.PlayBGM(true);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        }
    }

    public void GameOver()
    {
        ResetGame();  // 여기서 게임을 초기화
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
        // 씬 전환을 위해 TransferMap 클래스를 사용합니다.
        //TransferMap transferMap = FindObjectOfType<TransferMap>();
        //if (transferMap != null)
        //{
        //    transferMap.ChangeScene("Town");
        //}
        //else
        //{
        //    Debug.LogWarning("TransferMap component not found. Scene change may not work correctly.");
        //}
        ChangeScene("Town");
    }

    void Update()
    {
        if (currentSceneName == "Field") // 필드 씬에서는 게임 시간이 흐름
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
        else if (currentSceneName == "Town") // 마을 씬에서는 게임 시간이 흐르지 않음
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

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) // 레벨업에 필요한 경험치가 마지막 레벨보다 크면 마지막 레벨로 고정
        {
            level++; // 레벨업
            exp = 0; // 경험치 초기화
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

    public void ResetGame()
    {
        meatCount = 0;
        boneCount = 0;
    }

}
