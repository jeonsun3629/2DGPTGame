using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject playerObject; // Unity �ν����Ϳ��� Player ������Ʈ�� �Ҵ��� ����
    public GameObject getItemObject; // Unity �ν����Ϳ��� GetItem ������Ʈ�� �Ҵ��� ����
    public int meatCount = 0;  // ��� ������ ī��Ʈ
    public int boneCount = 0;  // ���ٱ� ������ ī��Ʈ

    void Awake()
    {
        LoadGame();  // ������ ���۵� �� ����� ���� �ҷ��ɴϴ�.
        SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ�� �޼��带 �߰��մϴ�.
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SaveGame();  // ���� �ٲ� ������ ���� �����͸� �����մϴ�.
    }

    void OnApplicationQuit()
    {
        SaveGame();  // ���ø����̼��� ����Ǳ� ���� ������ �����մϴ�.
    }

    void Update()
    {
        // GetItem�� ��ġ�� Player�� ��ġ�� ����
        getItemObject.transform.position = playerObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Item Collision detected with: " + other.gameObject.name
                  + ", Triggered by: " + gameObject.name
                  + ", Tag of Trigger: " + this.tag
                  + ", Tag of Collided Item: " + other.tag);

        // getItemObject�� �±װ� "GetItem"���� Ȯ��
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

    // ����
    public void SaveGame()
    {
        PlayerPrefs.SetInt("MeatCount", meatCount);
        PlayerPrefs.SetInt("BoneCount", boneCount);
        PlayerPrefs.Save();
    }

    // �ҷ�����
    public void LoadGame()
    {
        meatCount = PlayerPrefs.GetInt("MeatCount", 0);
        boneCount = PlayerPrefs.GetInt("BoneCount", 0);
    }
}