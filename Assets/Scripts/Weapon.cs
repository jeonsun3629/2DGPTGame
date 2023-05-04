using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // 시계방향으로 회전

                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // Level up Test Code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Placement();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero; // 무기의 위치 초기화

        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.Instance.pool.prefabs.Length; index++) // 무기의 프리팹과 풀의 프리팹이 같으면 프리팹 아이디를 설정
        {
            if (data.projectile == GameManager.Instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Placement();

                break;
            default:
                speed = 0.3f;
                break;
        }

        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); //모든 자식들에게 ApplyGear를 실행하라고 브로드캐스팅
    }
    
    void Placement()
    {
        for (int index = 0; index < count; index++) // 무기의 갯수만큼 루프를 돌면서 무기를 배치
        {
            Transform bullet;
            
            if (index < transform.childCount) // 기존 오브젝트 먼저 활용 후 모자란것은 풀링에서 가져오기
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform; // 풀에서 무기를 가져옴
                bullet.parent = transform; // 무기의 부모를 자신으로 설정
            }

            bullet.localPosition = Vector3.zero; // 무기의 위치 초기화
            bullet.localRotation = Quaternion.identity;


            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.3f, Space.World); // 자신의 위쪽으로 이동 (Translate 는 로컬좌표계로 이동)
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 은 무한으로 관통
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir= dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position; 
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); 
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
