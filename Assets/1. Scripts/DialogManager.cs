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
        if (isAction) // 대화중이면 대화창을 끄고 대화중이 아니면 대화창을 켠다.
        {
            isAction = false;
        }
        else // 대화중이 아니면 대화창을 켠다.
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";
        }
        talkPanel.SetActive(!isAction); // 대화창을 켜거나 끈다.
    }
}
