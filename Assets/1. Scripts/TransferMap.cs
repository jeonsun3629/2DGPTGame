using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(transferMapName);
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        // 씬 전환 전, 현재 씬의 Player 객체 비활성화
        if (Player.Instance != null)
        {
            Player.Instance.gameObject.SetActive(false);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬 전환 후, 새로운 씬의 Player 객체 활성화
        if (Player.Instance != null)
        {
            Player.Instance.gameObject.SetActive(true);
        }
    }
}
