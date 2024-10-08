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
    public GameObject inputField;  // InputField 게임 오브젝트
    public GameObject button;      // Button 게임 오브젝트
    public GameObject messageArea; // Message Area 게임 오브젝트
    public GameObject GPTClose;

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();

        // inputField가 활성화되어 있다면 talkPanel을 비활성화합니다.
        if (inputField.activeSelf)
        {
            talkPanel.SetActive(false);
        }
        else
        {
            Talk(objData.id, objData.isNPC);
            talkPanel.SetActive(isAction); // 대화창을 켜거나 끈다.
        }
    }


    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            ActivateUIElements(true);  // 여기서 UI 요소를 활성화
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
