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
        gameObject.SetActive(false); // 죽으면 비활성화
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
        anim.runtimeAnimatorController = animCon[data.spriteType]; // 스프라이트 타입에 맞는 애니메이션 컨트롤러를 가져옴
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // Enemy가 살아있을 때만 아래 실행
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
            isLive = false; // 죽으면 false(위 isLive 포함된 함수 실행 안됨)
            coll.enabled = false; // 콜라이더를 끔
            rigid.simulated = false; // 물리 시뮬레이션을 끔
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

                if (controllerName.Contains("AcEnemy 1")) // 좀비
                {
                    // 50% 확률로 고기 아이템 떨어뜨리기
                    if (Random.Range(0f, 1f) < 0.6f)
                    {
                        DropItem("Meat"); // 죽으면 고기 생성
                    }
                }
                else if (controllerName.Contains("AcEnemy 2")) // 뼈다귀
                {
                    // 30% 확률로 뼈다귀 아이템 떨어뜨리기
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

            // droppedItem을 원하는대로 처리 (예: 오브젝트 풀로 반환 등)
        }

        IEnumerator Knockback() // 코루틴만의 반환형 인터페이스
        {
            yield return wait; // 다음 하나의 물리 프레임을 딜레이 시킴
            Vector3 playerPos = GameManager.Instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
    }
}
