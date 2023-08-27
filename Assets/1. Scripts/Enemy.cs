using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public GameObject meatPrefab;
    public GameObject bonePrefab;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void Dead()
    {
        gameObject.SetActive(false); // ������ ��Ȱ��ȭ
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;

        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType]; // ��������Ʈ Ÿ�Կ� �´� �ִϸ��̼� ��Ʈ�ѷ��� ������
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // Enemy�� ������� ���� �Ʒ� ����
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(Knockback());

        if (health > 0)
        {
            // Live, Hit Action
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

        }
        else
        {
            // Dead
            isLive = false; // ������ false(�� isLive ���Ե� �Լ� ���� �ȵ�)
            coll.enabled = false; // �ݶ��̴��� ��
            rigid.simulated = false; // ���� �ùķ��̼��� ��
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();

            if (GameManager.Instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);

            DropItemBasedOnEnemyType();
        }

        void DropItemBasedOnEnemyType()
        {
            var currentController = anim.runtimeAnimatorController as AnimatorOverrideController;

            if (currentController != null)
            {
                string controllerName = currentController.name;

                if (controllerName.Contains("AcEnemy 1")) // ����
                {
                    // 50% Ȯ���� ��� ������ ����߸���
                    if (Random.Range(0f, 1f) < 0.6f)
                    {
                        DropItem("Meat"); // ������ ��� ����
                    }
                }
                else if (controllerName.Contains("AcEnemy 2")) // ���ٱ�
                {
                    // 30% Ȯ���� ���ٱ� ������ ����߸���
                    if (Random.Range(0f, 1f) < 0.1f)
                    {
                        DropItem("Bone");
                    }
                }
            }
        }

        void DropItem(string itemType)
        {
            GameObject droppedItem = null;

            if (itemType == "Meat")
            {
                droppedItem = Instantiate(meatPrefab, transform.position, Quaternion.identity);
                Debug.Log("Meat instantiated at " + transform.position);
            }
            else if (itemType == "Bone")
            {
                droppedItem = Instantiate(bonePrefab, transform.position, Quaternion.identity);
                Debug.Log("Bone instantiated at " + transform.position);
            }

            // droppedItem�� ���ϴ´�� ó�� (��: ������Ʈ Ǯ�� ��ȯ ��)
        }

        IEnumerator Knockback() // �ڷ�ƾ���� ��ȯ�� �������̽�
        {
            yield return wait; // ���� �ϳ��� ���� �������� ������ ��Ŵ
            Vector3 playerPos = GameManager.Instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
    }
}
