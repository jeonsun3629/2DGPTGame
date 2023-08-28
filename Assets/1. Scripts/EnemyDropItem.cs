using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject playerObject; // Unity 인스펙터에서 Player 오브젝트를 할당할 변수
    public GameObject getItemObject; // Unity 인스펙터에서 GetItem 오브젝트를 할당할 변수
    public int meatCount = 0;  // 고기 아이템 카운트
    public int boneCount = 0;  // 뼈다귀 아이템 카운트

    void Awake()
    {
        LoadGame();  // 게임이 시작될 때 저장된 값을 불러옵니다.
        SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트에 메서드를 추가합니다.
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveGame();  // 씬이 바뀔 때마다 게임 데이터를 저장합니다.
    }

    void OnApplicationQuit()
    {
        SaveGame();  // 어플리케이션이 종료되기 전에 게임을 저장합니다.
    }

    void Update()
    {
        // GetItem의 위치를 Player의 위치로 설정
        getItemObject.transform.position = playerObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Item Collision detected with: " + other.gameObject.name
                  + ", Triggered by: " + gameObject.name
                  + ", Tag of Trigger: " + this.tag
                  + ", Tag of Collided Item: " + other.tag);

        // getItemObject의 태그가 "GetItem"인지 확인
        if (getItemObject.CompareTag("GetItem"))
        {
            if (other.CompareTag("Meat"))
            {
                Destroy(other.gameObject);
                meatCount++;
                Debug.Log("Meat Count: " + meatCount);
                SaveGame();
            }
            else if (other.CompareTag("Bone"))
            {
                Destroy(other.gameObject);
                boneCount++;
                Debug.Log("Bone Count: " + boneCount);
                SaveGame();
            }
        }
    }

    // 저장
    public void SaveGame()
    {
        PlayerPrefs.SetInt("MeatCount", meatCount);
        PlayerPrefs.SetInt("BoneCount", boneCount);
        PlayerPrefs.Save();
    }

    // 불러오기
    public void LoadGame()
    {
        meatCount = PlayerPrefs.GetInt("MeatCount", 0);
        boneCount = PlayerPrefs.GetInt("BoneCount", 0);
    }
}