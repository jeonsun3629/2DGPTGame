using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public GameObject inputField;  // InputField ���� ������Ʈ
    public GameObject button;      // Button ���� ������Ʈ
    public GameObject messageArea; // Message Area ���� ������Ʈ
    public GameObject GPTClose;

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        // inputField�� Ȱ��ȭ�Ǿ� �ִٸ� talkPanel�� ��Ȱ��ȭ�մϴ�.
        if (inputField.activeSelf)
        {
            talkPanel.SetActive(false);
        }
        else
        {
            Talk(objData.id, objData.isNPC);
            talkPanel.SetActive(isAction); // ��ȭâ�� �Ѱų� ����.
        }
    }


    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            ActivateUIElements(true);  // ���⼭ UI ��Ҹ� Ȱ��ȭ
            return;
        }

        if (isNPC)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

    public void ActivateUIElements(bool activate)
    {
        inputField.SetActive(activate);
        button.SetActive(activate);
        messageArea.SetActive(activate);
        GPTClose.SetActive(activate);
    }
}
