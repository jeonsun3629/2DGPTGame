using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInspection : MonoBehaviour
{
    public DialogManager dialogManager;

    Rigidbody2D rigid;
    Vector3 dirVec;
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            dialogManager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
        //Ray°¡ ³ª¿À°Ô ²û
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
