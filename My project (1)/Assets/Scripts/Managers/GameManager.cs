using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public const int maxStageIndex = 100;

    public const int DefineCountS = 10;
    public const int DefineCountM = 5;
    public const int DefineCountL = 5;
    public const int DefineCountV = 3;
    public const int DefineCountH = 2;
    public const int DefineCountB = 1;

    public const int DefineEnemyTotalCount = 6;

    private int curStageIndex;

    private int maxSpawnCount;

    private List<(string, int)> enemyCountList = new List<(string, int)>();

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

    //public List<Spawn> spawnList;
    public int spawnIndex;
    public int spawnCount;
    public int spawnMax;
    public bool spawnEnd;
    // Update is called once per frame
    public Button[] btn;


    void Awake()
    {
        //spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB", "EnemyV", "EnemyH" };
        stage = PlayerPrefs.GetInt("stage");
        StageStart();

        Debug.Log(SelectSceneManager.Inst.targetSprite);

        player.GetComponent<SpriteRenderer>().sprite = SelectSceneManager.Inst.targetSprite;
    }

    private void UpdateUI() 
    {
        //fadeAnim.SetTrigger("Out");

        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "Stage " + curStageIndex + "\nStart";
        clearAnim.GetComponent<Text>().text = "Stage " + curStageIndex + "\nClear";

        //clearAnim.SetTrigger("On");

        fadeAnim.SetTrigger("In");
    }

    public void StageStart()
    {
        StageUp();

        //stageAnim.SetTrigger("On");
        //stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        ////stageAnim.GetComponent<Text>().text = "Stage 1" + "\nStart";
        //clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear";
        ////clearAnim.GetComponent<Text>().text = "Stage 1" + "\nClear";
        //
        ////ReadSpawnFile();
        //
        //LoadStageData();
        //
        //stage = 0;
        //
        ////spawnList = GetStageData($"Stage{stage}");
        ////
        ////nextSpawnDelay = spawnList[0].delay;
        //
        //fadeAnim.SetTrigger("In");
    }

    public void StageUp()
    {
        player.transform.position = playerPos.position;

        if (curStageIndex >= maxStageIndex)
        {
            // 게임이 끝났다는 ui정도 띄워주고
            // 게임의 최고 스테이지 이기 때문에 메인화면으로 돌립니다.

            UpdateUI();

            return;
        }

        curStageIndex++;

        enemyCountList.Clear();

        enemyCountList.Add(("S", DefineCountS * curStageIndex));
        enemyCountList.Add(("M", DefineCountM * curStageIndex));
        enemyCountList.Add(("L", DefineCountL * curStageIndex));
        enemyCountList.Add(("V", DefineCountV * curStageIndex));
        enemyCountList.Add(("H", DefineCountH * curStageIndex));
        enemyCountList.Add(("B", DefineCountB));

        maxSpawnCount = DefineCountS * curStageIndex +
                        DefineCountM * curStageIndex +
                        DefineCountL * curStageIndex +
                        DefineCountV * curStageIndex +
                        DefineCountH * curStageIndex +
                        DefineCountB;

        UpdateUI();
    }

    public void StageEnd()
    {
        clearAnim.SetTrigger("On");

        fadeAnim.SetTrigger("Out");

        player.transform.position = playerPos.position;

        stage++;

        //if (stage == 3)
        if (stage >= StageDic.Count - 1)
        {
            Invoke("GameOver", 6);
        }
        else
        {
            //Clearstage = PlayerPrefs.GetInt("Clearstage");
            //Clearstage++;

            //spawnList = GetStageData($"Stage{stage}");

            //PlayerPrefs.SetInt("Clearstage", Clearstage);
            //Invoke("SceneChange",6);
        }
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Main");
    }

    public Dictionary<string, List<Spawn>> StageDic;

    void LoadStageData() 
    {
        StageDic = new Dictionary<string, List<Spawn>>();

        var allStageDatas = Resources.LoadAll("Stage");

        foreach (var stageData in allStageDatas)
        {
            string key = stageData.name;

            var data = (TextAsset)stageData;

            StageDic.Add(key, CreateData(data));
        }
    }

    List<Spawn> CreateData(TextAsset asset)
    {
        var list = new List<Spawn>();

        StringReader stringReader = new StringReader(asset.text);

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
            list.Add(spawnData);
        }
        stringReader.Close();

        return list;
    }

    List<Spawn> GetStageData(string stageName) 
    {
        return StageDic[stageName];
    }

    //void ReadSpawnFile()
    //{
    //    spawnList.Clear();
    //    spawnIndex = 0;
    //    spawnEnd = false;
    //    TextAsset textFile = Resources.Load("Stage "+stage) as TextAsset;
    //    //TextAsset textFile = Resources.Load("Stage 1") as TextAsset;
    //    StringReader stringReader=new StringReader(textFile.text);
    //
    //    while (stringReader != null) 
    //    {
    //        string line = stringReader.ReadLine();
    //        Debug.Log(line);
    //        if (line == null)
    //            break;
    //
    //        Spawn spawnData = new Spawn();
    //        spawnData.delay = float.Parse(line.Split(',')[0]);
    //        spawnData.type = line.Split(',')[1];
    //        spawnData.point = int.Parse(line.Split(',')[2]);
    //        spawnList.Add(spawnData);
    //    }
    //    stringReader.Close();
    //    nextSpawnDelay = spawnList[0].delay;
    //}
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


    int TryGetEnemyIndex() 
    {
        int result = 0;
        bool isTrue = false;

        bool[] check = new bool[DefineEnemyTotalCount - 1];

        while (isTrue)
        {
            result = Random.Range(0,5);

            if (enemyCountList[result].Item2 <= 0)
            {
                check[result] = true;

                bool arrCheck = false;

                for (int i = 0; i < check.Length; i++) 
                {
                    arrCheck = check[i];
                }

                if (arrCheck)
                    break;

                continue;
            }

            var key = enemyCountList[result].Item1;
            var value = enemyCountList[result].Item2;
            value--;

            enemyCountList[result] = (key, value);

            isTrue = !isTrue;

            break;
        }

        if (!isTrue)
            result = DefineEnemyTotalCount - 1;

        return result;
    }

    void SpawnEnemy() 
    {


        //int enemyIndex = 0;
        //switch (spawnList[spawnIndex].type)
        //{
        //    case "S":
        //        {
        //            enemyIndex = 0;
        //            nextSpawnDelay = 1f;
        //        }
        //        break;
        //    case "M":
        //        {
        //            enemyIndex = 1;
        //            nextSpawnDelay = 2f;
        //        }
        //        break;
        //    case "L":
        //        {
        //            enemyIndex = 2;
        //            nextSpawnDelay = 2f;
        //        }
        //        break;
        //    case "B":
        //        {
        //            enemyIndex = 3;
        //            nextSpawnDelay = 2f;
        //        }
        //        break;
        //    case "V":
        //        {
        //            enemyIndex = 4;
        //            nextSpawnDelay = 3f;
        //        }
        //        break;
        //    case "H":
        //        {
        //            enemyIndex = 5;
        //            nextSpawnDelay = 3f;
        //        }
        //        break;
        //}
        //int ran = Random.Range(0, 7);  //스폰 포지션
        //
        //if (ran < 1)
        //{
        //    enemyIndex = 0;
        //    nextSpawnDelay = 1f;
        //}
        //else if (ran < 2)
        //{
        //    enemyIndex = 1;
        //    nextSpawnDelay = 2f;
        //
        //}
        //else if (ran < 4)
        //{
        //    enemyIndex = 2;
        //    nextSpawnDelay = 2f;
        //
        //}
        //else if (ran < 6)
        //{
        //    enemyIndex = 4;
        //    nextSpawnDelay = 3f;
        //
        //}
        //else if (ran < 8)
        //{
        //    enemyIndex = 5;
        //    nextSpawnDelay = 3f;
        //}
        //
        //int enemyPoint = Random.Range(0, 7);
        //
        //if (spawnCount == 20)
        //{
        //    enemyIndex = 3;
        //    enemyPoint = 0;
        //}

        int enemyIndex = TryGetEnemyIndex();

        //int enemyIndex = Random.Range(0, DefineEnemyTotalCount - 1);
        int spawnPointIndex = Random.Range(0, 7);

        nextSpawnDelay = enemyIndex + 3.0f;

        //if (spawnCount == 20)
        //{
        //    enemyIndex = 3;
        //    spawnPointIndex = 0;
        //}

        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex], false);
        enemy.transform.position = spawnPoints[spawnPointIndex].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
        enemyLogic.gameManager = this;
        enemyLogic.transform.localScale = enemyLogic.initScale;
        enemyLogic.transform.eulerAngles = enemyLogic.initEular;

        enemy.SetActive(true);

        if (spawnPointIndex == 3 || spawnPointIndex == 4) 
        {
            enemy.transform.Rotate(Vector3.back * 90);
            
            Vector2 dirVec= new Vector2(enemyLogic.speed * (-1), -1);
            rigid.velocity = dirVec;
            float bulletRotationAngle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;

            // 총알의 회전 설정
            Quaternion rotation = Quaternion.Euler(0, 0, bulletRotationAngle + 90f); // 총알이 나아가는 방향으로 머리가 가도록 90도 회전
            enemy.transform.rotation = rotation;
        }
        else if (spawnPointIndex == 5 || spawnPointIndex == 6)
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
        //if (spawnCount == spawnMax) 
        if (spawnCount == maxSpawnCount) 
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
        playerLogic.isControl = false;
        playerLogic.h = .0f;
        playerLogic.v = .0f;
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
