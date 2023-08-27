using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject playerObject; // Unity 인스펙터에서 Player 오브젝트를 할당할 변수
    public GameObject getItemObject; // Unity 인스펙터에서 GetItem 오브젝트를 할당할 변수
    public int meatCount = 0;  // 고기 아이템 카운트
    public int boneCount = 0;  // 뼈다귀 아이템 카운트

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
                GameManager.Instance.meatCount = meatCount; // GameManager의 meatCount 업데이트
                Debug.Log("Meat Count: " + meatCount);
            }
            else if (other.CompareTag("Bone"))
            {
                Destroy(other.gameObject);
                boneCount++;
                GameManager.Instance.boneCount = boneCount; // GameManager의 boneCount 업데이트
                Debug.Log("Bone Count: " + boneCount);
            }
        }
    }
}