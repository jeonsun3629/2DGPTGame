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
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // �ð�������� ȸ��

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
        transform.localPosition = Vector3.zero; // ������ ��ġ �ʱ�ȭ

        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for (int index = 0; index < GameManager.Instance.pool.prefabs.Length; index++) // ������ �����հ� Ǯ�� �������� ������ ������ ���̵� ����
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

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); //��� �ڽĵ鿡�� ApplyGear�� �����϶�� ��ε�ĳ����
    }
    
    void Placement()
    {
        for (int index = 0; index < count; index++) // ������ ������ŭ ������ ���鼭 ���⸦ ��ġ
        {
            Transform bullet;
            
            if (index < transform.childCount) // ���� ������Ʈ ���� Ȱ�� �� ���ڶ����� Ǯ������ ��������
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform; // Ǯ���� ���⸦ ������
                bullet.parent = transform; // ������ �θ� �ڽ����� ����
            }

            bullet.localPosition = Vector3.zero; // ������ ��ġ �ʱ�ȭ
            bullet.localRotation = Quaternion.identity;


            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.3f, Space.World); // �ڽ��� �������� �̵� (Translate �� ������ǥ��� �̵�)
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 �� �������� ����

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

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);

    }
}