using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Player : MonoBehaviour
{
    // Update is called once per frame
    
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;
    public int score;
    public float speed;
    public int boom;
    public int maxBoom;
    public int power;
    public int maxPower;
    public float maxShotDelay;
    public float curShotDelay;


    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;
    public GameManager gameManager;
    public ObjectManager objectManager;
    public bool isHit;
    public bool isBoomTime;
    public bool isRespawnTime;
    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA;
    public bool isButtonB;

    public GameObject[] followers;
    AudioManager audiomanager;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void OnEnable()
    {
        unbeatable();
        Invoke("unbeatable", 3);
    }
    void unbeatable() 
    {
        isRespawnTime = !isRespawnTime;

        if (isRespawnTime) 
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else 
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
        }
    }
    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }


    void Fire()
    {
        //if (!Input.GetButton("Fire1"))
        //    return;
        if (!isButtonA)
            return;
        if (curShotDelay < maxShotDelay)
            return;
        audiomanager.PlaySFX(audiomanager.attack);
        if (PlayerPrefs.GetInt("Character") == 1) 
        {
            switch (power)
            {
                case 1:
                    //Power one
                    GameObject bullet = objectManager.MakeObj("bulletPlayerA");
                    bullet.transform.position = transform.position;
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
                case 2:
                    //Power one
                    GameObject bulletR = objectManager.MakeObj("bulletPlayerA");
                    bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                    GameObject bulletL = objectManager.MakeObj("bulletPlayerA");
                    bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
                default:
                    //Power one
                    GameObject bulletRR = objectManager.MakeObj("bulletPlayerA");
                    bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                    GameObject bulletCC = objectManager.MakeObj("bulletPlayerB");
                    bulletCC.transform.position = transform.position;
                    GameObject bulletLL = objectManager.MakeObj("bulletPlayerA");
                    bulletLL.transform.position = transform.position + Vector3.left * 0.35f;
                    Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                    rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
            }
            
        }
        else
        {
            switch (power)
            {
                case 1:
                    //Power one
                    GameObject bullet = objectManager.MakeObj("bulletPlayerA");
                    bullet.transform.position = transform.position + Vector3.up * 0.75f;
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
                case 2:
                    //Power one
                    GameObject bulletR = objectManager.MakeObj("bulletPlayerA");
                    bulletR.transform.position = transform.position + Vector3.right * 0.1f + Vector3.up * 0.75f;
                    GameObject bulletL = objectManager.MakeObj("bulletPlayerA");
                    bulletL.transform.position = transform.position + Vector3.left * 0.1f + Vector3.up * 0.75f;
                    Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                    rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
                default:
                    //Power one
                    GameObject bulletRR = objectManager.MakeObj("bulletPlayerA");
                    bulletRR.transform.position = transform.position + Vector3.right * 0.35f + Vector3.up * 0.75f;
                    GameObject bulletCC = objectManager.MakeObj("bulletPlayerA");
                    bulletCC.transform.position = transform.position + Vector3.up * 0.75f;
                    GameObject bulletLL = objectManager.MakeObj("bulletPlayerA");
                    bulletLL.transform.position = transform.position + Vector3.left * 0.35f + Vector3.up * 0.75f;
                    Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                    Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                    rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                    break;
            }
        }


        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }

    public void JoyPanel(int type) 
    {
        for(int index=0; index<9;index++) 
        {
            joyControl[index] = index == type;
        }
    }
    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (joyControl[0]) { h = -1;v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }

        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft)||!isControl)
            h = 0;
        
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom) || !isControl)
            v = 0;
        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if ((Input.GetButtonDown("Horizontal")) || (Input.GetButtonUp("Vertical")))
        {
            anim.SetInteger("Input", (int)h);
        }
    }
    public void ButtonADown() 
    {
        isButtonA = true;
        
    }
    public void ButtonAUp()
    {
        isButtonA = false;
    }
    public void ButtonBDown()
    {
        isButtonB = true;
        
    }
    void Boom()
    {
        //if (!Input.GetButton("Fire2"))
        //    return;
        if (!isButtonB)
            return;
        if (isBoomTime)
            return;
        if (boom == 0)
            return;
        audiomanager.PlaySFX(audiomanager.boom);
        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);

        boomEffect.SetActive(true);
        Invoke("offBoomEffect", 3f);
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf) 
            {
                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {
                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {
                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }
        GameObject[] bulletsA = objectManager.GetPool("bulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("bulletEnemyB");
        for (int index = 0; index < bulletsA.Length; index++)
        {
            if (bulletsA[index].activeSelf)
            {
                bulletsA[index].SetActive(false);
            } 
        }
        for (int index = 0; index < bulletsB.Length; index++)
        {
            if (bulletsB[index].activeSelf)
            {
                bulletsB[index].SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;

            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isRespawnTime)
                return;
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");
            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }

            gameObject.SetActive(false);
            //if(collision.gameObject.name != "Enemy B") 
            //{
            //    collision.gameObject.SetActive(false);
            //}
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Item") 
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if (boom == maxBoom)
                        score += 500;
                    else 
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                    break;
                case "Plus1":
                    power++;
                    transform.localScale += new Vector3(0.5f,0.5f, 0);
                    if(speed >= 1)
                    {
                        speed -= 0.5f;
                    }
                    break;
                case "Plus2":
                    power += 2;
                    transform.localScale += new Vector3(1f, 1f, 0);
                    if (speed >= 1)
                    {
                        speed -= 1f;
                    }
                    break;
                case "Plus3":
                    power += 3;
                    transform.localScale += new Vector3(1.5f, 1.5f, 0);
                    if (speed >= 1)
                    {
                        speed -= 1.5f;
                    }
                    break;
                case "Minus1":
                    if (power >= 1)
                    {
                        power--;
                    }
                    transform.localScale -= new Vector3(0.5f, 0.5f, 0);
                    if (speed <= 6)
                    {
                        speed += 0.5f;
                    }
                    break;
                case "Minus2":
                    if (power >= 1)
                    {
                        power -= 2;
                    }
                    transform.localScale -= new Vector3(1f, 1f, 0);
                    if (speed <= 6)
                    {
                        speed += 1f;
                    }
                    break;
                case "Minus3":
                    if (power >= 1)
                    {
                        power -= 3;
                    }
                    transform.localScale -= new Vector3(1.5f, 1.5f, 0);
                    if (speed <= 6)
                    {
                        speed += 1.5f;
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }
    void AddFollower()
    {
        if (power == 4) 
        {
            followers[0].SetActive(true);
        }
        else if(power == 5) 
        {
            followers[1].SetActive(true);
        }
        else if (power == 6)
        {
            followers[2].SetActive(true);
        }
    }
    void offBoomEffect() 
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;

            }
        }
    }
}
