using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public int cnt;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector3 defaultV;

    public Transform barScale;
    public Vector3 defaultScale;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;


    public float bulletSpeed = 5f; // �Ѿ��� �߻� �ӵ�

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    public float maxHealth;
    //public string[] itemName;

    void Awake()
    {
        //itemName = new string[] { "Plus", "Minus" };
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (enemyName == "B")
            anim = GetComponent<Animator>();
        defaultV = new Vector3(0.05f, 0.05f, 1f);
    }
    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 200;
                maxHealth = health;
                defaultScale = barScale.localScale;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 7;
                break;
            case "M":
                health = 5;
                break;
            case "S":
                health = 3;
                break;
            case "V":
                cnt = 5;
                health =5;
                transform.localScale = defaultV * cnt;
                break;
            case "H":
                cnt = 1;
                health = 5;
                transform.localScale = defaultV * cnt;
                break;
        }
    }
    void Stop()
    {
        if (!gameObject.activeSelf)
            return;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Invoke("Think", 2);

    }
    void Think()
    {
        patternIndex = patternIndex == 8 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireDiagonal();
                //FireRotation();
                break;
            case 2:
                FireKnife();
                //FireAround();
                break;
            case 3:
                FireShot(); //�������� ���� �����°�
                break;
            case 4:
                FireRandom();
                break;
            case 5:
                FireArc();
                //FireSpiral();
                break;
            case 6:
                //FireRotation();
                break;
            case 7:
                //FireArc();
                break;
        }
    }
    void FireForward()
    {
        if (health <= 0) return;
        GameObject bulletR = objectManager.MakeObj("bulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        //GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        //bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        //GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        //bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        //Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        //Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        //rigidRR.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        //rigidLL.AddForce(Vector2.down * 4, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 3);

    }
    void FireShot()
    {
        if (health <= 0) return;
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec * 1, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {
        if (health <= 0) return;
        GameObject bullet = objectManager.MakeObj("bulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec * 3, ForceMode2D.Impulse);

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }
    void FireAround()
    {
        if (health <= 0) return;
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec * 3, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void FireKnife()
    {
        if (health <= 0) return;
        int cnt = 15; // �߻��� �Ѿ� ����
        float startAngle1 = 225f; // 5�� ���� ���� ����
        float startAngle2 = 135f; // 7�� ���� ���� ����
        float rotationAngle = 15f; // �� �Ѿ��� ȸ�� ����

        // �����ư��� ���� ���� ����
        float startAngle = (curPatternCount % 2 == 0) ? startAngle1 : startAngle2;

        // �߻��� �Ѿ��� �ʱ� �ӵ�
        float initialSpeed = 5f;

        for (int index = 0; index < cnt; index++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");

            // ���� �Ѿ��� �߻� ���� ���
            float bulletAngle = startAngle + index * rotationAngle;

            // �߻� ������ �������� ��ȯ
            float radians = bulletAngle * Mathf.Deg2Rad;

            // �Ѿ��� �ʱ� ���� ���� ���
            Vector2 initialDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // �Ѿ��� �ʱ� �ӵ� ���� ���
            Vector2 initialVelocity = initialDirection * initialSpeed;

            // �Ѿ� ��ġ ����
            bullet.transform.position = transform.position; // ������ ��ġ�� �ʱ�ȭ

            // �Ѿ��� �������� ȸ��
            bullet.transform.position = transform.position; // ������ ��ġ�� �ʱ�ȭ
            //�Ѿ��� ȸ�� ����
            float bulletRotationAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, bulletRotationAngle + 90f); // �Ѿ��� ���ư��� �������� �Ӹ��� ������ 90�� ȸ��
            bullet.transform.rotation = rotation;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            // �Ѿ˿� �ʱ� �ӵ��� �ο�
            rigid.velocity = initialVelocity;

            // �ð��� ������ ���� �Ѿ��� �ӵ��� ���ݾ� �������� ���������� �߻�ǵ��� ��
            StartCoroutine(IncreaseBulletSpeedOverTime(rigid, index * 0.1f));
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireKnife", 2f);
        else
            Invoke("Think", 3f);
    }

    IEnumerator IncreaseBulletSpeedOverTime(Rigidbody2D bulletRigidbody, float delay)
    {
        yield return new WaitForSeconds(delay);

        // �Ѿ��� ���� �ӵ��� ������
        Vector2 currentVelocity = bulletRigidbody.velocity;

        // ���� �ӵ��� �������� ���ʽ� �ӵ��� ������
        currentVelocity += currentVelocity.normalized * 1.5f;

        // �Ѿ��� �ӵ��� ������Ʈ
        bulletRigidbody.velocity = currentVelocity;
    }

    void FireRotation()
    {
        if (health <= 0) return;
        int bulletCount = 20; // źȯ�� ����
        float angleStep = 360f / bulletCount; // ���� ����
        float rotationOffset = 5f;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            float angle = i * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;

            // ¦�� ��° �Ѿ��� �������� ȸ��, Ȧ�� ��° �Ѿ��� ���������� ȸ��
            float rotationDirection = i % 2 == 0 ? -1 : 1;
            Quaternion rotation = Quaternion.Euler(0, 0, angle + rotationDirection * rotationOffset);
            bullet.transform.rotation = rotation;

            // ¦�� ��° �Ѿ��� �������� ������ ����
            if (i % 2 == 0)
                dir = Quaternion.Euler(0, 0, angle - rotationOffset) * Vector2.down;

            rigid.AddForce(dir * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireRotation", 0.3f);
        else
            Invoke("Think", 3);

    }

    void FireDiagonal()
    {
        //if (health <= 0) return;
        //int bulletCount = 10; // źȯ�� ����

        //for (int i = 0; i < bulletCount; i++)
        //{
        //    GameObject bullet = objectManager.MakeObj("bulletBossB");
        //    bullet.transform.position = transform.position;
        //    bullet.transform.rotation = Quaternion.identity;
        //    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        //    Vector2 dir = (player.transform.position - transform.position).normalized;
        //    dir.x += i * 0.2f; // x �������� ������ �������� �̵�
        //    rigid.AddForce(dir * 3, ForceMode2D.Impulse);
        //}

        //curPatternCount++;
        //if (curPatternCount < maxPatternCount[patternIndex])
        //    Invoke("FireDiagonal", 0.5f);
        //else
        //    Invoke("Think", 3);
        if (health <= 0) return;
        int bulletCount = 6; // źȯ�� �� ����
        int rightBullets = 3; // ���������� ������ źȯ ����
        int leftBullets = bulletCount - rightBullets; // �������� ������ źȯ ����

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dir = (player.transform.position - transform.position).normalized;
            // ���������� 3��, �������� 3�� �����ư��� �̵��ϴ� ����
            if (i < rightBullets)
                dir.x += i * 0.2f; // ���������� �̵�
            else
                dir.x -= (i - rightBullets) * 0.2f; // �������� �̵�
            rigid.AddForce(dir * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireDiagonal", 0.5f);
        else
            Invoke("Think", 3);
    }

    void FireRandom()
    {
        if (health <= 0)
            return;

        // �÷��̾ ���ϴ� ���� ���� ���
        Vector2 dirToPlayer = (player.transform.position - transform.position).normalized;

        // �Ѿ� �߻�
        GameObject bullet = objectManager.MakeObj("bulletBossB");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        // �÷��̾���� ���� ����Ͽ� ���� ���� ����
        float angleToPlayer = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);
        rigid.velocity = rotation * Vector2.right * bulletSpeed;

        // ���� ���� ������ ���� ���� Ȯ��
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireRandom", 0.1f); // ��� ȣ��� ���� �߻� ���� ����
        else
            Invoke("Think", 3); // ���� �������� �Ѿ�� ���� Think �޼ҵ� ȣ�� ����
    }




    void FireSpiral()
    {
        if (health <= 0)
            return;

        int bulletCount = 20; // �߻��� �Ѿ� ����
        float angleStep = 360f / bulletCount; // ���� ����

        // ȸ�� �ӵ�
        float rotateSpeed = 80f;

        for (int i = 0; i < bulletCount; i++)
        {
            // �߻�� ���� ���
            float angle = i * angleStep;

            // �߻�� ������ ���� ȸ�� ������ ���Ͽ� ȸ����Ŵ
            float rotatedAngle = angle + (curPatternCount * rotateSpeed);

            // ������ �������� ��ȯ�Ͽ� �ڻ��ΰ� ���� �Լ��� ����Ͽ� ���� ����
            float x = Mathf.Cos(rotatedAngle * Mathf.Deg2Rad);
            float y = Mathf.Sin(rotatedAngle * Mathf.Deg2Rad);

            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = new Vector2(x, y);
            rigid.AddForce(direction * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireSpiral", 0.3f); // ��� ȣ��� ���� �߻� ���� ����
        else
            Invoke("Think", 3); // ���� �������� �Ѿ�� ���� Think �޼ҵ� ȣ�� ����
    }






    void Update()
    {
        if (enemyName == "B")
            return;
        Fire();
        Reload();
    }
    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;
        if (enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("bulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec * 1, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("bulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.MakeObj("bulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);

        }
        else if (enemyName == "H")
        {
            cnt += 3;
            transform.localScale = defaultV * cnt * 3f;
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }
    public void OnHit(int dmg)
    {
        if (!gameObject.activeSelf || cnt == 0 && (enemyName == "V" || enemyName == "H"))
            return;

        if (enemyName == "V" || enemyName == "H")
        {
            cnt--;
            transform.localScale = defaultV * cnt;
        }
        else
        {

            health -= dmg;
        }

        if (enemyName == "B")
        {
            UpdateHealthBar();
        }
        //else
        //{
        //    spriteRenderer.sprite = sprites[1];
        //    Invoke("ReturnSprite", 0.1f);
        //}
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 1f);
        if (health <= 0 || cnt == 0 && (enemyName == "V" || enemyName == "H"))
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            int ran = enemyName == "B" ? 0 : Random.Range(1, 10);
            //if (ran < 3)
            //{
            //    Debug.Log("not item");
            //}
            if (ran > 0)
            {
                int ranNum = Random.Range(1, 4);
                int ranName = Random.Range(0, 2);
                GameObject oper = ranName == 0 ? objectManager.MakeObj("Plus" + ranNum) : objectManager.MakeObj("Minus" + ranNum);
                //GameObject Plus = objectManager.MakeObj("Plus" + ranNum);
                oper.transform.position = transform.position;
            }
            //else if (ran < 6)
            //{
            //    GameObject itemCoin = objectManager.MakeObj("itemCoin");
            //    itemCoin.transform.position = transform.position;
            //}
            //else if (ran < 8)
            //{
            //    GameObject itemPower = objectManager.MakeObj("itemPower");
            //    itemPower.transform.position = transform.position;
            //}
            //else if (ran < 10)
            //{
            //    GameObject itemBoom = objectManager.MakeObj("itemBoom");
            //    itemBoom.transform.position = transform.position;
            //}
            gameObject.SetActive(false);
            gameManager.CallExplosion(transform.position, enemyName);
            transform.rotation = Quaternion.identity;

            if (enemyName == "B")
            {
                barScale.localScale = defaultScale;
                Invoke("GameOver", 6);
                gameManager.StageEnd();
            }

        }

    }

    void UpdateHealthBar()
    {
        Vector3 newScale = barScale.localScale;
        newScale.x = defaultScale.x * health / maxHealth;
        barScale.localScale = newScale;
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}