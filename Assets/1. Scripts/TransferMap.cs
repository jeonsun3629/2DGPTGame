using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // �̵��� ���� �̸�

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
        // �� ��ȯ ��, ���� ���� Player ��ü ��Ȱ��ȭ
        if (Player.Instance != null)
        {
            Player.Instance.gameObject.SetActive(false);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // �� ��ȯ ��, ���ο� ���� Player ��ü Ȱ��ȭ
        if (Player.Instance != null)
        {
            Player.Instance.gameObject.SetActive(true);
        }
    }
}
