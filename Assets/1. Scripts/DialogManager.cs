using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public void Action(GameObject scanObj)
    {
        if (isAction) // ��ȭ���̸� ��ȭâ�� ���� ��ȭ���� �ƴϸ� ��ȭâ�� �Ҵ�.
        {
            isAction = false;
        }
        else // ��ȭ���� �ƴϸ� ��ȭâ�� �Ҵ�.
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� " + scanObject.name + "�̶�� �Ѵ�.";
        }
        talkPanel.SetActive(!isAction); // ��ȭâ�� �Ѱų� ����.
    }
}
