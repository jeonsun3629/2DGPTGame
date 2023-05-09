using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage; // this = 해당 클래스의 변수로 접근
        this.per = per;

        if (per >= 0)
        {
            rigid.velocity = dir * 15f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100) // per == -100 : 투사체가 아닌 경우
            return;

        per--;

        if (per < 0) // 투사체 관통력이 다했을 때
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision) // 투사체가 화면 밖으로 나갔을 때
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;
        gameObject.SetActive(false); // 투사체 비활성화
    }
}
