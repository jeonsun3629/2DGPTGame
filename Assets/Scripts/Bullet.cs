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

        if (per > -1)
        {
            rigid.velocity = dir * 15f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) // per == -1 : 투사체가 아닌 경우
            return;

        per--;

        if (per == -1) // 투사체 관통력이 다했을 때
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
