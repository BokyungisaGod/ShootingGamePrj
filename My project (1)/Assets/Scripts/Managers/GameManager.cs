using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int stage;
    public int Clearstage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    // Start is called before the first frame update
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;
    public ObjectManager objectManager;
    public static bool start;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public int spawnCount;
    public int spawnMax;
    public bool spawnEnd;
    // Update is called once per frame
    public Button[] btn;

    void Awake()
    {
        spawnList=new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB", "EnemyV", "EnemyH" };
        stage = PlayerPrefs.GetInt("stage");
        StageStart();

        player.GetComponent<SpriteRenderer>().sprite = SelectSceneManager.Inst.targetSprite;
    }

    public void StageStart() 
    {
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text="Stage "+stage +"\nStart";
        //stageAnim.GetComponent<Text>().text = "Stage 1" + "\nStart";
        clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear";
        //clearAnim.GetComponent<Text>().text = "Stage 1" + "\nClear";
        //ReadSpawnFile();
        fadeAnim.SetTrigger("In");
    }

    public void StageEnd()
    {
        clearAnim.SetTrigger("On");

        fadeAnim.SetTrigger("Out");

        player.transform.position = playerPos.position;

        if (stage == 3)
        {
            Invoke("GameOver", 6);
        }
        else
        {
            Clearstage = PlayerPrefs.GetInt("Clearstage");
            Clearstage++;
            PlayerPrefs.SetInt("Clearstage", Clearstage);
            Invoke("SceneChange",6);
        }
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Main");
    }
    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;
        TextAsset textFile = Resources.Load("Stage "+stage) as TextAsset;
        //TextAsset textFile = Resources.Load("Stage 1") as TextAsset;
        StringReader stringReader=new StringReader(textFile.text);

        while (stringReader != null) 
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
                break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            btn[0].onClick.Invoke();
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            btn[1].onClick.Invoke();
        }
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > nextSpawnDelay&&!spawnEnd) 
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy() 
    {
        int enemyIndex = 0;
        //switch (spawnList[spawnIndex].type)
        //{
        //    case "S":
        //        enemyIndex = 0;
        //        break;
        //    case "M":
        //        enemyIndex = 1;
        //        break;
        //    case "L":
        //        enemyIndex = 2;
        //        break;
        //    case "B":
        //        enemyIndex = 3;
        //        break;
        //    case "V":
        //        enemyIndex = 4;
        //        break;
        //    case "H":
        //        enemyIndex = 5;
        //        break;
        //}
        int ran = Random.Range(1, 10);
        if (ran < 3)
        {
            enemyIndex = 0;
            nextSpawnDelay = 1f;
        }
        else if (ran < 6)
        {
            enemyIndex = 1;
            nextSpawnDelay = 2f;

        }
        else if (ran < 8)
        {
            enemyIndex = 2;
            nextSpawnDelay = 2f;

        }
        else if (ran < 9)
        {
            enemyIndex = 4;
            nextSpawnDelay = 3f;

        }
        else if (ran < 10)
        {
            enemyIndex = 5;
            nextSpawnDelay = 3f;
        }

        ran = Random.Range(0, 9);
        int enemyPoint = ran;
        if (spawnCount == 20)
        {
            enemyIndex = 3;
            enemyPoint = 0;
        }
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
        enemyLogic.gameManager = this;
        if (enemyPoint == 5 || enemyPoint == 6) 
        {
            enemy.transform.Rotate(Vector3.back * 90);
            
            Vector2 dirVec= new Vector2(enemyLogic.speed * (-1), -1);
            rigid.velocity = dirVec;
            float bulletRotationAngle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;

            // 총알의 회전 설정
            Quaternion rotation = Quaternion.Euler(0, 0, bulletRotationAngle + 90f); // 총알이 나아가는 방향으로 머리가 가도록 90도 회전
            enemy.transform.rotation = rotation;
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            Vector2 dirVec = new Vector2(enemyLogic.speed, -1);

            rigid.velocity = dirVec;
            float bulletRotationAngle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;

            // 총알의 회전 설정
            Quaternion rotation = Quaternion.Euler(0, 0, bulletRotationAngle + 90f); // 총알이 나아가는 방향으로 머리가 가도록 90도 회전
            enemy.transform.rotation = rotation;
        }
        else 
        {
            rigid.velocity = enemyIndex == 5 ? new Vector2(0, enemyLogic.speed * (-1) * 3):new Vector2(0, enemyLogic.speed * (-1))  ;
            Vector2 dirVec = rigid.velocity;
            float bulletRotationAngle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;

            // 총알의 회전 설정
            Quaternion rotation = Quaternion.Euler(0, 0, bulletRotationAngle + 90f); // 총알이 나아가는 방향으로 머리가 가도록 90도 회전
            enemy.transform.rotation = rotation;
        }
            

        spawnCount++;
        if (spawnCount == spawnMax) 
        {
            spawnEnd= true;
            return;
        }

    }
    public void UpdateLifeIcon(int life) 
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < life; index++) 
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void UpdateBoomIcon(int boom)
    {
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void RespawnPlayer() 
    {
        Invoke("RespawnPlayerExe", 2f);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }
    public void CallExplosion(Vector3 pos,string type)
    {
        GameObject explosion = objectManager.MakeObj("explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }
    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene("Play");
        //0�ƴϸ� ���̸�
    }
}