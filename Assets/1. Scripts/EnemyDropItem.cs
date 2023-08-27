using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject playerObject; // Unity �ν����Ϳ��� Player ������Ʈ�� �Ҵ��� ����
    public GameObject getItemObject; // Unity �ν����Ϳ��� GetItem ������Ʈ�� �Ҵ��� ����
    public int meatCount = 0;  // ��� ������ ī��Ʈ
    public int boneCount = 0;  // ���ٱ� ������ ī��Ʈ

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
                GameManager.Instance.meatCount = meatCount; // GameManager�� meatCount ������Ʈ
                Debug.Log("Meat Count: " + meatCount);
            }
            else if (other.CompareTag("Bone"))
            {
                Destroy(other.gameObject);
                boneCount++;
                GameManager.Instance.boneCount = boneCount; // GameManager�� boneCount ������Ʈ
                Debug.Log("Bone Count: " + boneCount);
            }
        }
    }
}