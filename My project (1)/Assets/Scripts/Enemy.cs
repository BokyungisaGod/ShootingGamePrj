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

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;
    Animator anim;
    Vector3 defaultV;
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    //public string[] itemName;

    void Awake()
    {
        //itemName = new string[] { "Plus", "Minus" };
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(enemyName=="B")
            anim=GetComponent<Animator>();
        defaultV=new Vector3(0.05f, 0.05f, 1f);
    }
    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 200;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 40;
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 3;
                break;
            case "V":
                cnt = 5;
                health = 10;
                transform.localScale = defaultV * cnt;
                break;
            case "H":
                cnt = 1;
                health = 10;
                transform.localScale = defaultV * cnt;
                break;
        }
    }
    void Stop()
    {
        if (!gameObject.activeSelf)
            return;
        Rigidbody2D rigid=GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Invoke("Think", 2);

    }
    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex) 
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }
    void FireForward()
    {
        if (health <= 0) return;
        GameObject bulletR = objectManager.MakeObj("bulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("bulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.MakeObj("bulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("bulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 4, ForceMode2D.Impulse);

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

        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI*10*curPatternCount / maxPatternCount[patternIndex]), -1);
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

        for (int index=0;index< roundNum; index++) 
        {
            GameObject bullet = objectManager.MakeObj("bulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec * 3, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward*90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
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
        else if(enemyName == "H") 
        {
            cnt++;
            transform.localScale = defaultV * cnt;
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }
    public void OnHit(int dmg)
    {
        if (health < 0|| cnt == 0&&(enemyName=="V"|| enemyName == "H"))
            return;
        
        Debug.Log("��");
        if (enemyName == "V"|| enemyName == "H") 
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
            anim.SetTrigger("OnHit");
        }
        else 
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }
        if (health <= 0||cnt==0 && (enemyName == "V" || enemyName == "H")) 
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            int ran = enemyName == "B"|| enemyName == "V" ? 0 : Random.Range(0, 10);
            if (ran < 3)
            {
                Debug.Log("not item");
            }
            if (ran < 10)
            {
                int ranNum = Random.Range(1, 4);
                int ranName = Random.Range(0, 2);
                GameObject oper = ranName == 0 ? objectManager.MakeObj("Plus" + ranNum) : objectManager.MakeObj("Minus" + ranNum);
                //GameObject Plus = objectManager.MakeObj("Plus" + ranNum);
                oper.transform.position = transform.position;
            }
            else if (ran < 6)
            {
                GameObject itemCoin = objectManager.MakeObj("itemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if (ran < 8)
            {
                GameObject itemPower = objectManager.MakeObj("itemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 10)
            {
                GameObject itemBoom = objectManager.MakeObj("itemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            gameManager.CallExplosion(transform.position, enemyName);
            transform.rotation = Quaternion.identity;

            if (enemyName == "B") 
            {
                gameManager.StageEnd();
            }

        }

    }

    void ReturnSprite() 
    {
        spriteRenderer.sprite = sprites[0];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet"&& enemyName != "B")
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
